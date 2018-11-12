using System;
using ProtoBuf;

namespace SaharaLib
{
    [ProtoContract]
    public class UserData : BaseEvent
    {
        [ProtoMember(1)]
        public string UserEmail { get; set; }

        [ProtoMember(2)]
        public string UserPassword { get; set; }

        public UserData()
        {
            Type = EventType.GetUserData;
            UserEmail = null;
            UserPassword = null;
        }

        public UserData(string email , string password)
        {
            Type = EventType.GetUserData;
            UserEmail = email;
            UserPassword = password;

        }
    }
}