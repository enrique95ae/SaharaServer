using SaharaLib;
using ProtoBuf;
using System;

namespace SaharaServer
{
    public class ServerEventProcessor : BaseSingleton<ServerEventProcessor>
    {
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

                case EventType.GetUserData:
                    ProcessGetAccountInfo(eventData as UserData);
                    break;

                default:
                    return;
            }
        }

        private void ProcessCreateAccount(CreateAccountEvent newUserData)
        {
            Console.WriteLine("Processing CreateAccountEvent...");

            _processSuccess = _dbManager.CreateAccount(newUserData.UserName,newUserData.UserEmail, newUserData.UserPassword, newUserData.UserRepeatPassword);

            ServerReply(new ResponseEvent(_processSuccess));
        }

        private void ProcessLogin(LoginEvent loginData)
        {
            Console.WriteLine("Processing LoginEvent...");

            _processSuccess = _dbManager.VerifyLoginInfo(loginData._Email, loginData._Password);

            ServerReply(new ResponseEvent(_processSuccess));
        }

        private void ProcessGetAccountInfo(UserData userData)
        {
            Console.WriteLine("Processing GetUserData Event...");

            userData = _dbManager.GetUserData(userData.UserEmail);

            ServerReply(userData);
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