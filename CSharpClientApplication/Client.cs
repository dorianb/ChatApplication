using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CSharpClientApplication
{
    public class Client
    {
        private string mUserName;
        private string mPassword;

        private Socket mClientSocket;
       // private NetworkStream mNetworkStream;
        private IPEndPoint mServerEndPoint;

        public Client() { }

        public Client(IPEndPoint server, string user, string pass)
        {
            this.mServerEndPoint = server;
            UserName = user;
            Password = pass;
        }

        //public NetworkStream NetworkStream
        //{
        //    get { return this.mNetworkStream; }
        //    set { this.mNetworkStream = value; }
        //}

        public bool Connected
        {
            get
            {
                if (ClientSocket != null)
                {
                    return this.ClientSocket.Connected;
                }
                else
                {
                    return false;
                }
            }
        }

        public IPAddress ClientIP
        {
            get
            {
                if (this.Connected)
                    return ((IPEndPoint)this.mClientSocket.LocalEndPoint).Address;
                else
                    return IPAddress.None;
            }
        }

        public IPAddress ServerIP
        {
            get
            {
                if (this.Connected)
                    return this.mServerEndPoint.Address;
                else
                    return IPAddress.None;
            }
        }

        public int ServerPort
        {
            get
            {
                if (this.Connected)
                    return this.mServerEndPoint.Port;
                else
                    return -1;
            }
        }

        public Socket ClientSocket
        {
            get { return this.mClientSocket; }
            set { this.mClientSocket = value; }
        }

        public string UserName
        {
            get { return this.mUserName; }
            set { this.mUserName = value; }
        }

        public string Password
        {
            get { return this.mPassword; }
            set { this.mPassword = value; }
        }
    }
}
