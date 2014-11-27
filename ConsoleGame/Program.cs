using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

namespace ConsoleGame
{
    class Program
    {
        #region Variables

        public static string playerSymbol = "()_()";
        public static int playerLivesLeft = 3;
        public static int playerScore = 0;
        public static int movesLeft = 60;

        public static int consoleWidth = Console.WindowWidth;
        public static int consoleHeight = Console.WindowHeight;

        public static int centerOfConsoleWidth = consoleWidth / 2;
        public static int centerOfConsoleHeight = consoleHeight / 2;

        public static int playerPositionX = centerOfConsoleWidth;
        public static int playerPositionY = centerOfConsoleHeight;

        public static string[] foodSymbols = new string[5] { "@", "$", "&", "%", "*" };
        public static int[] foodScore = new int[5] { 2, 4, 6, 8, 10 };

        public static int foodCoordX;
        public static int foodCoordY;

        public static int generatedFoodSymbolIndex;
        public static int generatedFoodScoreIndex;

        #endregion

        static void init()
        {
            Console.Title = "Console Game";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetBufferSize(consoleWidth, consoleHeight);
            
            printPlayer(playerPositionX, playerPositionY);
            printScore();
            generateFood();
        }

        static void printPlayer(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(playerSymbol);
        }

        static void getPlayerMovement()
        {
            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
            keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                playerPositionY--;
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                playerPositionY++;
            }
            else if (keyInfo.Key == ConsoleKey.LeftArrow)
            {
                playerPositionX--;
            }
            else if (keyInfo.Key == ConsoleKey.RightArrow)
            {
                playerPositionX++;
            }
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }

            Console.Clear();
            checkPlayerPosition();
            printScore();
            printPlayer(playerPositionX, playerPositionY);
        }

        static void checkPlayerPosition()
        {
            if (playerPositionX < 0)
            {
                playerPositionX = consoleWidth - 1;
            }

            if (playerPositionX > consoleWidth - 1)
            {
                playerPositionX = 0;
            }

            if (playerPositionY < 0)
            {
                playerPositionY = consoleHeight - 1;
            }

            if (playerPositionY > consoleHeight - 1)
            {
                playerPositionY = 0;
            }
        }

        static void printFood(int x, int y, string foodSymbol)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(foodSymbol);
        }

        static void generateFood()
        {
            Random random = new Random();
            foodCoordX = random.Next(0, consoleWidth);
            foodCoordY = random.Next(0, consoleHeight);

            generatedFoodSymbolIndex = random.Next(0, foodSymbols.Length);
            generatedFoodScoreIndex = random.Next(0, foodScore.Length);
        }

        static void checkIfPlayerHitFoodCoord()
        {
            if (playerPositionX == foodCoordX && playerPositionY == foodCoordY)
            {
                playerScore += foodScore[generatedFoodScoreIndex];
                movesLeft += foodScore[generatedFoodScoreIndex];
                generateFood();
            }
        }

        static void checkLiveScore()
        {
            if (movesLeft == 0)
            {
                playerLivesLeft--;
                movesLeft = 60;
            }

            if (playerScore > 15)
            {
                playerScore = 0;
                playerLivesLeft+=1;
            }

            if (playerLivesLeft == 0)
            {
                Console.Clear();
                Console.SetCursorPosition(centerOfConsoleWidth, centerOfConsoleHeight);
                Console.WriteLine("You lose! Score: {0}", playerScore);
                Thread.Sleep(2000);
                Environment.Exit(0);
            }
        }

        static void printScore()
        {
            Console.SetCursorPosition(centerOfConsoleWidth - 14, 0);
            Console.WriteLine("Moves: {0} Lives: {1} Score: {2}", movesLeft, playerLivesLeft, playerScore);
        }

        static void Main(string[] args)
        {
            init();

            while (true)
            {
                getPlayerMovement();
                movesLeft--;
                checkIfPlayerHitFoodCoord();
                checkLiveScore();
                printFood(foodCoordX, foodCoordY, foodSymbols[generatedFoodSymbolIndex]);
            }
        }
    }
}
