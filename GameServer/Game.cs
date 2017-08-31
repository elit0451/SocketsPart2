using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    enum GameState
    {
        running,
        over
    }
    class Game
    {
        List<Client> listOfClients;

        public int Number { get; private set; }
        public int NumTries { get; private set; }
        public GameState GState { get; private set; }

        public Game()
        {
            listOfClients = new List<Client>();

            Reset();
        }

        public void AddClient(Client client)
        {
            listOfClients.Add(client);
        }

        private int GenerateNumber()
        {
            Random random = new Random();
            return random.Next(1, 10);
        }

        public bool Guess(int guessedNum)
        {
            NumTries--;
            bool rightGuess = false;

            if(guessedNum == Number)
            {
                rightGuess = true;
                GState = GameState.over;
            }

            return rightGuess;
        }

        public void Reset()
        {
            Number = GenerateNumber();
            NumTries = 10;
            GState = GameState.running;

            Console.WriteLine(Number);
        }

        
    }
}
