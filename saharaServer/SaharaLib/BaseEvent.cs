﻿using System;
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
        GetItemData,
        UpdateUserBillingInfo,
    }

    [ProtoContract]
    [ProtoInclude(50, typeof(CreateAccountEvent))]
    [ProtoInclude(51, typeof(LoginEvent))]
    [ProtoInclude(52, typeof(ResponseEvent))]
    [ProtoInclude(53, typeof(UserData))]
    [ProtoInclude(54, typeof(ItemData))]
    [ProtoInclude(55, typeof(/*CLASS HERE*/))]
    public class BaseEvent
    {
        [ProtoMember(1)]
        public EventType Type { get; set; }
    }
}