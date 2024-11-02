using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using Newtonsoft.Json; 


namespace server
{
    public partial class Form1 : Form
    {
        public class User
        {
            public string Username { get; set; } = "";
            public int winCount = 0, loseCount = 0;
            public string move = null;
            public bool isEliminated = false;


            public User()
            {
            }

            public void SetUsername(string username)
            {
                Username = username;
            }
            public void SetWinCount(int winCountenter)
            {
                winCount = winCountenter;
            }
            public void SetloseCount(int loseCountenter)
            {
                loseCount = loseCountenter;
            }
        }

        int currentMoveCounter = 0;
        int sentCounter = 500;

        List<User> allUsers = new List<User>();
        List<User> currentUsers = new List<User>();
        Queue<User> queueUsers = new Queue<User>();

        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();

        bool terminating = false;
        bool listening = false;

        string filePath = Path.Combine(Environment.CurrentDirectory, "leaderboard.json");


        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            allUsers = ReadUsersFromFile(filePath);
            InitializeComponent();
        }

        private void button_listen_Click(object sender, EventArgs e)
        {
            int serverPort;

            if(Int32.TryParse(textBox_port.Text, out serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);

                listening = true;
                button_listen.Enabled = false;
                
                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();
                
                logs.AppendText("Started listening on port: " + serverPort + "\n");
            }
            else
            {
                logs.AppendText("Please check port number \n");
            }
        }

        private void Accept()
        {
            while(listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept();
                    clientSockets.Add(newClient);
                    logs.AppendText("A client is connected.\n");

                    Thread receiveThread = new Thread(() => Receive(newClient)); 
                    receiveThread.Start();
                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        logs.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }

        private void Receive(Socket thisClient)
        {
            bool connected = true;
            User receivedUser = null;

            while (connected && !terminating)
            {
                try
                {
                    Byte[] ReceiveUserBuffer = new Byte[4096];
                    int receivedBytes = thisClient.Receive(ReceiveUserBuffer);
                    if (receivedUser == null && receivedBytes > 0)
                    {
                        
                        string userJson = Encoding.Default.GetString(ReceiveUserBuffer, 0, receivedBytes);
                        receivedUser = JsonConvert.DeserializeObject<User>(userJson);
                       
                        foreach(User tmpUser in currentUsers)
                        {
                            if(tmpUser != null)
                            {
                                if (tmpUser.Username == receivedUser.Username)
                                {
                                    receivedUser = null;
                                    SendUserList(thisClient, currentUsers, "rejectName");


                                }
                            }
                            
                        }
                        if (receivedUser != null)
                        {
                            logs.AppendText(" receivedUser null değil");

                            foreach (User tmpUser in allUsers)
                            {
                                if(tmpUser.Username == receivedUser.Username)
                                {
                                    receivedUser.winCount = tmpUser.winCount;
                                    receivedUser.loseCount = tmpUser.loseCount;
                                }
                            }
                            logs.AppendText("Received username: " + receivedUser.Username + "\n");
                            

                            if (currentUsers.Contains(null))
                            {
                                if(queueUsers.Count() > 0)
                                {
                                    for (int i = 0; i < currentUsers.Count(); i++)
                                    {
                                        if (currentUsers[i] == null)
                                        {
                                            currentUsers[i] = queueUsers.Dequeue();
                                            queueUsers.Enqueue(receivedUser);
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < currentUsers.Count(); i++)
                                    {
                                        if (currentUsers[i] == null)
                                        {
                                            currentUsers[i] = receivedUser;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (currentUsers.Count() < 4)
                                {
                                    currentUsers.Add(receivedUser);
                                }
                                else
                                {
                                    queueUsers.Enqueue(receivedUser);
                                }
                            }

                            if(clientSockets.Count == 1)
                            {
                                logs.AppendText(" client socket count 1");

                                SendUserList(thisClient, currentUsers, "currentUsers");
                                SendUserList(thisClient, queueUsers, "queueUsers");
                            }else
                            {
                                logs.AppendText(" client socket count 2+");

                                for (int i = 0; i < 1; i++)
                                {
                                    SendUserList(thisClient, currentUsers, "currentUsers");
                                    SendUserList(thisClient, queueUsers, "queueUsers");

                                    BroadcastUserList(thisClient, currentUsers, queueUsers, "Form2currentUsers", "Form2queueUsers");

                                }

                            }

                        }
                    }
                    else if (receivedBytes > 0)
                    {
                        string incomingMessage = Encoding.UTF8.GetString(ReceiveUserBuffer, 0, receivedBytes).Trim('\0');
                        logs.AppendText("rejoined username: " + receivedUser.Username + "\n");
                        if (!string.IsNullOrEmpty(incomingMessage))
                        {
                            ProcessReceivedData(incomingMessage, thisClient);
                        }
                        else
                        {
                        }
                    }
                }
                catch (Exception ex)
                {
                    logs.AppendText("Error: " + ex.Message + "\n");
                    thisClient.Close();
                    clientSockets.Remove(thisClient);
                    connected = false;
                }
            }
        }

        private void BroadcastUserList(Socket clientSocket, List<User> userList, Queue<User> userList2,  string listType, string listType2)
        {
            lock (lockObject)
            {
                string userListJson = listType + JsonConvert.SerializeObject(userList);
                Byte[] data = Encoding.UTF8.GetBytes(userListJson);

                string userListJson2 = listType2 + JsonConvert.SerializeObject(userList2);
                Byte[] data2 = Encoding.UTF8.GetBytes(userListJson2);

                foreach (Socket client in clientSockets)
                {
                    if (clientSocket != client)
                    {
                        client.Send(data);
                        client.Send(data2);
                    }
                }
            }
        }

        private void BroadcastUserList(Socket clientSocket, List<User> userList, string listType)
        {
            lock (lockObject)
            {
                string userListJson = listType + JsonConvert.SerializeObject(userList);
                Byte[] data = Encoding.UTF8.GetBytes(userListJson);


                foreach (Socket client in clientSockets)
                {
                    if (clientSocket != client)
                    {
                        client.Send(data);
                    }
                }
            }
        }

        private void SendUserList<T>(Socket clientSocket, T userList, string listType)
        {
            lock (lockObject)
            {
                string userListJson = listType + JsonConvert.SerializeObject(userList);
                Byte[] userListBuffer = Encoding.UTF8.GetBytes(userListJson);
                clientSocket.Send(userListBuffer);

                leaderBoardTextBox.Text = "";
                foreach (User curUser in currentUsers)
                {
                    if (curUser != null)
                    {
                        leaderBoardTextBox.AppendText(curUser.Username + " W: " + curUser.winCount + " L: " + curUser.loseCount + "\n");

                    }
                }
                foreach (User curUser in queueUsers)
                {
                    if (curUser != null)
                    {
                        leaderBoardTextBox.AppendText(curUser.Username + " W: " + curUser.winCount + " L: " + curUser.loseCount + "\n");

                    }
                }
            }
        }

        private static readonly object lockObject = new object();

        public static List<User> DetermineWinners(List<User> currentUsers)
        {
            Console.WriteLine("determine winners");

            List<User> tempCurrentUsers = new List<User>();
            foreach (User tmpUser in currentUsers)
            {
                if(tmpUser != null)
                {
                    if (tmpUser.move != null)
                    {
                        tempCurrentUsers.Add(tmpUser);
                        Console.WriteLine("tempCur eklendi: " +tmpUser.Username);
                    }
                }
            }


            var moveGroups = tempCurrentUsers.GroupBy(user => user.move).ToDictionary(g => g.Key, g => g.ToList());

            if (moveGroups.Count == 1)
            {
                // All the same choice
                return tempCurrentUsers;
            }
            else if (moveGroups.Count == 2)
            {
                // Two and Two scenarios
                if (moveGroups.ContainsKey("Rock") && moveGroups.ContainsKey("Paper"))
                {
                    Console.WriteLine("Rock-Paper");
                    return moveGroups["Paper"];
                }
                else if (moveGroups.ContainsKey("Rock") && moveGroups.ContainsKey("Scissors"))
                {
                    Console.WriteLine("Rock-Scissors");
                    return moveGroups["Rock"];
                }
                else if (moveGroups.ContainsKey("Paper") && moveGroups.ContainsKey("Scissors"))
                {
                    Console.WriteLine("Paper-Scissors");

                    return moveGroups["Scissors"];
                }
            }
            else if (moveGroups.Count == 3)
            {
                // Two of One Choice, One of Each of the Others
                if (moveGroups["Rock"].Count == 2)
                {
                    return moveGroups["Rock"];
                }
                else if (moveGroups["Paper"].Count == 2)
                {
                    return moveGroups["Paper"];
                }
                else if (moveGroups["Scissors"].Count == 2)
                {
                    return moveGroups["Scissors"];
                }
            }
            else
            {
                // Three Choices Alike
                if (moveGroups.ContainsKey("Rock") && moveGroups.ContainsKey("Paper") && moveGroups.ContainsKey("Scissors"))
                {
                    return new List<User>();
                }
            }

            
            return new List<User>(); // Default to empty list if no valid scenario
        }

        private void ProcessReceivedData(string data, Socket thisClient)
        {

            if (data.StartsWith("disconnect"))
            {
                int prefixLength = "disconnect".Length;
                string json = data.Substring(prefixLength);
                User deletingUser = JsonConvert.DeserializeObject<User>(json);


                bool inCurrentUser = false;

                for (int j = 0; j < currentUsers.Count; j++)
                {
                    try {
                        if(currentUsers.Count > 0 && currentUsers[j] != null)
                        {
                            if (currentUsers[j].Username == deletingUser.Username)
                            {
                                inCurrentUser = true;

                                if (queueUsers.Count() > 0)
                                {
                                    currentUsers[j] = queueUsers.Dequeue();
                                }
                                else
                                {
                                    currentUsers[j] = null;
                                }
                            }
                        }
                        
                    }catch(Exception e)
                    {

                    }
                    
                }

                if (!inCurrentUser)
                {
                    for (int b = 0; b < queueUsers.Count(); b++)
                    {
                        User candidate = queueUsers.Dequeue();
                        if(candidate != deletingUser)
                        {
                            queueUsers.Enqueue(candidate);
                        }
                    }
                }
                for (int i = 0; i < sentCounter/10; i++)
                {
                    lock (lockObject)
                    {
                        SendUserList(thisClient, currentUsers, "currentUsers");
                        SendUserList(thisClient, queueUsers, "queueUsers");

                        BroadcastUserList(thisClient, currentUsers, queueUsers, "Form2currentUsers", "Form2queueUsers");
                        BroadcastUserList(null, currentUsers, queueUsers, "Form2currentUsers", "Form2queueUsers");
                    }
                }
            }
            else if (data.StartsWith("rejoin"))
            {
                int prefixLength = "rejoin".Length;
                string json = data.Substring(prefixLength);
                User receivedUser = JsonConvert.DeserializeObject<User>(json);

                if (receivedUser != null)
                {
                    logs.AppendText("Rejoined username: " + receivedUser.Username + "\n");


                    if (currentUsers.Contains(null))
                    {
                        if (queueUsers.Count() > 0)
                        {
                            for (int i = 0; i < currentUsers.Count(); i++)
                            {
                                if (currentUsers[i] == null)
                                {
                                    currentUsers[i] = queueUsers.Dequeue();
                                    queueUsers.Enqueue(receivedUser);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < currentUsers.Count(); i++)
                            {
                                if (currentUsers[i] == null)
                                {
                                    currentUsers[i] = receivedUser;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (currentUsers.Count() < 4)
                        {
                            currentUsers.Add(receivedUser);
                        }
                        else
                        {
                            queueUsers.Enqueue(receivedUser);
                        }
                    }

                    if (clientSockets.Count == 1)
                    {
                        for (int i = 0; i < sentCounter; i++)
                        {

                            SendUserList(thisClient, currentUsers, "currentUsers");
                            SendUserList(thisClient, queueUsers, "queueUsers");
                            BroadcastUserList(null, currentUsers, queueUsers, "Form2currentUsers", "Form2queueUsers");

                        }

                    }
                    else
                    {
                        for (int i = 0; i < sentCounter; i++)
                        {

                            SendUserList(thisClient, currentUsers, "currentUsers");
                            SendUserList(thisClient, queueUsers, "queueUsers");

                            BroadcastUserList(thisClient, currentUsers, queueUsers, "Form2currentUsers", "Form2queueUsers");
                            BroadcastUserList(null, currentUsers, queueUsers, "Form2currentUsers", "Form2queueUsers");

                            
                        }



                    }

                }
            }else if (data.StartsWith("afterGameDisconnect"))
            {
                int prefixLength = "afterGameDisconnect".Length;
                string json = data.Substring(prefixLength);
                User deletingUser = JsonConvert.DeserializeObject<User>(json);
                logs.AppendText("deleting User: " + deletingUser.Username + "\n");

                bool inCurrentUser = false;

                for (int j = 0; j < currentUsers.Count; j++)
                {
                    try
                    {
                        if (currentUsers.Count > 0 && currentUsers[j] != null)
                        {
                            if (currentUsers[j].Username == deletingUser.Username)
                            {
                                inCurrentUser = true;

                                currentUsers[j] = null;
                            }
                        }

                    }
                    catch (Exception e)
                    {

                    }

                }

                if (!inCurrentUser)
                {
                    for (int b = 0; b < queueUsers.Count(); b++)
                    {
                        User candidate = queueUsers.Dequeue();
                        logs.AppendText("dequeulanan isim: " + deletingUser.Username + "\n");

                        if (candidate.Username != deletingUser.Username)
                        {
                            queueUsers.Enqueue(candidate);
                            logs.AppendText("enquelanan isim: " + candidate.Username + "\n");
                        }
                    }
                }
                for (int i = 0; i < sentCounter; i++)
                {
                    SendUserList(thisClient, currentUsers, "currentUsers");
                    SendUserList(thisClient, queueUsers, "queueUsers");

                    BroadcastUserList(thisClient, currentUsers, queueUsers, "Form2currentUsers", "Form2queueUsers");

                    BroadcastUserList(null, currentUsers, queueUsers, "Form2currentUsers", "Form2queueUsers");
                }
            }else if (data.StartsWith("move"))
            {
                currentMoveCounter++;
                int prefixLength = "move".Length;
                string json = data.Substring(prefixLength);
                User currentUser = JsonConvert.DeserializeObject<User>(json);

                foreach(User tempUser in currentUsers)
                {
                    if(tempUser != null)
                    {
                        if (tempUser.Username == currentUser.Username)
                        {
                            tempUser.move = currentUser.move;
                        }
                    }
                    
                }

                if(currentMoveCounter == currentUsers.Count)
                {
                    Console.WriteLine("currentMoveCounter == currentUsers.Count");

                    List <User> winners = DetermineWinners(currentUsers);

                    foreach(User tempUser in currentUsers)
                    {
                        if(tempUser != null)
                        {
                            tempUser.isEliminated = true;
                        }
                        
                    }

                    foreach(User tempUser in currentUsers)
                    {
                        foreach(User winUser in winners)
                        {
                            if(tempUser != null)
                            {
                                if (tempUser.Username == winUser.Username)
                                {
                                    tempUser.isEliminated = false;
                                }
                            }
                            
                        }
                    }
                    foreach (User tmpUser in winners)
                    {
                        Console.WriteLine("winner: " + tmpUser.Username);
                    }
                    foreach (User tmpUser in currentUsers)
                    {
                        if(tmpUser != null)
                        {
                            Console.WriteLine(tmpUser.Username + " " + tmpUser.isEliminated);

                        }
                    }


                    for (int i = 0; i < sentCounter; i++)
                    {
                        SendUserList(thisClient, currentUsers, "currentUsers");
                        SendUserList(thisClient, queueUsers, "queueUsers");

                        BroadcastUserList(thisClient, currentUsers, queueUsers, "Form2currentUsers", "Form2queueUsers");
                        BroadcastUserList(null, currentUsers, queueUsers, "Form2currentUsers", "Form2queueUsers");
                    }
                    for (int i = 0; i < sentCounter; i++)
                    {
                        BroadcastUserList(thisClient, winners, "winnerWinner");
                        BroadcastUserList(null, winners, "winnerWinner");
                    }
                    currentMoveCounter = 0;
                    if(winners.Count == 1)
                    {
                        foreach(User tmpUser in currentUsers)
                        {
                            if(tmpUser != null)
                            {
                                if (tmpUser.Username == winners[0].Username)
                                {
                                    tmpUser.winCount++;
                                }
                                else
                                {
                                    tmpUser.loseCount++;
                                }
                            }
                            
                        }

                        foreach (User tmpUser in currentUsers)
                        {
                            if(tmpUser != null)
                            {
                                UpdateWinLoseCounts(filePath, tmpUser.Username, tmpUser.winCount, tmpUser.loseCount);

                            }
                        }
                    }


                }


            }



        }

        public static void WriteWinLoseCountsToFile(List<User> currentUsers)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "leaderboard.json");

            Console.WriteLine($"Writing to file: {filePath}");

            try
            {
                // Serialize the currentUsers list to a JSON string
                string json = JsonConvert.SerializeObject(currentUsers, Formatting.Indented);

                // Write the JSON string to the file
                File.WriteAllText(filePath, json);

                Console.WriteLine("Win and lose counts written to file as JSON.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
            }
        }

        public static List<User> ReadUsersFromFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    return JsonConvert.DeserializeObject<List<User>>(json);
                }
                else
                {
                    Console.WriteLine("File not found.");
                    return new List<User>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
                return new List<User>();
            }
        }

        public static void UpdateWinLoseCounts(string filePath, string username, int wins, int losses)
        {
            List<User> users = ReadUsersFromFile(filePath);

            User user = users.Find(u => u.Username == username);
            if (user != null)
            {
                user.winCount = wins;
                user.loseCount = losses;
            }
            else
            {
                users.Add(new User { Username = username, winCount = wins, loseCount = losses });
            }

            WriteWinLoseCountsToFile(users);
        }


        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox_port_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox_message_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
