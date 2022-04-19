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
        public static bool[] columnFull = new bool[numColumns];
        public static char pLetter;
        public static int columnInserted = 0;

        public const int numColumns = 7;
        public const int numRows = 6;
        protected static char[,] Board = new char[numRows, numColumns];
        
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
            for(int i = 0; i < numRows; i++)
            {
                columnFull[i] = false;
                for (int j = 0; j < numColumns; j++)
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
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    Console.Write($"{Board[i, j]} ") ;
                }
                Console.WriteLine();
            }
        Console.WriteLine("0 1 2 3 4 5 6");
        }

        public void PutLetterInColumn(int columninserted, char letter)
        {
            int index = numRows - 1;
            char pl = Board[index, columninserted]; // pl for Player Letter
            while ((pl == 'X' || pl == 'O') && index >= 0)
            {
                index--;
                if (index >= 0) pl = Board[index, columninserted];
            }
            if (index < 0) columnFull[columninserted] = true;
            if (!columnFull[columninserted])
            {
                Board[index, columninserted] = letter;
                DisplayBoard();
            }
            else
            {
                Console.WriteLine("Column full! Choose another one.");
            }
        }

        public bool FullBoard()
        {
            bool boardFull = true;
            for (int i = 0; i < numRows;i++)
            {
                if (!columnFull[i])
                {
                    boardFull = false;
                }
                if (boardFull)
                {
                    DisplayBoard();
                }
            }
            return boardFull;
        }

        public static bool WinMethod(char letter)
        {
            bool winnerBool = false;

            //Horizontal lines
            for(int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i, j] == letter && Board[i, j + 1] == letter && Board[i, j + 2] == letter && Board[i, j + 3] == letter)
                    {
                        winnerBool = true;
                    }
                }
            }

            //Vertical lines
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0;j < 7; j++)
                {
                    if(Board[i, j] == letter && Board[i + 1, j] == letter && Board[i + 2, j] == letter && Board[i + 3, j] == letter)
                    {
                        winnerBool = true;
                    }
                }
            }

            return winnerBool;
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
            if (GetPlayerName(true) == PlayerOne)
            {
                pLetter = 'X';
            }
            else
            {
                pLetter = 'O';
            }

            do
            {
                string numInsert = Console.ReadLine();
                columnInserted = Convert.ToInt32(numInsert);

                if (columnInserted >= 0 && columnInserted <= 7)
                {

                    PutLetterInColumn(columnInserted, pLetter);

                    if (!columnFull[columnInserted])
                    {
                        pLetter = (pLetter == 'O' ? 'X' : 'O');
                    }
                    else if (WinMethod(pLetter)) // redundant method...?
                    {
                        Console.WriteLine("Player '{0}', with '{1}' symbol won the game!", base.GetPlayerName(), pLetter);
                        SetupANewGame();
                    }

                    if (FullBoard())
                    {
                        Console.WriteLine("It is a draw! Start the games again.");
                        SetupANewGame();
                    }

                }

            } while (true);



            //Board[5, 0] = 'A';
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
                    //Console.Clear();
                    //newGame.DisplayBoard();
                    //Console.ReadKey();

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
