using System;
using ProtoBuf;

namespace SaharaLib
{
    public enum EventType
    {
        CreateAccount,
        Login,
        Logout,
        Response,
        GetUserData,
    }

    [ProtoContract]
    [ProtoInclude(50, typeof(LoginEvent))]
    [ProtoInclude(51, typeof(CreateAccountEvent))]
    [ProtoInclude(52, typeof(ResponseEvent))]
    [ProtoInclude(53, typeof(UserData))]
    public class BaseEvent
    {
        [ProtoMember(1)]
        public EventType Type { get; set; }
    }
}