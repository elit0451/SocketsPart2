using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GameServer
{
    class Client
    {
        NetworkStream ns;
        StreamReader reader;
        StreamWriter writer;
        Socket client;
        Game game;
        public Client(Socket clientS)
        {
            ns = new NetworkStream(clientS);
            reader = new StreamReader(ns);
            writer = new StreamWriter(ns);
            writer.AutoFlush = true;

            client = clientS;
            game = new Game();
        }

        public void Run()
        {
            bool rightGuess = true;

            game.AddClient(this);
            
            do
            {
                if(rightGuess == true)
                {
                    writer.WriteLine("Guess a number between 1 and 10.");
                }

                rightGuess = game.Guess(int.Parse(reader.ReadLine()));

                if (rightGuess == true)
                {
                    writer.WriteLine("Great! Just " + (10 - game.NumTries) + " guess. Do you want to try again? (y/n)");

                    if (reader.ReadLine() == "y")
                    {
                        game.Reset();
                    }
                }
                else
                {
                    if (game.NumTries == 0)
                    {
                        writer.WriteLine("You didn't manage to guess the right number (" + game.Number + "). Do you want to try again? (y/n)");
                        if (reader.ReadLine() == "y")
                        {
                            game.Reset();
                        }
                    }
                    else
                    {
                        writer.WriteLine("Wrong answer, you got " + game.NumTries + " more tries");
                    }
                }

            } while (game.GState == GameState.running);
        }
    }
}
