using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using SaharaLib;
using System;

namespace SaharaServer
{
    public class SaharaServer
    {
        private const int _port = 27015;
        private readonly TcpListener _listener;

        public SaharaServer()
        {
            _listener = new TcpListener(IPAddress.IPv6Any, _port);

            _listener?.Server?.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);
        }

        public async Task StartAndListenAsync()
        {
            _listener?.Start();

            
            Console.WriteLine("Server started. Listening for connections...");

            while (Globals.g_isRunning)
            {
                var tcpClient = await _listener.AcceptTcpClientAsync();
                var userData = new Connection(tcpClient);
                Console.WriteLine("Accepted connection");

                var newConnection = new UserConnection(userData);
                await Task.Factory.StartNew(() => newConnection?.HandleUserEvents(), TaskCreationOptions.LongRunning).ConfigureAwait(false);
            }
        }
    }
}