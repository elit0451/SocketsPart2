using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient server = new TcpClient("10.140.66.126", 11000);

            string serverMessage = "";

            while (serverMessage != "Goodbye")
            {
                NetworkStream stream = server.GetStream();
                StreamReader reader = new StreamReader(stream);
                StreamWriter writer = new StreamWriter(stream);

                writer.AutoFlush = true;

                serverMessage = reader.ReadLine();
                Console.WriteLine(serverMessage);

                writer.WriteLine(Console.ReadLine());
            }

            Console.ReadKey();
        }
    }
}
