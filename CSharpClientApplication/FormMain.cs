using Newtonsoft.Json;
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

namespace CSharpClientApplication
{
    public partial class FormMain : Form
    {
        public static string rightClicked = null;
        public static Client client = null; // Objet Client associé

        public static String ipAddressServer = "127.0.0.1"; // Adresse IP par défaut du serveur
        public static int port = 8001; // Port par défaut du serveur

        private List<FormChatRoom> listFormChatRoom; // Listes des Chat Rooms ouvertes par cette instance client
        private List<FormChatPrivate> listFormChatPrivate;// Listes des Chat Rooms privées ouvertes par cette instance client

        private bool finish = false;

        private Thread t; // Thread de réception de cette instance client

        // Constructeur
        public FormMain()
        {
            // Initialisation des composants
            InitializeComponent();

            menuStrip_Update();

            initTreeViewChatRooms();
            initTreeViewClients();

            listFormChatRoom = new List<FormChatRoom>();
            listFormChatPrivate = new List<FormChatPrivate>();

            this.FormClosing += FormMain_FormClosing;// A la fermeture de la fenêtre
        }

        private void  FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // On coupe toutes les connexions et on ferme toutes les Chat Rooms ouvertes
            foreach (FormChatRoom chatRoom in this.listFormChatRoom)
            {
                chatRoom.Close();
            }

            foreach (FormChatPrivate privateChatRoom in this.listFormChatPrivate)
            {
                privateChatRoom.Close();
            }

            listFormChatRoom.Clear();
            listFormChatPrivate.Clear();

            this.treeViewChatRooms.Nodes[0].Nodes.Clear();
            this.treeViewClients.Nodes[0].Nodes.Clear();
            if (client != null)
            {
                // On previent le serveur de notre déconnexion
                ChatLib.MessageChat msg = new ChatLib.MessageChat(ChatLib.MessageType.DisconnectFromServer, client.UserName, "", "");
                ChatLib.ChatLib.SendMessage(client.ClientSocket, msg);
            }
        }

        public void receiveMessageFromServer()
        {
            // Boucle de réception des messages du serveur
            byte[] buf = new byte[ChatLib.ChatLib.MESSAGE_MAX_SIZE];
            ChatLib.MessageChat msg = null;

            while (true)
            {
                if (!finish)
                {
                    try
                    {
                        int nbBytes = client.ClientSocket.Receive(buf);

                        if (nbBytes > 0)
                        {
                            if (client.Connected && client.ClientSocket.Connected)
                            {
                                System.Diagnostics.Debug.WriteLine("FormMain received");

                                try
                                {
                                    msg = ChatLib.ChatLib.createMessageFromString(ChatLib.ChatLib.GetString(buf, nbBytes));
                                    System.Diagnostics.Debug.WriteLine(msg.MessageType);

                                    switch (msg.MessageType)// En fonction du type de message recu
                                    {
                                         
                                        case ChatLib.MessageType.ChatRoomExit:// Le serveur nous confirme que nous quittons la Chat Room
                                            if (msg.ContentMessage == "true")
                                            {
                                                this.Invoke((MethodInvoker)delegate
                                                {
                                                    listFormChatRoom.Remove(listFormChatRoom.Find(x => x.ChatRoomName == msg.TargetName));
                                                });
                                            }
                                            
                                            break;

                                        case ChatLib.MessageType.PrivateChatExit: // Le serveur nous confirme que nous quittons la Chat Room privée
                                            if (msg.ContentMessage == "true")
                                            {
                                                this.Invoke((MethodInvoker)delegate
                                                {
                                                    listFormChatPrivate.Remove(listFormChatPrivate.Find(x => x.PrivateChatName == msg.TargetName));
                                                });
                                            }
                                            break;

                                        case ChatLib.MessageType.ChatRoomJoin: // Le serveur nous confirme que nous nous connectons à une Chat Room
                                            if (msg.ContentMessage == "true")
                                            {
                                                this.Invoke((MethodInvoker)delegate
                                                {
                                                    if (!listFormChatRoom.Exists(x => x.ChatRoomName == msg.TargetName))
                                                    {
                                                        listFormChatRoom.Add(new FormChatRoom(msg.TargetName));
                                                    }

                                                    listFormChatRoom.Find(x => x.ChatRoomName == msg.TargetName).Show();
                                                });
                                            }
                                            break;

                                        
                                        case ChatLib.MessageType.ChatRoomCreate: // Le serveur nous confirme que nous avons créé une Chat Room
                                            if (msg.ContentMessage == "true")
                                            {
                                                this.Invoke((MethodInvoker)delegate
                                                {
                                                    if(!this.listFormChatRoom.Exists(x => x.ChatRoomName == msg.TargetName))
                                                    {

                                                        this.formNewRoom_OnCreateNewChatRoom(msg.TargetName);
                                                    }
                                                });
                                            }

                                            break;

                                        case ChatLib.MessageType.ClientChatRoomMessage: // A la reception d'un message
                                            // Verifier parmis toutes les Chat Rooms ouvertes dans lesquels le client est connecté
                                            // Transférer le message à cette chat room
                                            if (listFormChatRoom.Exists(x => x.ChatRoomName == msg.TargetName))
                                            {
                                                this.Invoke((MethodInvoker)delegate
                                                {
                                                    FormChatRoom chat = listFormChatRoom.Find(x => x.ChatRoomName == msg.TargetName);
                                                    chat.OnReceivedMessage(msg);
                                                });
                                            }

                                            break;

                                        case ChatLib.MessageType.ClientChatRoomImage: // A la reception d'un message de type image

                                            if (listFormChatRoom.Exists(x => x.ChatRoomName == msg.TargetName))
                                            {
                                                this.Invoke((MethodInvoker)delegate
                                                {
                                                    FormChatRoom chat = listFormChatRoom.Find(x => x.ChatRoomName == msg.TargetName);
                                                    chat.OnReceivedMessage(msg);
                                                });
                                            }

                                            
                                            break;

                                        case ChatLib.MessageType.UpdateConnectedUsersInChatRoom: // Le serveur nous informe que la liste des utilisateurs connectés à une Chat Room précise a été mise à jour

                                            if (listFormChatRoom.Exists(x => x.ChatRoomName == msg.TargetName))
                                            {
                                                this.Invoke((MethodInvoker)delegate
                                                {
                                                    FormChatRoom chat = listFormChatRoom.Find(x => x.ChatRoomName == msg.TargetName);
                                                    chat.UpdateConnectedUsersInChatRoom(msg);
                                                });
                                            }

                                            break;
                                        case ChatLib.MessageType.UpdateChatRoomList: // Le serveur nous indique que la liste des Chat Rooms a été mise à jour

                                            if (msg.ContentMessage != "false")
                                            {
                                                this.Invoke((MethodInvoker)delegate
                                                {
                                                    this.treeViewChatRooms.Nodes[0].Nodes.Clear(); // On vide la liste des Chat Rooms
                                                });

                                                foreach (string chatRoom in msg.ContentMessage.Split('\\'))
                                                {
                                                    // On réaffiche la liste des Chat Rooms
                                                    if (chatRoom.Length > 0)          
                                                    {
                                                        this.Invoke((MethodInvoker)delegate
                                                        {
                                                            this.formNewRoom_OnCreateNewChatRoom(chatRoom);
                                                        });
                                                    }

                                                }
                                               
                                            }
                                            if (msg.ContentMessage == "false")
                                            {
                                                MessageBox.Show("You can't delete this Chat Room while users are still in it.");
                                            }

                                            break;

                                        case ChatLib.MessageType.PrivateChatCreate: // Le serveur nous confirme la création d'une Chat Room privée
                                            
                                            FormChatPrivate privchat = new FormChatPrivate(msg.SenderName);
                                            if (!this.listFormChatPrivate.Exists(x => x.PrivateChatName == msg.SenderName))
                                            {
                                                this.Invoke((MethodInvoker)delegate
                                                {
                                                    this.listFormChatPrivate.Add(privchat);
                                                    this.listFormChatPrivate.Find(x => x.PrivateChatName == privchat.PrivateChatName).Show();
                                                });
                                            }
                                            break;


                                        case ChatLib.MessageType.ClientPrivateMessage: // A la réception d'un message privé

                                            if (listFormChatPrivate.Exists(x => x.PrivateChatName == msg.SenderName))
                                            {
                                                this.Invoke((MethodInvoker)delegate
                                                {
                                                    FormChatPrivate chat = listFormChatPrivate.Find(x => x.PrivateChatName == msg.SenderName);
                                                    chat.OnReceivedMessage(msg);
                                                });
                                            }
                                            break;
                                        case ChatLib.MessageType.ClientPrivateImage: // A la réception d'un message privé de type image

                                            if (listFormChatPrivate.Exists(x => x.PrivateChatName == msg.SenderName))
                                            {
                                                this.Invoke((MethodInvoker)delegate
                                                {
                                                    FormChatPrivate chat = listFormChatPrivate.Find(x => x.PrivateChatName == msg.SenderName);
                                                    chat.OnReceivedMessage(msg);
                                                });
                                            }
                                            break;
                                        case ChatLib.MessageType.UpdateConnectedUsers: // Le serveur nous indique que la liste des utlisateurs connectés a été mise à jour
                                            string[] users = msg.ContentMessage.Split('\\');

                                            this.Invoke((MethodInvoker)delegate
                                               {
                                                   this.treeViewClients.Nodes[0].Nodes.Clear(); // On vide la liste des utilisateurs connectés

                                                   foreach (string s in users)
                                                   {
                                                       System.Diagnostics.Debug.WriteLine(s);

                                                       if (s.Length > 0 && s != client.UserName)
                                                       {
                                                           this.treeViewClients.Nodes[0].Nodes.Add(new TreeNode(s));
                                                       }
                                                   }

                                                   this.treeViewClients.ExpandAll();
                                               });
                                            break;

                                        case ChatLib.MessageType.DisconnectFromServer: // Le serveur nous confirme notre déconnexion
                                            if (msg.ContentMessage == "true")
                                            {
                                                FormMain.client = null;

                                                finish = true;
                                                if (!this.IsDisposed)
                                                {
                                                    this.Invoke((MethodInvoker)delegate
                                                  {
                                                      this.Text = "Chat Application";
                                                      this.menuStrip_Update();
                                                  });
                                                }

                                                return;
                                            }
                                            break;

                                    }
                                }
                                catch (JsonReaderException)
                                {
                                }
                            }
                            else return;
                        }
                    }
                    catch (SocketException)
                    {
                    }
                }
                else return;
            }
        }

        public void initTreeViewChatRooms()
        {
            // Initialisation des évènements associés à notre liste de Chat Rooms
            this.treeViewChatRooms.NodeMouseDoubleClick += treeViewChatRooms_NodeMouseDoubleClick;
            this.treeViewChatRooms.NodeMouseClick += treeViewChatRooms_NodeMouseClick;
        }

        private void treeViewChatRooms_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)// lorsque l'on clique droit sur une Chat Room
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (e.Node.Level == 1)
                {
                    Console.WriteLine(rightClicked);
                    rightClicked = e.Node.Text;
                    rightClickChatRoomMenuStrip.Show(this, new Point(e.X, e.Y));
                }
            }
        }

        public void initTreeViewClients()
        {
            // Initialisation des évènements associés à notre liste d'utilisateurs disponibles
            this.treeViewClients.NodeMouseDoubleClick += treeViewClients_NodeMouseDoubleClick;
        }

        /***** GETTER *****/
        public bool Connected
        {
            get
            {
                if (FormMain.client != null)
                {
                    return true;
                }
                else return false;
            }
        }
        /******************/

        /***** TREE VIEW *****/
        private void treeViewChatRooms_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)// Lorsque l'on double clique sur une Chat Room, on s'y connecte
        {
            TreeNode treeNode = (TreeNode)e.Node;

            // Si c'est un chat room
            if (treeNode.Level == 1)
            {
                ChatLib.MessageChat msg = new ChatLib.MessageChat(ChatLib.MessageType.ChatRoomJoin, client.UserName, null, treeNode.Text);
                ChatLib.ChatLib.SendMessage(client.ClientSocket, msg);
            }
        }

        private void treeViewClients_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)// Lorsque l'on double clique sur un utilisateur, on lance une Chat Room privée
        {
            TreeNode treeNode = (TreeNode)e.Node;
            ChatLib.MessageChat msg = new ChatLib.MessageChat(ChatLib.MessageType.PrivateChatCreate, client.UserName, treeNode.Text,"");
            ChatLib.ChatLib.SendMessage(client.ClientSocket, msg);

        }

        private void createNewChatRoom(string chatRoomName) // Nous ajoutons une Chat Room à notre liste de Chat Rooms
        {
            TreeNode newTreeNode = new TreeNode(chatRoomName);

            this.treeViewChatRooms.Nodes[0].Nodes.Add(newTreeNode);
        }
        /******************/

        /***** MENU STRIP *****/
        private void quitToolStripMenuItem_Click(object sender, EventArgs e) // Lorsque que l'on clique sur quitter
        {
            // On coupe toutes les connexions
            foreach (FormChatRoom chatRoom in this.listFormChatRoom)
            {
                chatRoom.Close();
            }

            foreach (FormChatPrivate privateChatRoom in this.listFormChatPrivate)
            {
                privateChatRoom.Close();
            }

            listFormChatRoom.Clear();
            listFormChatPrivate.Clear();

            this.treeViewChatRooms.Nodes[0].Nodes.Clear();
            this.treeViewClients.Nodes[0].Nodes.Clear();

            if (client != null)
            {
                ChatLib.MessageChat msg = new ChatLib.MessageChat(ChatLib.MessageType.DisconnectFromServer, client.UserName, null, null);
                ChatLib.ChatLib.SendMessage(client.ClientSocket, msg);
            }
            System.Environment.Exit(0);
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)// Lorsque l'on clique sur Connect
        {
            if (!Connected)
            {
                if (FormMain.ipAddressServer != null || FormMain.port != -1)
                {
                    finish = false;
                    FormConnection formConnection = new FormConnection();
                    formConnection.OnCreateConnectingClient += formConnection_OnCreateConnectingClient;
                    formConnection.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please first configure the server.\nGo to Menu > Parameters > Server IP Address.");
                }
            }
            else
            {
                // On coupe toutes les connexions
                foreach (FormChatRoom chatRoom in this.listFormChatRoom)
                {
                    chatRoom.Close();
                }

                foreach (FormChatPrivate privateChatRoom in this.listFormChatPrivate)
                {
                    privateChatRoom.Close();
                }

                listFormChatRoom.Clear();
                listFormChatPrivate.Clear();

                this.treeViewChatRooms.Nodes[0].Nodes.Clear();
                this.treeViewClients.Nodes[0].Nodes.Clear();

                ChatLib.MessageChat msg = new ChatLib.MessageChat(ChatLib.MessageType.DisconnectFromServer, client.UserName, null, null);
                ChatLib.ChatLib.SendMessage(client.ClientSocket, msg);
            }

            menuStrip_Update();
        }

        private void newChatRoomToolStripMenuItem_Click(object sender, EventArgs e) // Lorsque l'on clique sur New Chat Room
        {
            FormNewRoom formNewRoom = new FormNewRoom();
            formNewRoom.ShowDialog();
        }

        /*********************/

        /***** DELEGATES *****/
        private void formConnection_OnCreateConnectingClient(Client client)
        {
            if (client != null)
            {
                FormMain.client = client;
                this.Text = FormMain.client.UserName + " - Chat Application";

                t = new Thread(new ThreadStart(receiveMessageFromServer));
                t.IsBackground = false;
                t.Start();
            }
            else
            {
                MessageBox.Show("Error while trying to connect");
            }
        }

        private void formNewRoom_OnCreateNewChatRoom(string chatRoomName)
        {
            if (chatRoomName != null)
            {
                createNewChatRoom(chatRoomName);

                listFormChatRoom.Add(new FormChatRoom(chatRoomName));

                this.treeViewChatRooms.ExpandAll();
            }
        }

        private void formIPServer_OnConfiguringServer(string ip, int port)
        {
            FormMain.ipAddressServer = ip;
            FormMain.port = port;
        }
        /********************/

        /***** VUES *****/
        private void menuStrip_Update() // Mise à jour de notre menu déroulant
        {
            if (Connected)
            {
                ToolStripMenuItem menuItem = (ToolStripMenuItem)MainMenuStrip.Items[0];
                foreach (ToolStripItem item in menuItem.DropDownItems)
                {
                    if (item.Name == "connectToolStripMenuItem")
                    {
                        item.Text = "Disconnect";
                    }
                    else if (item.Name == "newChatRoomToolStripMenuItem")
                    {
                        item.Visible = true;
                    }
                }
            }
            else
            {
                ToolStripMenuItem menuItem = (ToolStripMenuItem)MainMenuStrip.Items[0];
                foreach (ToolStripItem item in menuItem.DropDownItems)
                {
                    if (item.Name == "connectToolStripMenuItem")
                    {
                        item.Text = "Connect";
                    }
                    else if (item.Name == "newChatRoomToolStripMenuItem")
                    {
                        item.Visible = false;
                    }
                }
            }
        }

        private void serverIPAddressToolStripMenuItem_Click(object sender, EventArgs e)// En cliquant sur les paramètres du serveur
        {
            FormIPServer formIPServer = new FormIPServer(ipAddressServer, port);

            formIPServer.OnConfiguringServer += formIPServer_OnConfiguringServer;

            formIPServer.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) // Lorsque l'on clique droit sur une Chat Room puis Delete
        {
            ChatLib.MessageChat msg = new ChatLib.MessageChat(ChatLib.MessageType.ChatRoomDelete, client.UserName, rightClicked, null);
            ChatLib.ChatLib.SendMessage(client.ClientSocket, msg);
        }
        /****************/
    }
}
