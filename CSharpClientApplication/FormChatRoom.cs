using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace CSharpClientApplication
{
    public partial class FormChatRoom : Form
    {
        private volatile bool closingWindow = false;

        private string chatRoomName;
        private Client client = null;

        private int maxHeight=200;
        private int maxWidth=200;

        public FormChatRoom(string name)
        {
            InitializeComponent();

            client = FormMain.client;
            this.chatRoomName = name;
            this.Text = chatRoomName + " - Chat Room";

            this.FormClosing += FormChatRoom_FormClosing;

            this.richTextBoxChat.LinkClicked += richTextBoxChat_LinkClicked;

            this.textBoxMessage.AllowDrop = true;
        }

        private void richTextBoxChat_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        public string ChatRoomName
        {
            get { return this.chatRoomName; }
        }

        private void FormChatRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            ChatLib.MessageChat msg = new ChatLib.MessageChat(ChatLib.MessageType.ChatRoomExit, client.UserName, this.chatRoomName, this.chatRoomName);

            ChatLib.ChatLib.SendMessage(client.ClientSocket, msg);

            closingWindow = true;
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string msgToSend = this.textBoxMessage.Text;

            ChatLib.MessageChat msg = new ChatLib.MessageChat(ChatLib.MessageType.ClientChatRoomMessage, client.UserName, this.chatRoomName, msgToSend);

            ChatLib.ChatLib.SendMessage(client.ClientSocket, msg);

            this.textBoxMessage.Text = "";
        }

        private void writeUsername(string username)
        {
            this.richTextBoxChat.SelectionFont = new Font(this.richTextBoxChat.SelectionFont, FontStyle.Bold);

            if (username == client.UserName)
                this.richTextBoxChat.SelectionColor = Color.Red;
            else
                this.richTextBoxChat.SelectionColor = Color.Green;

            this.richTextBoxChat.SelectedText += username + " : ";
        }

        private void writeMessage(string message)
        {
            this.richTextBoxChat.SelectionFont = new Font(this.richTextBoxChat.SelectionFont, FontStyle.Regular);
            this.richTextBoxChat.SelectionColor = Color.Black;
            this.richTextBoxChat.SelectedText += message.Trim() + Environment.NewLine;
        }

        private void drawImage(string message)
        {


            Image im;

            MemoryStream ms = new MemoryStream(ChatLib.ChatLib.GetBytes(message));
            im = Image.FromStream(ms);

            im=resizeImage(im);

            Clipboard.SetDataObject(im);

            Console.WriteLine("je suis laaaaa");
            this.richTextBoxChat.ReadOnly = false;
            if (this.richTextBoxChat.CanPaste(DataFormats.GetFormat(DataFormats.Bitmap)))
            {

                Console.WriteLine("Je colle l'image!");
                this.richTextBoxChat.Paste(DataFormats.GetFormat(DataFormats.Bitmap));

            }
            this.richTextBoxChat.ReadOnly =true;

            this.richTextBoxChat.SelectedText += Environment.NewLine;




        }

        private void textBoxMessage_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxMessage.Text.Length > 0)
            {
                this.buttonSend.Enabled = true;
            }
            else
            {
                this.buttonSend.Enabled = false;
            }
        }


        public void UpdateConnectedUsersInChatRoom(ChatLib.MessageChat message)
        {
            if (message.TargetName == chatRoomName)
            {
                string[] listClient = message.ContentMessage.Split('\\');
                this.listClient.Items.Clear();
                foreach (string s in listClient)
                {
                    //if (s.Length > 0 && client.UserName != s)
                    //{
                    //    if (this.listClient.FindItemWithText(s) == null)
                    //    {
                    //        ListViewItem item = new ListViewItem(s);
                    //        this.listClient.Items.Add(item);
                    //    }
                    //}
                    if (s.Length > 0 && client.UserName != s)
                    {
                        ListViewItem item = new ListViewItem(s);
                        this.listClient.Items.Add(item);
                    }
                    
                }
            }
        }

        public void OnReceivedMessage(ChatLib.MessageChat message)
        {
            // Suivant le type de message 
            switch (message.MessageType)
            {
                case ChatLib.MessageType.None: break;

                // Message texte chat room
                case ChatLib.MessageType.ClientChatRoomMessage:
                    if (message.TargetName == chatRoomName)
                    {
                        this.writeUsername(message.SenderName);
                        this.writeMessage(message.ContentMessage);
                    }
                    break;

                case ChatLib.MessageType.UpdateConnectedUsersInChatRoom:
                    if (message.TargetName == chatRoomName)
                    {
                        this.listClient.Items.Clear();
                        string[] listClient = message.ContentMessage.Split('\\');
                        foreach (string s in listClient)
                        {
                            ListViewItem item = new ListViewItem(s);
                            this.listClient.Items.Add(item);
                        }

                    }
                    break;

                case ChatLib.MessageType.ClientPrivateMessage:
                    break;


                case ChatLib.MessageType.ClientChatRoomImage:

                    if (message.TargetName == chatRoomName)
                    {
                        this.writeUsername(message.SenderName);
                        this.drawImage(message.ContentMessage);
                    }
                    break;


                    
            }
        }

        //private void receiveDatas()
        //{
        //    byte[] buf = new byte[ChatLib.ChatLib.MESSAGE_MAX_SIZE];
        //    int bufLenght;

        //    ChatLib.MessageChat message = null;

        //    while (true)
        //    {
        //        if (closingWindow) return;

        //        //listen the message from the server

        //        bufLenght = FormMain.client.ClientSocket.Receive(buf);

        //        message = ChatLib.ChatLib.createMessageFromString(ChatLib.ChatLib.GetString(buf, bufLenght));

        //        // Suivant le type de message 
        //        switch (message.MessageType)
        //        {
        //            case ChatLib.MessageType.None: break;

        //            // Message texte chat room
        //            case ChatLib.MessageType.ClientChatRoomMessage:
        //                if (message.TargetName == chatRoomName)
        //                {
        //                    this.writeUsername(message.SenderName);
        //                    this.writeMessage(message.ContentMessage);
        //                }
        //                break;

        //            case ChatLib.MessageType.UpdateConnectedUsersInChatRoom:
        //                if (message.TargetName == chatRoomName)
        //                {
        //                    this.listClient.Items.Clear();
        //                    string[] listClient = message.ContentMessage.Split('\\');
        //                    foreach( string s in listClient)
        //                    {
        //                        ListViewItem item = new ListViewItem(s);
        //                        this.listClient.Items.Add(item);
        //                    }
                            
        //                }
        //                break;

        //            case ChatLib.MessageType.ClientPrivateMessage:
        //                break;
        //        }
        //    }
        //}

        private void listClient_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonPrivateChat_Click(object sender, EventArgs e)
        {
            if (this.listClient.SelectedItems.Count > 0)
            {
                ListViewItem item = this.listClient.SelectedItems[0];

                string clientPrivateName = item.Text;

                

                ChatLib.MessageChat message = new ChatLib.MessageChat(ChatLib.MessageType.PrivateChatCreate, this.client.UserName, clientPrivateName, null);
                ChatLib.ChatLib.SendMessage(client.ClientSocket, message);

            }
        }

        private void textBoxMessage_DragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void textBoxMessage_DragDrop(object sender, DragEventArgs e)
        {

            string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));

              

                foreach (string fileLoc in filePaths)
                {
                 
                        Image im = Image.FromFile(System.IO.Path.GetFullPath(fileLoc));
                        FileInfo fil = new FileInfo(fileLoc);
                        if (fil.Length > 100000)
                        {
                            MessageBox.Show("Le fichier exède 100ko");
                            return;
                        }
                       // im = resizeImage(im);
                        ChatLib.MessageChat msg = new ChatLib.MessageChat(ChatLib.MessageType.ClientChatRoomImage,client.UserName, this.chatRoomName, im,null);

                        ChatLib.ChatLib.SendMessage(client.ClientSocket, msg);
                        closingWindow = true;

                       

                    }

               

           

            


        }


        private Image resizeImage(Image im)
        {
            int tempX = 0;
            int tempY = 0;
            float ratio;
            if (im.Size.Width > im.Size.Height)
            {
                ratio = (float)((float)im.Size.Width / (float)im.Size.Height);
                tempX = 0;
                tempY = 0;

                tempX = maxWidth;
                tempY = (int)(tempX / ratio);

                while (tempY > maxHeight)
                {
                    tempX--;
                    tempY = (int)(tempX * ratio);

                }

            }
            else
            {

                ratio = ((float)im.Size.Width / (float)im.Size.Height);
                tempX = 0;
                tempY = 0;

                tempY = maxHeight;
                tempX = (int)(tempY * ratio);

                while (tempX > maxWidth)
                {
                    tempY--;
                    tempX = (int)(tempY * ratio);

                }


            }

            Bitmap newImage = new Bitmap(im, tempX, tempY);

            return newImage;

        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }


    }
}
