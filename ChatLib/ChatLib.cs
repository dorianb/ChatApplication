using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;


namespace ChatLib
{

    public class ChatLib
    {
        // Taille max d'un message en bytes
        public static int MESSAGE_MAX_SIZE = 30000;

        // Deserialization d'un message au format JSON
        public static MessageChat createMessageFromString(string str)
        {
            MessageChat msg = JsonConvert.DeserializeObject<MessageChat>(str);

            return msg;
        }

        // Convertit une string (ici le JSON) en bytes pour faire transiter l'info via le socket
        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        // Convertit des bytes en string
        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        // Convertit des bytes en string.
        // Length represente le nombre de bytes récupéré par le socket
        public static string GetString(byte[] bytes, int length)
        {
            length++;

            char[] chars = new char[length / sizeof(char)];

            if (length > 0)
                System.Buffer.BlockCopy(bytes, 0, chars, 0, length - 1);

            return new string(chars);
        }

        // Convertit une image en bytes
        public static byte[] getBytesFromImage(Image im)
        {
            MemoryStream ms = new MemoryStream();

            im.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        // Permet de restreindre l'accès aux ressources
        static Semaphore semaphor = new Semaphore(1, 1);

        // Envoi d'un message au socket donné. Effectuer dans un autre thread
        public static void SendMessage(Socket socket, MessageChat msg)
        {
            BackgroundWorker bwSender = new BackgroundWorker();
            bwSender.DoWork += new DoWorkEventHandler(bworkerSender);
            bwSender.RunWorkerAsync(new object[] { socket, msg });
        }

        private static void bworkerSender(object sender, DoWorkEventArgs e)
        {
            object[] obj = e.Argument as object[];

            Socket socket = obj[0] as Socket;
            MessageChat msg = obj[1] as MessageChat;

            // Bloque la ressource
            semaphor.WaitOne();

            string json = JsonConvert.SerializeObject(msg);
            try
            {
                socket.Send(GetBytes(json));
            }
            catch (SocketException)
            {
                Console.WriteLine("SocketException dans bworkerSender");
            }

            // Libere la ressource
            semaphor.Release();
        }
    }
}
