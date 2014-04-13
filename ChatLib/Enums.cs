using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatLib
{
    public enum MessageType
    {
        None,
        ConnectToServer, // un client essaye de se connecter au serveur
        ChatRoomJoin, // un client essaye de joindre une chat room
        ChatRoomExit, // un client quitte une chat room
        PrivateChatExit, // un client quitte un chat privée
        PrivateChatJoin,
        ClientChatRoomMessage, // un client envoie un message à une chat room
        ClientChatRoomImage,
        ClientPrivateMessage, // un client envoie un message privé à un autre client
        ClientPrivateImage,
        PrivateChatCreate, // un client demande la création d'une conversation privée avec un autre client
        DisconnectFromServer, // un client se déconnecte du serveur
        ChatRoomDelete, // un client supprime une chat room
        ChatRoomCreate, // un client demande la creation d'un chat room
        UpdateConnectedUsers, // le serveur met à jour la liste des utilisateurs connectés
        UpdateConnectedUsersInChatRoom, // le serveur met à jour la liste des utilisateurs connectés au sein d'une chat room
        UpdateChatRoomList, // le serveur met à jour la liste des chat rooms
        ServerChatRoomMessage, // le serveur envoie le message envoyé par un client aux autres clients d'une chat room
        ServerChatRoomImage,
        ServerPrivateMessage, // le serveur envoie le message privé envoyé par un client au client dont le message est destiné
        ShutdownServer // le serveur s'arrête
    }
}
