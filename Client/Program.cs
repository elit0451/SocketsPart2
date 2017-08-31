using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient server = new TcpClient("10.140.64.224", 11000);

            string serverMessage = "";

            while (serverMessage != "Hello client")
            {
                NetworkStream stream = server.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);

                writer.AutoFlush = true;

                Console.WriteLine("Message:");
                writer.WriteLine(Console.ReadLine());
                serverMessage = reader.ReadLine();
                

                Console.WriteLine("Server says: " + serverMessage);
            }

            Console.ReadKey();

        }
    }
}
