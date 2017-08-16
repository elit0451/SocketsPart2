using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer
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

                Random randomNum = new Random();
                int pickedNum = randomNum.Next(1, 10);
                Console.WriteLine(pickedNum);
                bool isRunning = true;

                int numTries = 10;

                do
                {
                    if (numTries == 10)
                    {
                        writer.WriteLine("Guess a number between 1 and 10");
                    }
                    else
                    {
                        writer.WriteLine("Wrong answer, you got " + numTries + " more tries");
                    }
                    numTries--;

                    string clientMessage = reader.ReadLine();
                    if (clientMessage != "")
                    {
                        if (clientMessage == "exit")
                        {
                            isRunning = false;
                        }
                        else if (numTries != 0)
                        {
                            if (int.Parse(clientMessage) == pickedNum)
                            {
                                writer.WriteLine("Great! Just " + (10 - numTries) + " guess. Do you want to try again? (y/n)");
                                clientMessage = reader.ReadLine();

                                if (clientMessage == "y")
                                {
                                    numTries = 10;
                                    pickedNum = randomNum.Next(1, 10);
                                }
                                else
                                {
                                    isRunning = false;
                                }
                            }
                        }
                        else
                        {
                            writer.WriteLine("You didn't manage to guess the right number (" + pickedNum + "). Do you want to try again? (y/n)");
                            clientMessage = reader.ReadLine();

                            if (clientMessage == "y")
                            {
                                numTries = 10;
                                pickedNum = randomNum.Next(1, 10);
                            }
                            else
                            {
                                isRunning = false;
                            }
                        }

                    }
                } while (isRunning == true);

                writer.WriteLine("Goodbye");
                client.Disconnect(false);
            }

        }
    }
}
