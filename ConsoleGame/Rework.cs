using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleGame
{
    class Rework
    {
        struct player
        {
            string playerSymbol;
            ConsoleColor playerColor;
            int playerLives;
            int playerScore;
            int playerPositionX;
            int playerPositionY;
        }

        struct food
        {
            int foodCoordX;
            int foodCoordY;
            string[] foodSymbols = new string[5] { "@", "$", "&", "%", "*" };
            int[] foodScore = new int[5] { 2, 4, 6, 8, 10 };
        }
    }
}
