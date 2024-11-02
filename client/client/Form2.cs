using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;

using Newtonsoft.Json;
using System.Net.Sockets;

namespace client
{
    public partial class Form2 : Form
    {
        bool isPlayable = false;
        bool isRoundFinished = false;
        bool connected = true;
        int seconds;
        private Socket clientSocket;
        private User currentUser;

        List<User> currentUsers;
        List<User> winners;
        Queue<User> queueUsers;

        public Form2(List<User> currentUsers, Queue<User> queueUsers, User currentUser, Socket clientSocket)
        {
            
            InitializeComponent();

            this.currentUser = currentUser;
            this.clientSocket = clientSocket;

            this.currentUsers = currentUsers;
            this.queueUsers = queueUsers;


            Thread receiveThread = new Thread(Receive);
            receiveThread.Start();

            this.FormClosing += Form2_FormClosing;

            if(queueUsers.Count + currentUsers.Count >= 5) {
                isPlayable = true;
                messagegeneric.Text = "Game Has Already Started!";
            }
            UpdateUI();



        }

        private void Receive()
        {
            while (connected)
            {
                try
                {

                    byte[] buffer = new byte[8192];
                    int bytesReceived = clientSocket.Receive(buffer);
                    if (bytesReceived > 0)
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

                        ProcessReceivedData(message);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void ProcessReceivedData(string data)
        {
            if (data.StartsWith("Form2currentUsers"))
            {
                int prefixLength = "Form2currentUsers".Length;
                string json = data.Substring(prefixLength);
                currentUsers = JsonConvert.DeserializeObject<List<User>>(json);
            }
            if (data.StartsWith("Form2queueUsers"))
            {
                int prefixLength = "Form2queueUsers".Length;
                string json = data.Substring(prefixLength);
                queueUsers = JsonConvert.DeserializeObject<Queue<User>>(json);

            }
            if (data.StartsWith("winnerWinner"))
            {
                isRoundFinished = true;
                Console.WriteLine("winnerWinner chickern Dinner XXXXXXX");
                try
                {
                    string[] segments = data.Split(new[] { "winnerWinner" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string segment in segments)
                    {
                        string json = segment.Trim();
                        if (!string.IsNullOrEmpty(json))
                        {
                            winners = JsonConvert.DeserializeObject<List<User>>(json);
                            
                        }
                    }
                }
                catch (JsonReaderException ex)
                {
                    Console.WriteLine($"JSON reader exception: {ex.Message}");
                    Console.WriteLine($"Faulty JSON data: {data}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception in processing data: {ex.Message}");
                }

                

            }

            this.Invoke((MethodInvoker)delegate {
                UpdateUI();
            });

        }

        private void UpdateUI()
        {
            List<User> displayWaitingList = queueUsers.ToList();

            for (int i = 0; i < currentUsers.Count(); i++)
            {
                int x = i + 1;
                string currentTableName = "label" + x.ToString();

                Label foundLabel = null;

                foreach (Control panel in this.Controls)
                {
                    if (panel is Panel)
                    {
                        foreach (Control control in panel.Controls)
                        {
                            if (control is Label label && label.Name == currentTableName)
                            {
                                foundLabel = label;
                                break;
                            }
                        }
                    }
                }
                if (foundLabel != null && foundLabel is Label)
                {
                    if(currentUsers[i] != null)
                    {
                        foundLabel.Text = currentUsers[i].Username + "\n" + "W: " + currentUsers[i].winCount + "\n" + "L: " + currentUsers[i].loseCount;
                    }
                    else
                    {
                        foundLabel.Text = "";
                    }
                }
            }

            if (queueUsers.Count > 0)
            {
                label5.Visible = true;
                panel5.Visible = true;
                for (int i = 5; i < queueUsers.Count() + 5; i++)
                {
                    int x = i + 1;
                    string currentTableName = "label" + x.ToString();

                    Label foundLabel = null;

                    foreach (Control panel in this.Controls)
                    {
                        if (panel is Panel)
                        {
                            foreach (Control control in panel.Controls)
                            {
                                if (control is Label label && label.Name == currentTableName)
                                {
                                    foundLabel = label;
                                    break;
                                }
                            }
                        }
                    }
                    if (foundLabel != null && foundLabel is Label)
                    {
                        foundLabel.Text = displayWaitingList[i - 5].Username + " W: " + displayWaitingList[i - 5].winCount + " L: " + displayWaitingList[i - 5].loseCount;
                    }
                    else
                    {
                    }
                }

                int y = queueUsers.Count() + 6;
                string CurrentTableName2 = "label" + y.ToString();
                Label foundLabel2 = null;

                foreach (Control panel in this.Controls)
                {
                    if (panel is Panel)
                    {
                        foreach (Control control in panel.Controls)
                        {
                            if (control is Label label && label.Name == CurrentTableName2)
                            {
                                foundLabel2 = label;
                                break;
                            }
                        }
                    }
                }
                if (foundLabel2 != null && foundLabel2 is Label)
                {
                    foundLabel2.Text = "";
                }

            }
            else
            {
                label5.Visible = false;
                panel5.Visible = false;
            }

            int currentUsersInTable = 0;
            foreach(User tmpUser in currentUsers)
            {
                if(tmpUser != null)
                {
                    currentUsersInTable++;
                }
            }
            if (currentUsersInTable == 4 && isPlayable == false)
            {
                startCounter1();
            }
            if(currentUsersInTable != 4)
            {
                isPlayable = false;
            }

            if (isRoundFinished)
            {
                /*
                foreach (User tmpUser in winners)
                {
                    Console.WriteLine("winner: " + tmpUser.Username);
                }
                foreach (User tmpUser in currentUsers)
                {
                    Console.WriteLine(tmpUser.Username + " " + tmpUser.isEliminated);
                }
                */
                if(currentUsers[0] != null)
                {
                    user1_move.Text = "last pick: " + currentUsers[0].move;

                }
                else
                {
                    user1_move.Text = "";

                }
                if (currentUsers[1] != null)
                {
                    user2_move.Text = "last pick: " + currentUsers[1].move;

                }
                else
                {
                    user2_move.Text = "";

                }
                if (currentUsers[2] != null)
                {
                    user3_move.Text = "last pick: " + currentUsers[2].move;

                }
                else
                {
                    user3_move.Text = "";

                }
                if (currentUsers[3] != null)
                {
                    user4_move.Text = "last pick: " + currentUsers[3].move;

                }
                else
                {
                    user4_move.Text = "";

                }

                foreach (User tmpUser in currentUsers)
                {
                    if(tmpUser != null)
                    {
                        if (tmpUser.Username == currentUser.Username)
                        {
                            currentUser.isEliminated = tmpUser.isEliminated;
                            currentUser.winCount = tmpUser.winCount;
                            currentUser.loseCount = tmpUser.loseCount;
                            currentUser.isEliminated = tmpUser.isEliminated;
                            currentUser.move = tmpUser.move;
                        }
                    }
                    
                }
                if (currentUser.isEliminated == true)
                {
                    rock.Enabled = false;
                    rock.Visible = false;
                    paper.Enabled = false;
                    paper.Visible = false;
                    scissors.Enabled = false;
                    scissors.Visible = false;
                }

                if(winners.Count == 1)
                {
                    rock.Enabled = false;
                    rock.Visible = false;
                    paper.Enabled = false;
                    paper.Visible = false;
                    scissors.Enabled = false;
                    scissors.Visible = false;
                    startCounter3();

                }
                else
                {
                    currentUser.move = null;
                    isRoundFinished = false;
                    winners.Clear();
                    startCounter2();
                }

            }


        }

        private void startCounter1()
        {
            seconds = 5;
            timer1.Start();
        }
        private void startCounter2()
        {
            seconds = 10;
            timer2.Start();
        }
        private void startCounter3()
        {
            seconds = 6;
            timer3.Start();
        }


        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            connected = false;
            currentUser.move = null;
            SendMoveToServer(clientSocket, currentUser);
            SendUserToRemove(clientSocket, currentUser, "disconnect");
        }

        private void SendUserToRemove<T>(Socket clientSocket, T userData, string listType)
        {
            string userListJson = listType + JsonConvert.SerializeObject(userData);
            Byte[] userListBuffer = Encoding.UTF8.GetBytes(userListJson);
            clientSocket.Send(userListBuffer);
        }

        private void SendUserToRejoin<T>(Socket clientSocket, T userData, string listType)
        {
            string userListJson = listType + JsonConvert.SerializeObject(userData);
            Byte[] userListBuffer = Encoding.UTF8.GetBytes(userListJson);
            clientSocket.Send(userListBuffer);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            messagegeneric.Text = "Starting game in " + seconds--.ToString() + " seconds..";
            if(seconds < 0)
            {
                timer1.Stop();
                messagegeneric.Text = "Go!";
                isPlayable = true;

                if (isPlayable == true)
                {
                    rock.Enabled = true;
                    rock.Visible = true;
                    paper.Enabled = true;
                    paper.Visible = true;
                    scissors.Enabled = true;
                    scissors.Visible = true;
                    
                    startCounter2();


                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            messagegeneric.Text = "Choose your move in " + seconds--.ToString() + " seconds..";
            if (seconds < 0)
            {
                timer2.Stop();
                messagegeneric.Text = "Result";
                SendMoveToServer(clientSocket, currentUser);


            }
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            messagegeneric.Text = "The winner is: " + winners[0].Username + "!";
            seconds--;
            if (seconds < 0)
            {
                timer3.Stop();

                isPlayable = false;
                isRoundFinished = false;
                winners.Clear();
                user1_move.Text = "";
                user2_move.Text = "";
                user3_move.Text = "";
                user4_move.Text = "";
                messagegeneric.Text = "Waiting for the players...";

                this.Invoke((MethodInvoker)delegate {
                    UpdateUI();
                });
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //leave button
            if(isPlayable == true)
            {
                SendUserToRemove(clientSocket, currentUser, "afterGameDisconnect");

            }
            else
            {
                SendUserToRemove(clientSocket, currentUser, "disconnect");

            }

            rejoinButton.Visible = true;
            rejoinButton.Enabled = true;
            leaveButton.Visible = false;
            leaveButton.Enabled = false;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //rejoin button
            if(isPlayable == false)
            {
                SendUserToRejoin(clientSocket, currentUser, "rejoin");
                rejoinButton.Visible = false;
                rejoinButton.Enabled = false;
                leaveButton.Visible = true;
                leaveButton.Enabled = true;
            }
            
        }

        private void user2_move_Click(object sender, EventArgs e)
        {

        }

        private void SendMoveToServer<T>(Socket clientSocket, T userData)
        {
            string moveJson = "move" + JsonConvert.SerializeObject(userData);
            Byte[] userListBuffer = Encoding.UTF8.GetBytes(moveJson);
            clientSocket.Send(userListBuffer);
        }

       

        private void rock_Click(object sender, EventArgs e)
        {
            currentUser.move = "Rock";
            
        }

        //paper button
        private void button2_Click_1(object sender, EventArgs e)
        {
            currentUser.move = "Paper";
            
        }

        private void scissors_Click(object sender, EventArgs e)
        {
            currentUser.move = "Scissors";
            
        }

        
    }   
}
