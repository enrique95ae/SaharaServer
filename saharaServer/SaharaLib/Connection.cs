using System.Net.Sockets;


/*
 * This file contains:
 *  
 *  -Connection class to hold the pertinent data
 *  -Constructors
 * 
 * 
 * 
 */

namespace SaharaLib
{
    public class Connection
    {
        public UserData UserData { get; set; }
        public TcpClient TcpClient { get; set; }
        public NetworkStream UserStream { get; set; }

        public Connection()
        {
            UserData = new UserData();
            TcpClient = null;
            UserStream = null;
        }

        public Connection(TcpClient tcpClient)
        {
            UserData = new UserData();
            TcpClient = tcpClient;
            UserStream = tcpClient.GetStream();
        }

        public Connection(UserData userData, TcpClient tcpClient)
        {
            UserData = userData;
            TcpClient = tcpClient;
            UserStream = tcpClient.GetStream();
        }
    }
}