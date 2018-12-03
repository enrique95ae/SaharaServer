using System;
using SaharaLib;
using ProtoBuf;

//Handles the user connection.

namespace SaharaServer
{
    public class UserConnection
    {
        private Connection                          _userData;
        private readonly Object                     _lock = new object();
        private readonly ServerEventProcessor       _serverEventProcessor;

        public Connection GetUserData { get => _userData; }

        //user connection constructor
        public UserConnection()
        {
            _userData = new  Connection();
            _serverEventProcessor = ServerEventProcessor.Instance;
        }

        //user connection with data
        public UserConnection(Connection userData)
        {
            if(userData != null)
            {
                _userData = userData;
            }
            else
            {
                _userData = new Connection();
            }

            _serverEventProcessor = ServerEventProcessor.Instance;
        }

        //While a connection exists, attempts to Read and Process event
        public void HandleUserEvents()
        {
            try
            {
                while(Globals.g_isRunning)
                {
                    var eventData = Serializer.DeserializeWithLengthPrefix<BaseEvent>(_userData.UserStream, PrefixStyle.Base128);

                    Console.WriteLine($"Read event of type: {eventData.GetType().Name}");

                    //Mutex lock on each event process
                    lock (_lock)
                    {
                        _serverEventProcessor.Process(ref eventData, ref _userData);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"Caught an exception of type {e.GetType()}");
                Console.WriteLine(e.ToString());
            }
        }
    }
}
