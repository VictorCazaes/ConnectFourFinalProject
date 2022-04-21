﻿using System;
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

        public virtual string GetPlayerName()
        {
            return PlayerOne;
        }
    }

    public abstract class GameBase : Player
    {
        protected static int Turn;
        private string PlayerTwo;
        public static bool[] columnFull = new bool[numColumns];
        public static char PlayerLetter;
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
            Turn = 2;
            PlayerLetter = 'O';
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

        protected static void PlayerTurn()
        {
            Turn = Turn == 2 ? 1 : 2;
        }

        protected static void GetPlayerLetter()
        {
            PlayerLetter = (PlayerLetter == 'O' ? 'X' : 'O');
        }

        public override string GetPlayerName()
        {
            if (Turn == 2) return PlayerTwo;
            return base.GetPlayerName();

        }

        public virtual void DisplayBoard()
        {
            for (int i = 0; i < numRows; i++)
            {
                Console.Write('|');
                for (int j = 0; j < numColumns; j++)
                {
                    Console.Write($"{Board[i, j]} ") ;
                }
                Console.Write('|');
                Console.WriteLine();
            }
            Console.WriteLine(" 1 2 3 4 5 6 7");
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
            }
            else
            {
                Console.WriteLine("Column full! Choose another one.");
                Thread.Sleep(3 * 1000);
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
            for(int row = 0; row < 6; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    if (Board[row, column] == letter && Board[row, column + 1] == letter && Board[row, column + 2] == letter && Board[row, column + 3] == letter)
                    {
                        winnerBool = true;
                    }
                }
            }

            //Vertical lines
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 7; column++)
                {
                    if (Board[row, column] == letter && Board[row + 1, column] == letter && Board[row + 2, column] == letter && Board[row + 3, column] == letter)
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

        protected static void RepeatPlayerTurn()
        {
            PlayerTurn();
            GetPlayerLetter();
        }

        public override void Play()
        {
            do
            {
                Console.Clear();
                DisplayBoard();

                //setting the next player
                PlayerTurn();
                GetPlayerLetter();
                Console.WriteLine();
                Console.WriteLine($"Now it's {GetPlayerName()} turn.");

                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int columnInserted))
                {
                    columnInserted -= 1;
                    if (columnInserted >= 0 && columnInserted <= 6)
                    {
                        PutLetterInColumn(columnInserted, PlayerLetter);

                        if (columnFull[columnInserted])
                        {
                            RepeatPlayerTurn();
                        }
                        else if (WinMethod(PlayerLetter))
                        {
                            Console.Clear();
                            DisplayBoard();
                            Console.WriteLine();
                            Console.WriteLine("Player '{0}', with '{1}' symbol won the game!", GetPlayerName(), PlayerLetter);
                            break;
                            //SetupANewGame();
                        }

                        if (FullBoard())
                        {
                            Console.WriteLine("It is a draw! Start the games again.");
                            SetupANewGame();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Out of range! choose a column between 1 and 7");
                        Thread.Sleep(3 * 1000);
                        RepeatPlayerTurn();
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input! choose a column between 1 and 7");
                    Thread.Sleep(3 * 1000);
                    RepeatPlayerTurn();
                }

            } while (true);

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
                    Console.Clear();
                    Game newGame = new Game(player1Name, player2Name);
                    //Console.WriteLine(newGame.GetPlayerName(false));
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
