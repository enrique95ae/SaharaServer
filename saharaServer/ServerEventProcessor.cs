﻿using SaharaLib;
using ProtoBuf;
using System;
using System.Data.SQLite;
using Dapper;
using System.Linq;

//In this file we define the behavior of the server depending on the type of event going on. 


namespace SaharaServer
{


    public class ServerEventProcessor : BaseSingleton<ServerEventProcessor>
    {
        private const string _dbSource = "Data Source = /Users/enriquealonsoesposito/Desktop/SaharaDB.db";
        private readonly DatabaseManager _dbManager = DatabaseManager.Instance;
        private bool _processSuccess;
        private Connection _userData; 
        private readonly Object _lock = new object();

        public void Process(ref BaseEvent eventData, ref Connection userData)
        {
            _userData = userData;
            
            switch (eventData.Type)
            {
                case EventType.CreateAccount:
                    ProcessCreateAccount(eventData as CreateAccountEvent);
                    break;

                case EventType.Login:
                    ProcessLogin(eventData as LoginEvent);
                    break;

                /* Commented out until implemented
                case EventType.Logout:
                    ProcessLogout();
                    break;
                */

                case EventType.GetUserData:
                    ProcessGetAccountInfo(eventData as UserData);
                    break;

                case EventType.GetItemData:
                    ProcessGetItemData(eventData as ItemData);
                    break;

                case EventType.UpdateUserBillingInfo:
                    ProcessUpdateBillingInfo(eventData as BillingInfo);
                    break;

                case EventType.UpdateUserPaymentInfo:
                    ProcessUpdatePaymentInfo(eventData as PaymentInfo);
                    break;

                default:
                    return;
            }
        }

        private void ProcessCreateAccount(CreateAccountEvent newUserData)
        {
            Console.WriteLine("Processing CreateAccountEvent...");

            _processSuccess = _dbManager.CreateAccount(newUserData.UserEmail, newUserData.UserPassword);

            ServerReply(new ResponseEvent(_processSuccess));
        }

        private void ProcessLogin(LoginEvent loginData)
        {
            Console.WriteLine("Processing LoginEvent...");

            _processSuccess = _dbManager.VerifyLoginInfo(loginData._Email, loginData._Password);

            var email = loginData._Email;
            var password = loginData._Password;

            string sqlSelect = $"select UserPassword from UserData where UserEmail='{email}'";

            ServerReply(new ResponseEvent(_processSuccess));
            //ServerReply(new ResponseEvent(true));

        }

        /* Commented out until implemented
        private void ProcessLogout(LogoutEvent logoutData)
        {
            Console.WriteLine("Processing LoginEvent...");

            _processSuccess = _dbManager.VerifyLoginInfo(loginData._Email, loginData._Password);

            ServerReply(new ResponseEvent(_processSuccess));
        }
        */


        /*
            Process event functions:
            Prints to console and then makes database call and sends result to server
        */
        private void ProcessGetAccountInfo(UserData userData)
        {
            Console.WriteLine("Processing GetUserData Event...");

            userData = _dbManager.GetUserData(userData.UserEmail);

            ServerReply(userData);
        }

        private void ProcessGetItemData(ItemData itemData)
        {
            Console.WriteLine("Processing GetItemData Event...");

            itemData = _dbManager.GetItemData(itemData.ItemId);

            ServerReply(itemData);
        }

        private void ProcessUpdateBillingInfo(BillingInfo billingInfo)
        {
            Console.WriteLine("Processing UpdateBillingInfo Event...");

            billingInfo = _dbManager.UpdateBillingInfo(billingInfo.UserEmail);

            ServerReply(billingInfo);
        }

        private void ProcessUpdatePaymentInfo(PaymentInfo paymentInfo)
        {
            Console.WriteLine("Processing UpdatePaymentInfo Event...");

            paymentInfo = _dbManager.UpdatePaymentInfo(paymentInfo.UserCreditCardNumber);

            ServerReply(paymentInfo);
        }

        private void ServerReply<T>(T message)
        {
            try
            {
                Serializer.SerializeWithLengthPrefix(_userData.UserStream, message, PrefixStyle.Base128);

            }
            catch (Exception e)
            {
                Console.WriteLine("Caught exception when server was replying...");
                Console.WriteLine(e.GetType().Name);
            }
        }
    }
}