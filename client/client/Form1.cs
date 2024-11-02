using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json; 

namespace client
{
    public partial class Form1 : Form
    {
        public class BooleanReference
        {
            public int Value { get; set; }

            public BooleanReference(int initialValue)
            {
                Value = initialValue;
            }
        }
        static int ReceiveCounter = 0;
        private bool keepReceiving = true;

        BooleanReference countForm2Opened = new BooleanReference(0);


        List<User> currentUsers = new List<User>();
        Queue<User> queueUsers = new Queue<User>();

        User currentUser = new User();

        bool terminating = false;
        bool connected = false;
        Socket clientSocket;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = textBox_ip.Text;
            currentUser.SetUsername(username_textbox.Text);

            if (Int32.TryParse(textBox_port.Text, out int portNum))
            {
                try
                {
                    clientSocket.Connect(IP, portNum);
                    button_connect.Enabled = false;
                    
                    connected = true;

                    Thread receiveThread = new Thread(Receive);
                    receiveThread.Start();

                    if(countForm2Opened.Value > 1)
                    {
                        receiveThread.Abort();
                    }

                    string userJson = JsonConvert.SerializeObject(currentUser);  
                    Byte[] userBuffer = Encoding.Default.GetBytes(userJson);
                    clientSocket.Send(userBuffer);

                }
                catch (Exception ex)  
                {
                    
                }
            }
        }

        private void Receive()
        {
            while (connected && keepReceiving)
            {
                try
                {
                    byte[] buffer = new byte[4096]; 
                    int receivedBytes = clientSocket.Receive(buffer);

                    if (receivedBytes > 0) 
                    {
                        string incomingMessage = Encoding.UTF8.GetString(buffer, 0, receivedBytes).Trim('\0');

                        if (!string.IsNullOrEmpty(incomingMessage))
                        {
                            

                            if (countForm2Opened.Value <= 1)
                            {
                                //username_textbox.Text = "process aldı";
                                ProcessReceivedData(incomingMessage);
                                countForm2Opened.Value = countForm2Opened.Value + 1;
                            }
                            
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                        break; 
                    }

                    ReceiveCounter++;
                }
                catch (SocketException se)
                {
                    break;
                }
                catch (Exception ex)
                {
                    break; 
                }
            }

            if (connected && !terminating)
            {

            }
            else
            {
                clientSocket.Close();
                connected = false;
            }
        }

        private readonly object lockObject = new object();

        private void ProcessReceivedData(string data)
        {
            Console.WriteLine(data);
            lock (lockObject)
            {
                if (data.StartsWith("rejectName"))
                {
                    username_textbox.Text = "Username is Taken!";
                    countForm2Opened.Value = countForm2Opened.Value - 1;
                    button_connect.Enabled = true;
                }
                else
                {
                    //username_textbox.Text = "else e girdi";

                    if (data.StartsWith("currentUsers"))
                    {
                        //usernam_label.Text = "current users";

                        int prefixLength = "currentUsers".Length;
                        string json = data.Substring(prefixLength);

                        try
                        {
                            currentUsers = JsonConvert.DeserializeObject<List<User>>(json);
                            Console.WriteLine("currentUsers deserialized:");
                            foreach (User tmpUser in currentUsers)
                            {
                                if(tmpUser != null)
                                {
                                    Console.WriteLine($"Current User: {tmpUser.Username}");

                                }
                            }
                        }
                        catch (JsonException ex)
                        {
                            Console.WriteLine($"JSON deserialization error: {ex.Message}");
                            //usernam_label.Text = "JSON error";
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Unexpected error: {ex.Message}");
                            //usernam_label.Text = "Unexpected error";
                        }
                    }
                    if (data.StartsWith("queueUsers"))
                    {
                        //label1.Text = "queue users";

                        int prefixLength = "queueUsers".Length;
                        string json = data.Substring(prefixLength);
                        //textBox_port.Text = "str: " + json;

                        try
                        {
                            string[] segments = json.Split(new[] { "queueUsers", "currentUsers", "winnerWinner" }, StringSplitOptions.RemoveEmptyEntries);

                            foreach (string segment in segments)
                            {
                                string trimmedSegment = segment.Trim();
                                if (!string.IsNullOrEmpty(trimmedSegment))
                                {
                                    // Deserialize only if this part belongs to queueUsers
                                    if (data.Contains("queueUsers" + trimmedSegment))
                                    {
                                        queueUsers = JsonConvert.DeserializeObject<Queue<User>>(trimmedSegment);
                                        //textBox_port.Text += "Processed queueUsers: " + trimmedSegment + Environment.NewLine;
                                    }
                                }
                            }

                            //label1.Text = "sonra";
                            keepReceiving = false;

                            this.Invoke((MethodInvoker)delegate
                            {
                                Console.WriteLine("form2ac");
                                // Ensure currentUsers is still populated before opening Form2
                                Console.WriteLine("currentUsers before Form2:");
                                foreach (User tmpUser in currentUsers)
                                {
                                    if(tmpUser != null)
                                    {
                                        Console.WriteLine($"Current User Before Form2: {tmpUser.Username}");

                                    }
                                }
                                Form2 form2 = new Form2(currentUsers, queueUsers, currentUser, clientSocket);
                                form2.Show();
                                this.Hide();
                            });
                        }
                        catch (JsonException ex)
                        {
                            Console.WriteLine($"JSON deserialization error: {ex.Message}");
                            //label1.Text = "JSON error";
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Unexpected error: {ex.Message}");
                            //label1.Text = "Unexpected error";
                        }
                    }
                }
            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_ip_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}