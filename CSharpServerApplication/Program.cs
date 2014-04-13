using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Drawing;
using ChatLib;
using System.Threading;

namespace CSharpServerApplication
{
    class Program
    {
        public static int port = 8001;
        public static IPAddress ipServer = null;

        public static List<User> userList;
        public static List<ChatRoom> chatRoomList;
        public static List<ConnectedUser> connectedUserList;
        public static Socket serverSocket;
        static Semaphore semaphor = new Semaphore(1, 1);


        //Bind socket to server ip address and Listen to an available port
        public static Socket setServerSocket()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int choix = 0;

            IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());

            //On prend uniquement une adresse IPV4 (Ne marche pas pour les IPV 6 car le serverSocket est initialisé en mode InterNetwork, pas en InterNetworkV6)
            IPAddress ipAddress = null;
            foreach (IPAddress ipAdd in ipHostEntry.AddressList)
            {
                if (ipAdd.AddressFamily.Equals(AddressFamily.InterNetwork))
                {
                    ipAddress = ipAdd;
                }
            }

            IPEndPoint localEndPoint = null;
            do
            {
                Console.WriteLine("1. Local IP");
                Console.WriteLine("2. Automatic IP");
                Console.Write("Your choice : ");
                choix = int.Parse(Console.ReadLine());
                Console.Clear();

            } while (choix != 1 && choix != 2);

            switch (choix)
            {
                case 1:
                    ipAddress = IPAddress.Parse("127.0.0.1");
                    localEndPoint = new IPEndPoint(ipAddress, port);

                    break;
                case 2: localEndPoint = new IPEndPoint(ipAddress, port);
                    break;
            }

            ipServer = ipAddress;

            serverSocket.Bind(localEndPoint);

            serverSocket.Listen(50);

            System.Console.WriteLine("Listening to: " + ipAddress + ":" + port);

            return serverSocket;
        }

        public static void connectionEstablishment()
        {
            while (true)
            {
                Socket newSocket = null;

                newSocket = serverSocket.Accept();

                System.Console.WriteLine("Connection Accepted");


                if (newSocket != null)
                {
                    newSocket.Send(ChatLib.ChatLib.GetBytes("Connection Established"));
                    Thread socketConnection = new Thread(new ParameterizedThreadStart(launchNewSocketConnection));
                    socketConnection.Start(newSocket);
                }
            }
        }

        private static void sendMessage(object obj)
        {
            object[] objects = obj as object[];
            ChatLib.ChatLib.SendMessage(objects[0] as Socket, objects[1] as MessageChat);
        }

        private static String splitChatRoomList(List<ChatRoom> list)
        {
            String splittedList = "";

            foreach (ChatRoom chatRoom in list)
            {
                splittedList += chatRoom.name + "\\";
            }

            System.Diagnostics.Debug.WriteLine(splittedList);

            return splittedList;
        }

        private static String splitChatRoomUserList(List<ConnectedUser> list, string chatRoomName)
        {
            String splittedList = "";

            foreach (ConnectedUser user in list)
            {
                if (user.chatRoomList.Exists(x => x.name == chatRoomName))
                    splittedList += user.name + "\\";
            }

            System.Diagnostics.Debug.WriteLine(splittedList);

            return splittedList;
        }


        private static String splitConnectedUserList(List<ConnectedUser> list)
        {
            String splittedList = "";

            foreach (ConnectedUser user in list)
            {
                splittedList += user.name + "\\";
            }

            System.Diagnostics.Debug.WriteLine(splittedList);

            return splittedList;
        }


        private static void updateConnectedUserList()
        {
            foreach (ConnectedUser user in connectedUserList)
            {
                //envoie de la liste de chatRoom
                ChatLib.MessageChat msgChatRoomList = new ChatLib.MessageChat(MessageType.UpdateConnectedUsers, "server", user.name, splitConnectedUserList(connectedUserList));
                Console.WriteLine("send updateConnectedUser to : " + user.name + " " + user.userSocket.ToString());
                ChatLib.ChatLib.SendMessage(user.userSocket, msgChatRoomList);
            }
        }


        private static void updateChatRoomList()
        {
            foreach (ConnectedUser user in connectedUserList)
            {
                //envoie de la liste de chatRoom
                ChatLib.MessageChat msgChatRoomList = new ChatLib.MessageChat(MessageType.UpdateChatRoomList, "server", user.name, splitChatRoomList(chatRoomList));
                Console.WriteLine("send updateChatRoom to : " + user.name + " " + user.userSocket.ToString());
                ChatLib.ChatLib.SendMessage(user.userSocket, msgChatRoomList);
            }
        }

        private static void updateChatRoomUserList(string chatRoomName)
        {
            foreach (ConnectedUser user in connectedUserList)
            {
                if (user.chatRoomList.Exists(x => x.name == chatRoomName))
                {
                    //envoie de la liste de chatRoom
                    ChatLib.MessageChat msgChatRoomList = new ChatLib.MessageChat(MessageType.UpdateConnectedUsersInChatRoom, "server", chatRoomName, splitChatRoomUserList(connectedUserList, chatRoomName));
                    Console.WriteLine("send updateChatRoomUserList to : " + user.name + " " + user.userSocket.ToString());
                    ChatLib.ChatLib.SendMessage(user.userSocket, msgChatRoomList);
                    //envoie de la liste des utilisteurs connectés par chatRoom
                }
            }
        }



        private static void launchNewSocketConnection(object obj)
        {
            Socket newSocket = (Socket)obj;
            Thread socketThread;
            object[] objects;
            String chatRoomName;

            if (newSocket != null)
            {

                while (newSocket.Connected)
                {
                    byte[] MessageReceived = new byte[ChatLib.ChatLib.MESSAGE_MAX_SIZE];

                    try
                    {
                        // On attend de recevoir un msg provenant d'un client
                        int nbBytes = newSocket.Receive(MessageReceived);

                        if (newSocket.Connected)
                        {
                            // On recupere la requete envoye par un client
                            string msgFromClient = ChatLib.ChatLib.GetString(MessageReceived, nbBytes);
                            System.Diagnostics.Debug.WriteLine(msgFromClient);

                            // On caste la requete en message 
                            ChatLib.MessageChat msg = ChatLib.ChatLib.createMessageFromString(msgFromClient.Trim());

                            if (msg != null)
                            {

                                Console.WriteLine(msg.MessageType.ToString());

                                // Suivant le type de message 
                                switch (msg.MessageType)
                                {
                                    case MessageType.None:

                                        break;

                                    case MessageType.ClientPrivateMessage:
                                        semaphor.WaitOne();
                                        ConnectedUser target;

                                        target = connectedUserList.Find(x => x.name == msg.TargetName);
                                        semaphor.Release();
                                        Thread.Sleep(100);
                                        ChatLib.ChatLib.SendMessage(target.userSocket, msg);


                                        break;

                                    // Message texte
                                    case MessageType.ClientChatRoomMessage:
                                        semaphor.WaitOne();
                                        String targetChatRoom = msg.TargetName;

                                        foreach (ConnectedUser user in connectedUserList)
                                        {
                                            if (user.chatRoomList.Exists(x => x.name == targetChatRoom))
                                            {

                                                objects = new object[2] { user.userSocket, msg };
                                                semaphor.Release();
                                                socketThread = new Thread(new ParameterizedThreadStart(sendMessage));
                                                socketThread.Start(objects);
                                                semaphor.WaitOne();
                                            }
                                        }
                                        semaphor.Release();
                                        break;

                                    // un client se log
                                    case MessageType.ConnectToServer:
                                        semaphor.WaitOne();
                                        String userName = msg.SenderName;
                                        String userPassword = msg.ContentMessage;


                                        ChatLib.MessageChat msgLogIn = new ChatLib.MessageChat(MessageType.ConnectToServer, "server", msg.SenderName, "true");
                                        foreach (User usr in userList)
                                        {
                                            Console.WriteLine("user : " + usr.name + "pass : " + usr.password);
                                        }
                                        if (userList.Exists(x => x.name == userName))
                                        {
                                            //vérifier son mot de passe
                                            Console.WriteLine("user exists name log in : " + userName);
                                            if (userList.Find(x => x.name == userName).password.Equals(userPassword))
                                            {
                                                Console.WriteLine("user exists  pasword  : " + userPassword);


                                                if (connectedUserList.Exists(x => x.name == userName))
                                                {
                                                    if (connectedUserList.Find(x => x.name == userName).userSocket.Connected)
                                                    {
                                                        connectedUserList.Find(x => x.name == userName).userSocket.Disconnect(true);
                                                    }


                                                    connectedUserList.Find(x => x.name == userName).userSocket = newSocket;
                                                }
                                                else
                                                {
                                                    connectedUserList.Add(new ConnectedUser(userName, new List<ChatRoom>(), newSocket));
                                                }

                                                semaphor.Release();
                                                if (msgLogIn != null)
                                                {
                                                    ChatLib.ChatLib.SendMessage(newSocket, msgLogIn);

                                                    Thread.Sleep(100);

                                                    updateChatRoomList();

                                                    Thread.Sleep(100);

                                                    updateConnectedUserList();
                                                }
                                            }
                                            else
                                            {
                                                semaphor.Release();
                                                if (msgLogIn != null)
                                                {
                                                    msgLogIn.ContentMessage = "false";
                                                    ChatLib.ChatLib.SendMessage(newSocket, msgLogIn);
                                                }

                                                newSocket.Disconnect(true);
                                            }
                                        }
                                        else
                                        {
                                            //Sinon on ajoute l'utilisateur à la liste des utilisateurs connectés
                                            connectedUserList.Add(new ConnectedUser(userName, new List<ChatRoom>(), newSocket));
                                            userList.Add(new User(userName, userPassword));
                                            semaphor.Release();
                                            if (msgLogIn != null)
                                            {
                                                ChatLib.ChatLib.SendMessage(newSocket, msgLogIn);

                                                Thread.Sleep(100);

                                                updateChatRoomList();

                                                Thread.Sleep(100);

                                                updateConnectedUserList();
                                            }
                                        }

                                        break;

                                    // un client se déconnecte
                                    case MessageType.DisconnectFromServer:
                                        semaphor.WaitOne();
                                        ConnectedUser c = connectedUserList.Find(x => x.name == msg.SenderName);
                                        connectedUserList.Remove(connectedUserList.Find(x => x.name == msg.SenderName));
                                        semaphor.Release();
                                        ChatLib.MessageChat msgDisconnectOK = new ChatLib.MessageChat(MessageType.DisconnectFromServer, "server", msg.SenderName, "true");
                                        Thread.Sleep(100);

                                        updateConnectedUserList();

                                        Thread.Sleep(100);

                                        if (msgDisconnectOK != null)
                                        {
                                            ChatLib.ChatLib.SendMessage(newSocket, msgDisconnectOK);
                                        }
                                        Thread.Sleep(100);

                                        foreach (ChatRoom chat in c.chatRoomList)
                                        {
                                            updateChatRoomUserList(chat.name);
                                        }

                                        Thread.Sleep(100);

                                        newSocket.Disconnect(true);

                                        break;

                                    //un client rejoint la ChatRoom
                                    case MessageType.ChatRoomJoin:
                                        semaphor.WaitOne();
                                        chatRoomName = msg.ContentMessage;

                                        ChatLib.MessageChat msgJoinOK = new ChatLib.MessageChat(MessageType.ChatRoomJoin, msg.SenderName, chatRoomName, "true");

                                        if (connectedUserList.Find(x => x.name == msg.SenderName).chatRoomList.Exists(x => x.name == chatRoomName))
                                            msgJoinOK.ContentMessage = "false";
                                        else
                                        {
                                            if (!connectedUserList.Find(x => x.name == msg.SenderName).chatRoomList.Exists(x => x.name == chatRoomName))
                                            {
                                                connectedUserList.Find(x => x.name == msg.SenderName).chatRoomList.Add(new ChatRoom(chatRoomName));

                                            }
                                        }

                                        semaphor.Release();

                                        Console.WriteLine(msgJoinOK.ContentMessage);
                                        if (msgJoinOK != null)
                                        {
                                            ChatLib.ChatLib.SendMessage(newSocket, msgJoinOK);

                                            Thread.Sleep(100);
                                            updateChatRoomUserList(chatRoomName);
                                        }

                                        break;

                                    //un client quitte la chatRoom
                                    case MessageType.ChatRoomExit:
                                        semaphor.WaitOne();
                                        chatRoomName = msg.ContentMessage;

                                        connectedUserList.Find(x => x.name == msg.SenderName).chatRoomList.Remove(connectedUserList.Find(x => x.name == msg.SenderName).chatRoomList.Find(x => x.name == chatRoomName));
                                        Thread.Sleep(100);
                                        semaphor.Release();
                                        updateChatRoomUserList(chatRoomName);

                                        ChatLib.MessageChat msgExitOK = new ChatLib.MessageChat(MessageType.ChatRoomExit, msg.SenderName, chatRoomName, "true");

                                        ChatLib.ChatLib.SendMessage(newSocket, msgExitOK);

                                        break;

                                    //supprimer un chatRoom
                                    case MessageType.ChatRoomDelete:
                                        semaphor.WaitOne();
                                        chatRoomName = msg.TargetName;
                                        bool exist = false;
                                        foreach (ConnectedUser user in connectedUserList)
                                        {
                                            if (user.chatRoomList.Exists(x => x.name == chatRoomName))
                                            {
                                                semaphor.Release();
                                                //user.chatRoomList.Remove(user.chatRoomList.Find(x => x.name == chatRoomName));
                                                exist = true;
                                                ChatLib.MessageChat msgChatRoomList = new ChatLib.MessageChat(MessageType.UpdateChatRoomList, "server", msg.SenderName, "false");
                                                
                                                ChatLib.ChatLib.SendMessage(newSocket, msgChatRoomList);
                                                break;
                                            }
                                        }
                                        if (!exist)
                                        {
                                            semaphor.WaitOne();
                                            chatRoomList.Remove(chatRoomList.Find(x => x.name == chatRoomName));
                                            semaphor.Release();
                                            updateChatRoomList();
                                        }

                                        break;

                                    // un client créé un chat room
                                    case MessageType.ChatRoomCreate:
                                        
                                        chatRoomName = msg.ContentMessage;

                                        ChatLib.MessageChat msgCreateOK = new ChatLib.MessageChat(MessageType.ChatRoomCreate, msg.SenderName, chatRoomName, "true");
                                        semaphor.WaitOne();
                                        if (chatRoomList.Exists(x => x.name == chatRoomName))
                                        {
                                            if (msgCreateOK != null)
                                            {
                                                msgCreateOK.ContentMessage = "false";
                                                semaphor.Release();
                                            }
                                        }
                                        else
                                        {

                                            chatRoomList.Add(new ChatRoom(chatRoomName));
                                            semaphor.Release();

                                        }

                                        updateChatRoomList();
                                       
                                        ChatLib.ChatLib.SendMessage(newSocket, msgCreateOK);
                                        
                                        
                                        break;

                                    case MessageType.ClientPrivateImage:
                                        Console.WriteLine("PrivateImage processing");
                                        ConnectedUser targetPrivateImage = connectedUserList.Find(x => x.name == msg.TargetName);
                                        Thread.Sleep(100);
                                        ChatLib.ChatLib.SendMessage(targetPrivateImage.userSocket, msg);
                                        Thread.Sleep(400);
                                        ChatLib.ChatLib.SendMessage(newSocket, msg);
                                        break;
                                    case MessageType.PrivateChatCreate:
                                        chatRoomName = msg.TargetName;
                                        ChatLib.MessageChat msgPrivateCreateOK = new ChatLib.MessageChat(MessageType.PrivateChatCreate, msg.TargetName, msg.SenderName, "true");
                                        ChatLib.ChatLib.SendMessage(newSocket, msgPrivateCreateOK);
                                        ConnectedUser targetPrivateChat = connectedUserList.Find(x => x.name == chatRoomName);
                                        Thread.Sleep(100);
                                        ChatLib.ChatLib.SendMessage(targetPrivateChat.userSocket,new MessageChat(MessageType.PrivateChatCreate,msg.SenderName,msg.TargetName,"true"));
                                        break;
                                    case MessageType.PrivateChatExit:
                                        chatRoomName = msg.TargetName;

                                        ChatLib.MessageChat msgPrivateExitOK = new ChatLib.MessageChat(MessageType.PrivateChatExit, msg.SenderName, chatRoomName, "true");

                                        ChatLib.ChatLib.SendMessage(newSocket, msgPrivateExitOK);

                                        break;

                                    case MessageType.ClientChatRoomImage:
                                        String targetImage = msg.TargetName;

                                        foreach (ConnectedUser user in connectedUserList)
                                        {
                                            if (user.chatRoomList.Exists(x => x.name == targetImage))
                                            {
                                                

                                                objects = new object[2] { user.userSocket, msg };

                                                socketThread = new Thread(new ParameterizedThreadStart(sendMessage));
                                                socketThread.Start(objects);
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                        else break;
                    }
                    catch (SocketException e)
                    {
                        Console.WriteLine("erreur socket" + e.ToString());
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            connectedUserList = new List<ConnectedUser>();
            chatRoomList = new List<ChatRoom>();
            userList = new List<User>();

            serverSocket = setServerSocket();

            connectionEstablishment();
        }
    }
}
