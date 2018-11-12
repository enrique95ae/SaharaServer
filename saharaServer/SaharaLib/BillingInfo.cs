using System;
using ProtoBuf;

namespace SaharaLib
{
    [ProtoContract]
    public class BillingInfo : UserData
    {
        [ProtoMember(1)]
        public string UserAddressLine1 { get; set; }

        [ProtoMember(2)]
        public string UserAddressLine2 { get; set; }

        [ProtoMember(3)]
        public int? UserCity { get; set; }

        [ProtoMember(4)]
        public string UserState { get; set; }

        [ProtoMember(5)]
        public int? UserZipCode { get; set; }

        public BillingInfo()
        {
            Type = EventType.UpdateUserBillingInfo;
            UserAddressLine1 = null;
            UserAddressLine2 = null;
            UserCity = null;
            UserZipCode = null;
            UserState = null;
        }

        public BillingInfo(string ccNumber, string addLine1, string addLine2, int zipCode, string state)
        {
            Type = EventType.UpdateUserBillingInfo;
            UserAddressLine1 = addLine1;
            UserAddressLine2 = addLine2;
            UserZipCode = zipCode;
            UserState = state;
        }
    }
}