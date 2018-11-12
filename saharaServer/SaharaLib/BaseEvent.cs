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
        GetItemData,
        UpdateUserBillingInfo,
        UpdateUserPaymentInfo,
    }

    [ProtoContract]
    [ProtoInclude(50, typeof(CreateAccountEvent))]
    [ProtoInclude(51, typeof(LoginEvent))]
    [ProtoInclude(52, typeof(ResponseEvent))]
    [ProtoInclude(53, typeof(UserData))]
    [ProtoInclude(54, typeof(ItemData))]
    [ProtoInclude(55, typeof(BillingInfo))]
<<<<<<< HEAD

=======
    [ProtoInclude(56, typeof(PaymentInfo))]
>>>>>>> 65a0f6526a849d2a2ba5d664e2a39a53cea293a8
    public class BaseEvent
    {
        [ProtoMember(1)]
        public EventType Type { get; set; }
    }
}