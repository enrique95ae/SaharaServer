using System;
using ProtoBuf;

namespace SaharaLib
{
    [ProtoContract]
    public class AccountData : BaseEvent
    {
        [ProtoMember(1)]
        public string Tag { get; set; }

        [ProtoMember(2)]
        public string Email { get; set; }

        [ProtoMember(3)]
        public string Password { get; set; }

        public AccountData()
        {
            Type = EventType.GetAccountData;
            Tag = null;
            Email = null;
            Password = null;
        }

        public AccountData(string tag, string email, string password)
        {
            Type = EventType.GetAccountData;
            Tag = tag;
            Email = email;
            Password = password;

        }
    }
}