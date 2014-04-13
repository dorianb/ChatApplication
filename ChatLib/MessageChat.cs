using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ChatLib
{
    public class MessageChat
    {
        private MessageType messageType = MessageType.None;
        private string senderName = null;
        private string targetName = null;
        private string content = null;
        private Image image = null;

        public MessageChat() { }

        /*
         * Format :  
         *         {
                        "type":"Message",
                        "senderName":"client1",
                        "targetName":"client2",
                        "content":"blablabla"
                    }
         */

        public MessageChat(MessageType type, string name, string target, string content)
        {
            this.messageType = type;
            this.senderName = name;
            this.targetName = target;
            this.content = content;
        }
        public MessageChat(MessageType type, string name, string target, Image im, string content)
        {
            this.messageType = type;
            this.senderName = name;
            this.targetName = target;
            this.content = ChatLib.GetString(ChatLib.getBytesFromImage(im));
        }

        public MessageChat(string msg)
        {

        }

        public string SenderName
        {
            get { return senderName; }
            set { senderName = value; }
        }
        
        public MessageType MessageType
        {
            get { return messageType; }
            set { messageType = value; }
        }

         public string TargetName
        {
            get { return targetName; }
            set { targetName = value; }
        }

        public string ContentMessage
        {
            get { return content; }
            set { content = value; }
        }
    }
}
