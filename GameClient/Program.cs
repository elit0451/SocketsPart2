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
            TcpClient server = new TcpClient("localhost", 11000);

            NetworkStream stream = server.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);

            writer.AutoFlush = true;

            string serverMessage = "";

            while (serverMessage != "Goodbye")
            {

                serverMessage = reader.ReadLine();
                Console.WriteLine(serverMessage);

                writer.WriteLine(Console.ReadLine());
            }

            Console.ReadKey();
        }
    }
}
