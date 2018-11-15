using ProtoBuf;
using System;

/*
 * This file contains:
 *      -Object type definition for holding the log out pertinent data.
 * 
 */

namespace SaharaLib
{
    [ProtoContract]
    public class LogoutEvent : BaseEvent
    {
        [ProtoMember(1)]
        public string _Email { get; set; }

        [ProtoMember(2)]
        public string _Password { get; set; }

        public LogoutEvent()
        {
            Type = EventType.Login;
            _Email = null;
            _Password = null;
        }

        public LogoutEvent(string email, string password)
        {
            Type = EventType.Login;
            _Email = email;
            _Password = password;
        }
    }
}