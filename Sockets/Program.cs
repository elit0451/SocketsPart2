using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Sockets
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 11000);
            listener.Start();

            Socket client = null;

            while (true)
            {
                client = listener.AcceptSocket();
                ThreadPool.QueueUserWorkItem(ThreadProc, client);
            }
        }

        private static void ThreadProc(object obj)
        {
            Socket client = (Socket)obj;

            Console.WriteLine("A client just connected.");

            while (client.Connected == true)
            {
                NetworkStream stream = new NetworkStream(client);
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);

                writer.AutoFlush = true;

                string clientMessage = reader.ReadLine();
                if (clientMessage != "")
                {
                    Console.WriteLine(client.RemoteEndPoint + " says \"" + clientMessage + "\"");
                    string[] words = clientMessage.Split(' ');

                    switch (words[0])
                    {
                        case "Hello,server":
                            writer.WriteLine("Hello client");
                            break;
                        case "time?":
                            writer.WriteLine(DateTime.Now.TimeOfDay);
                            break;
                        case "date":
                            writer.WriteLine(DateTime.Today.Date);
                            break;
                        case "add":
                            writer.WriteLine("sum " + (int.Parse(words[1]) + int.Parse(words[2])));
                            break;
                        case "sub":
                            writer.WriteLine("difference " + (int.Parse(words[1]) - int.Parse(words[2])));
                            break;
                        case "exit":
                            writer.WriteLine("Goodbye!");
                            Console.WriteLine(client.RemoteEndPoint + " just disconnected!");
                            client.Disconnect(true);
                            
                            break;

                        default:
                            writer.WriteLine("Unknown command");
                            break;
                    }
                }
            }
        }
    }
}
