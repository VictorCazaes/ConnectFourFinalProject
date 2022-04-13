using System;
using System.Threading;

namespace ConnectFourFinalProject
{
    public abstract class Player
    {
        protected string PlayerOne;

        public Player(string playerName)
        {
            if(playerName == "")
            {
                PlayerOne = "Player-1";
            } else
            {
                PlayerOne = playerName;
            }
        }

        public virtual string GetPlayerName(bool win)
        {
            return PlayerOne;
        }
    }
    public abstract class GameBase : Player
    {
        protected static int turn;
        private string PlayerTwo;
        protected static char[,] Board = new char[6,7];

        public GameBase(string playerOneName, string playerTwoName) : base(playerOneName)
        {
            if (playerTwoName == "")
            {
                PlayerTwo = "Player-2";
            }
            else
            {
                PlayerTwo = playerTwoName;
            }
        }
        protected static void SetupANewGame()
        {
            turn = 0;
            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 7; j++)
                {
                    Board[i, j] = '*';
                }
            }
        }
        public virtual void Reset()
        {
            SetupANewGame();
        }

        public abstract void Play();
        public virtual int PlayerTurn()
        {
            return turn % 2 + 1;
        }

        public override string GetPlayerName(bool win = false)
        {
            if (win == true) turn--;
            if (turn % 2 == 1)
                return PlayerTwo;
            return base.GetPlayerName(win);
        }

        public virtual void DisplayBoard()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Console.Write($"{Board[i, j]} ") ;
                }
                Console.WriteLine();
            }
        }
    }
    public class GameController : GameBase
    {
        public GameController(string playerOneName, string playerTwoName) : base(playerOneName, playerTwoName)
        {
            SetupANewGame();
        }

        public override void Play()
        {
            Board[0, 0] = 'A';
        }

        public override string GetPlayerName(bool win)
        {
            return base.GetPlayerName(win);
        }
 
    }
    public class Game : GameController
    {
        public Game(string playerOneName, string playerTwoName) : base(playerOneName, playerTwoName)
        {
            SetupANewGame();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Press enter to start or type 'exit' to exit the game");
                string choice = Console.ReadLine();
                if(choice == "exit")
                {
                    break;
                } 
                else if(choice == "")
                {
                    Console.Clear();
                    Console.WriteLine("Type the nickname for player one or press enter to use the default name");
                    string player1Name = Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Type the nickname for player two or press enter to use the default name");
                    string player2Name = Console.ReadLine();
                    Game newGame = new Game(player1Name, player2Name);
                    Console.WriteLine(newGame.GetPlayerName(false));
                    newGame.DisplayBoard();
                    newGame.Play();
                    newGame.DisplayBoard();

                }
                else
                {
                    Console.Clear();
                    Console.Write("Unknown command! ");
                    continue;
                }
            } while (true);
        }
    }
}
