using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpClientApplication
{
    public partial class FormNewRoom : Form
    {
    //    public delegate void CreateNewChatRoom(string chatRoomName);
    //    public event CreateNewChatRoom OnCreateNewChatRoom;

        private Client client;

        //private Thread t;

        public FormNewRoom()
        {
            InitializeComponent();

            this.client = FormMain.client;

            //t = new Thread(new ParameterizedThreadStart(receiveMessage));
            //t.IsBackground = true;
        }

        //private void receiveMessage(object obj)
        //{
        //    string chatRoomName = (string)obj;

        //    byte[] buffer = new byte[ChatLib.ChatLib.MESSAGE_MAX_SIZE];

        //    while (true)
        //    {
        //        System.Diagnostics.Debug.WriteLine("FormNewRoom Waiting");
        //        int nbBytes = client.ClientSocket.Receive(buffer);

        //        System.Diagnostics.Debug.WriteLine("FormNewRoom received");

        //        if (nbBytes > 0)
        //        {
        //            try
        //            {
        //                ChatLib.MessageChat msg = ChatLib.ChatLib.createMessageFromString(ChatLib.ChatLib.GetString(buffer, nbBytes));

        //                if (msg.MessageType == ChatLib.MessageType.ChatRoomCreate)
        //                {
        //                    if (msg.ContentMessage == "true")
        //                    {
        //                        this.Invoke((MethodInvoker)delegate
        //                        {
        //                            this.OnCreateNewChatRoom(chatRoomName);
        //                            this.Close();
        //                        });
        //                    }

        //                    break;
        //                }
        //            }
        //            catch (JsonReaderException)
        //            {
        //            }
        //        }
        //    }
        //}

        private void buttonCreateChatRoom_Click(object sender, EventArgs e)
        {
            string chatRoomName = this.textBoxChatRoomName.Text.Trim();

            if (chatRoomName.Length > 0)
            {
                //t.Start(chatRoomName);

                ChatLib.MessageChat msg = new ChatLib.MessageChat(ChatLib.MessageType.ChatRoomCreate, client.UserName, null, chatRoomName);
                if (msg != null)
                {
                    System.Diagnostics.Debug.WriteLine("Sending : " + msg.ToString());

                    ChatLib.ChatLib.SendMessage(client.ClientSocket, msg);

                    //this.OnCreateNewChatRoom(chatRoomName);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Chat Room name is missing...");
            }
        }

        public TextBox TextBox
        {
            get { return this.textBoxChatRoomName; }
        }
    }
}
