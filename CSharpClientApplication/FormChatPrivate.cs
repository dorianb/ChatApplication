using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpClientApplication
{
    public partial class FormChatPrivate : Form
    {
        private volatile bool closingWindow = false;
        private Client client = null;
        private string colleagueName = null;

        public FormChatPrivate(string nameSecondClient)
        {
            InitializeComponent();

            colleagueName = nameSecondClient;

            this.Text = colleagueName + " - Private Chat";

            client = FormMain.client;

            this.FormClosing += FormChatPrivate_FormClosing;
            this.richTextBoxChat.LinkClicked += richTextBoxChat_LinkClicked;
            this.textBoxMessage.AllowDrop = true;
            
        }

        private void richTextBoxChat_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        public string PrivateChatName
        {
            get { return this.colleagueName; }
            set { this.colleagueName = value; }
        }

        private void FormChatPrivate_FormClosing(object sender, FormClosingEventArgs e)
        {
            ChatLib.MessageChat msg = new ChatLib.MessageChat(ChatLib.MessageType.PrivateChatExit, client.UserName, this.colleagueName, null);

            ChatLib.ChatLib.SendMessage(client.ClientSocket, msg);

            closingWindow = true;
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string msgToSend = this.textBoxMessage.Text;

            ChatLib.MessageChat msg = new ChatLib.MessageChat(ChatLib.MessageType.ClientPrivateMessage, client.UserName, this.colleagueName, msgToSend);

            ChatLib.ChatLib.SendMessage(client.ClientSocket, msg);

            this.writeUsername(client.UserName);
            this.writeMessage(msgToSend);

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

        public void OnReceivedMessage(ChatLib.MessageChat message)
        {
            // Suivant le type de message 
            switch (message.MessageType)
            {
                case ChatLib.MessageType.None: break;

                case ChatLib.MessageType.ClientPrivateMessage:
                    if (message.SenderName == PrivateChatName)
                    {
                        this.writeUsername(message.SenderName);
                        this.writeMessage(message.ContentMessage);
                    }
                    break;
                case ChatLib.MessageType.ClientPrivateImage:
                    try
                    {


                        if (message.SenderName == PrivateChatName)
                        {

                            this.writeUsername(message.SenderName);
                            this.drawImage(message.ContentMessage);
                        }
                    }
                    catch (JsonReaderException)
                    {
                        MessageBox.Show("La taille de l'image excède 100ko");
                    }
                    break;
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
                ChatLib.MessageChat msg = new ChatLib.MessageChat(ChatLib.MessageType.ClientPrivateImage ,client.UserName, this.PrivateChatName, im,null);

                ChatLib.ChatLib.SendMessage(client.ClientSocket, msg);

                this.writeUsername(msg.SenderName);
                this.drawImage(msg.ContentMessage);

                closingWindow = true;



            }


        }

        private Image resizeImage(Image im)
        {
            int tempX = 0;
            int tempY = 0;
            int maxHeight=200;
            int maxWidth=200;
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

        private void drawImage(string message)
        {


            Image im;
           
            MemoryStream ms = new MemoryStream(ChatLib.ChatLib.GetBytes(message));
            im = Image.FromStream(ms);

            im = resizeImage(im);

            Clipboard.SetDataObject(im);

            Console.WriteLine("je suis laaaaa");
            this.richTextBoxChat.ReadOnly = false;
            if (this.richTextBoxChat.CanPaste(DataFormats.GetFormat(DataFormats.Bitmap)))
            {
                
                Console.WriteLine("Je colle l'image!");
                this.richTextBoxChat.Paste(DataFormats.GetFormat(DataFormats.Bitmap));

            }
            this.richTextBoxChat.ReadOnly = true;

            this.richTextBoxChat.SelectedText += Environment.NewLine;




        }
    }
}
