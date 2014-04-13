using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib
{
    public class ConnectedUser
    {
        public String name;
        public List<ChatRoom> chatRoomList;
        public Socket userSocket;

        public ConnectedUser() 
        {
        
        }

        public ConnectedUser(String user, List<ChatRoom> chatRoomList, Socket userSocket)
        {
            this.name = user;
            this.chatRoomList = chatRoomList;
            this.userSocket = userSocket;
        }

    }
}
