using ProtoBuf;
using System;

namespace SaharaLib
{
    [ProtoContract]
    public class LoginEvent : BaseEvent
    {
        [ProtoMember(1)]
        public string _Email { get; set; }

        [ProtoMember(2)]
        public string _Password { get; set; }

        public LoginEvent()
        {
            Type = EventType.Login;
            _Email = null;
            _Password = null;
        }

        public LoginEvent(string email, string password)
        {
            Type = EventType.Login;
            _Email = email;
            _Password = password;
        }
    }
}