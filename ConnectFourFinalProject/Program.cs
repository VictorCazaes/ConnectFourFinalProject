using System;

namespace ConnectFourFinalProject
{
    public class Player
    {
        protected string PlayerName;
        protected int Wins { get; set; }

        public Player(string playerName, int playerNum)
        {
            if(playerName == "")
            {
                PlayerName = $"Player-{playerNum}";
            } else
            {
                PlayerName = playerName;
            }
        }

        public virtual string GetPlayerName()
        {
            return PlayerName;
        }
    }
    public abstract class Game
    {
        protected static void SetupANewGame()
        {

        }
        public virtual void Reset()
        {
            SetupANewGame();
        }
    }
    public class Controller
    {

    }
    public class GameInformation
    {

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
                    Console.WriteLine("Type the desired name for player one or press enter to use the default name");
                    string player1Name = Console.ReadLine();
                    Console.Clear();
                    Console.WriteLine("Type the desired name for player two or press enter to use the default name");
                    string player2Name = Console.ReadLine();
                    Player test1 = new Player(player1Name, 1);
                    Player test2 = new Player(player2Name, 2);
                    Console.Clear();
                    Console.WriteLine("Players:");
                    Console.WriteLine(test1.GetPlayerName());
                    Console.WriteLine(test2.GetPlayerName());
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Unknown command!");
                    continue;
                }
            } while (true);
        }
    }
}
