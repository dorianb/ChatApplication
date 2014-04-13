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

namespace CSharpClientApplication
{
    public partial class FormConnection : Form
    {
        public delegate void ConnectingClient(Client client);
        public event ConnectingClient OnCreateConnectingClient;

        private Thread t;
        private bool cancel = false;

        public FormConnection()
        {
            InitializeComponent();

            this.FormClosing += FormConnection_FormClosing;
        }

        private delegate void ProgressBarDelegate();

        // Lancer dans un thread
        private void connectToServer()
        {
            Client newClient = null;

            Socket socketConnection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                socketConnection.Connect(IPAddress.Parse(FormMain.ipAddressServer), FormMain.port);

                // If Connection Successful
                if (socketConnection.Connected)
                {
                    socketConnection.Receive(new byte[1]);

                    newClient = new Client(new IPEndPoint(IPAddress.Parse(FormMain.ipAddressServer), FormMain.port), this.textBoxUsername.Text, this.textBoxPassword.Text);
                    newClient.ClientSocket = socketConnection;
                    FormMain.client = newClient;

                    ChatLib.MessageChat msg = new ChatLib.MessageChat(ChatLib.MessageType.ConnectToServer, newClient.UserName, null, newClient.Password);

                    ChatLib.ChatLib.SendMessage(newClient.ClientSocket, msg);
                }

                if (cancel) return;

                this.Invoke((MethodInvoker)delegate
                {
                    buttonOK.Enabled = true;
                    textBoxPassword.Enabled = true;
                    textBoxUsername.Enabled = true;
                    this.OnCreateConnectingClient(newClient);
                    this.Close();
                });
            }
            catch (Exception ex)
            {
                if (cancel) return;

                this.Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show(ex.Message);
                });
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (this.textBoxUsername.Text.Length == 0 || this.textBoxPassword.Text.Length == 0)
            {
                MessageBox.Show("Username and/or password is missing...");
            }
            else
            {
                t = new Thread(new ThreadStart(RunThread));
                t.IsBackground = true;
                t.Start();
            }
        }

        private void RunThread()
        {
            ProgressBar pb = new ProgressBar();
            pb.Style = ProgressBarStyle.Marquee;
            pb.Location = new Point(30, this.Bounds.Height - 80);

            if (cancel) return;

            this.Invoke((MethodInvoker)delegate
            {
                this.Controls.Add(pb);
                buttonOK.Enabled = false;
                textBoxPassword.Enabled = false;
                textBoxUsername.Enabled = false;
            });

            connectToServer();

            if (cancel) return;

            this.Invoke((MethodInvoker)delegate
            {
                buttonOK.Enabled = true;
                textBoxPassword.Enabled = true;
                textBoxUsername.Enabled = true;
                this.Controls.Remove(pb);
            });
        }

        private void FormConnection_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (t != null && t.IsAlive)
            {
                try
                {
                    cancel = true;
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
