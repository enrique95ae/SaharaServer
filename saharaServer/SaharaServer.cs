using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SaharaServer
{
    public class SaharaServer
    {
        private const int _port = 27015;
        private readonly TcpListener _listener;

        public SaharaServer()
        {
            _listener = new TcpListener(IPAddress.IPv6Any, _port);

            _listener?.server?.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);
        }

        public async Task StartAndListenAsync()
        {
            _listener?.Start();

            Console.WriteLine("Server started. Listening for connections...");

            while (GlobalProxySelection.g_isRunning)
            {
                var tcpClient = await _listener.AcceptTcpClientAsync();
                var clientData = new ClientData(tcpClient);
                Console.WriteLine("Accepted connection");

                var newConnection = new ClientConnection(clientData);

                await Task.Factory.StartNew(() => newConnection?.HandleClientEvents(), TaskCreationOptions.LongRunning).ConfigureAwait(false);
            }
        }
    }
}