using System;
using System.Threading;
using System.Threading.Tasks;


//Starts the listening for connections.

namespace SaharaServer
{
    public static class Program
    {
        public static void Main()
        {
            var server = new SaharaServer();

            Task.Factory.StartNew(() => server.StartAndListenAsync(),
                                  TaskCreationOptions.LongRunning);

            while(Globals.g_isRunning)
            {
                if(Console.KeyAvailable)
                {
                    Globals.g_isRunning = false;
                    break;
                }

                Console.Write(".");

                Thread.Sleep(1000);
            }
        }
    }
}