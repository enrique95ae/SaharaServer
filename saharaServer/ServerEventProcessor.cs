using SaharaLib;
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

            //Email and Password verification to be implemented
          /*  if(newUserData.UserEmail != null && newUserData.UserPassword != null)
            {
                _processSuccess = true; 
            }
            else
            {
                _processSuccess = false;
            } */

            ServerReply(new ResponseEvent(_processSuccess));
        }

        private void ProcessLogin(LoginEvent loginData)
        {
            Console.WriteLine("Processing LoginEvent...");

            //debug
            // _processSuccess = _dbManager.VerifyLoginInfo(loginData._Email, loginData._Password);

            var email = loginData._Email;
            var password = loginData._Password;

            string sqlSelect = $"select UserPassword from UserData where UserEmail='{email}'";


            using (var connection = new SQLiteConnection(_dbSource))
            {
                connection.Open();

                    var selectedPassword = connection.Query<string>(sqlSelect).First();

                    Console.WriteLine($"Trying to find {email}'s password: {password}");
                    Console.WriteLine($"Found password: {selectedPassword}");

                if (selectedPassword == password)   
                {
                    Console.WriteLine("Passwords were equal");
                    ServerReply(new ResponseEvent(true));
                }
                else
                {
                    ServerReply(new ResponseEvent(false));
                }
            } 

            //ServerReply(new ResponseEvent(_processSuccess));
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

            itemData = _dbManager.GetItemData(itemData.ItemName);

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