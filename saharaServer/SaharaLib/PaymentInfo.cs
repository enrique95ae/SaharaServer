using System;
using ProtoBuf;

/*
 * This file contains:
 *      -Object type definition for holding payment data.
 * 
 */

namespace SaharaLib
{
    [ProtoContract]
    public class PaymentInfo : UserData
    {
        [ProtoMember(1)]
        public string UserCreditCardNumber { get; set; }
        
        [ProtoMember(2)]
        public int? UserCreditCardCVC { get; set; }

        [ProtoMember(3)]
        public string UserCreditCardExpirationDate { get; set; }

        public PaymentInfo()
        {
            Type = EventType.UpdateUserPaymentInfo;
            UserCreditCardNumber = null;
            UserCreditCardCVC = null;
            UserCreditCardExpirationDate = null;
        }

        public PaymentInfo(string userCreditCardNumber, int userCreditCardCVC, string userCreditCardExpirationDate)
        {
            Type = EventType.UpdateUserPaymentInfo;
            UserCreditCardNumber = userCreditCardNumber;
            UserCreditCardCVC = userCreditCardCVC;
            UserCreditCardExpirationDate = userCreditCardExpirationDate;
        }
    }
}
