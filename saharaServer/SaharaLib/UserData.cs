using System.Net.Sockets;

namespace SaharaLib
{
    public class UserData
    {
        public AccountData AccountData { get; set; }
        public TcpClient TcpClient { get; set; }
        public NetworkStream ClientStream { get; set; }

        public UserData()
        {
            AccountData = new AccountData();
            TcpClient = null;
            ClientStream = null;
        }

        public UserData(TcpClient tcpClient)
        {
            AccountData = new AccountData();
            TcpClient = tcpClient;
            ClientStream = tcpClient.GetStream();
        }

        public UserData(AccountData accountData, TcpClient tcpClient)
        {
            AccountData = accountData;
            TcpClient = tcpClient;
            ClientStream = tcpClient.GetStream();
        }
    }
}