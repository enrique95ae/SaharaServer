using SaharaLib;
using ProtoBuf;
using System;

namespace SaharaServer
{
    public class ServerEventProcessor : BaseSingleton<ServerEventProcessor>
    {
        private readonly DatabaseManager _dbManager = DatabaseManager.Instance;
        private bool _processSuccess;
        private UserData _userData;

        public void Process(ref BaseEvent eventData, ref UserData userData)
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

                case EventType.GetAccountData:
                    ProcessGetAccountInfo(eventData as AccountData);
                    break;

                default:
                    return;
            }
        }

        private void ProcessCreateAccount(CreateAccountEvent newAccountData)
        {
            Console.WriteLine("Processing CreateAccountEvent...");

            _processSuccess = _dbManager.CreateAccount(newAccountData.Email, newAccountData.Password);

            ServerReply(new ResponseEvent(_processSuccess));
        }

        private void ProcessLogin(LoginEvent loginData)
        {
            Console.WriteLine("Processing LoginEvent...");

            _processSuccess = _dbManager.VerifyLoginInfo(loginData._Email, loginData._Password);

            ServerReply(new ResponseEvent(_processSuccess));
        }

        private void ProcessGetAccountInfo(AccountData accountData)
        {
            Console.WriteLine("Processing GetAccountData Event...");

            accountData = _dbManager.GetAccountData(accountData.Email);

            ServerReply(accountData);
        }

        private void ServerReply<T>(T message)
        {
            try
            {
                Serializer.SerializeWithLengthPrefix(_userData.ClientStream, message, PrefixStyle.Base128);

            }
            catch (Exception e)
            {
                Console.WriteLine("Caught exception when server was replying...");
                Console.WriteLine(e.GetType().Name);
            }
        }
    }
}