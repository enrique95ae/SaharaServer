using System;
using System.Collections.Generic;

namespace SaharaServer
{
    public class ServerManager : BaseSingleton<ServerManager>
    {
        public List<UserConnection> Connections { get; set; }

        private ServerManager()
        {
            Connections = new List<UserConnection>();
        }

        public void AddConnection(UserConnection newConnection)
        {
            Connections.Add(newConnection);
        }

        public UserData FindUser(string username)
        {
            var userData = Connections.Find(user => user.GetUserData.AccountData.UserName, Equals(username));
            return userData.GetUserData;
        }
    }
}