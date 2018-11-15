using System;
using ProtoBuf;

/*
 * This file contains:
 *      -Object type definition for holding the user pertinen data.
 * 
 */

namespace SaharaLib
{
    [ProtoContract]
    public class UserData : BaseEvent
    {
        [ProtoMember(1)]
        public string UserEmail { get; set; }

        [ProtoMember(2)]
        public string UserPassword { get; set; }

        [ProtoMember(3)]
        public string UserName { get; set; }

        public UserData()
        {
            Type = EventType.GetUserData;
            UserName = null;
            UserEmail = null;
            UserPassword = null;
        }

        public UserData(string name, string email , string password)
        {
            Type = EventType.GetUserData;
            UserName = name;
            UserEmail = email;
            UserPassword = password;
        }
    }
}