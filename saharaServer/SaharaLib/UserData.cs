using System;
using ProtoBuf;

namespace SaharaLib
{
    [ProtoContract]
    public class UserData : BaseEvent
    {
        [ProtoMember(1)]
        public string UserName { get; set; }
         
        [ProtoMember(2)]
        public string UserEmail { get; set; }

        [ProtoMember(3)]
        public string UserPassword { get; set; }

        public UserData()
        {
            Type = EventType.GetUserData;
            UserName = null;
            UserEmail = null;
            UserPassword = null;
        }

        public UserData(string userName, string userEmail, string userPassword)
        {
            Type = EventType.GetUserData;
            UserName = userName;
            UserEmail = userEmail;
            UserPassword = userPassword;

        }
    }
}