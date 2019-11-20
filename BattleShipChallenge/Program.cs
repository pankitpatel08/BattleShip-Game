using System;

namespace BattleShipChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Battleship btlGame = new Battleship();
            int guessHor, guessVer;
            bool isIntegerHor, isIntegerVer;

            btlGame.ShipPosition();
            Console.WriteLine("Welcome to Battleship Challenge ... Find your ship!");
            Console.WriteLine("\nEnter two co-ordinates between 1 to 8 following by Enter Key");
            Console.WriteLine("From Board: \n{0}", btlGame.shipBoard);

            do
            {
                // If more than 20 guesses then ends the game
                if (btlGame.countHit >= 20)
                {
                    btlGame.invalid = true;
                    btlGame.invalidMessage = "You haven't got a hit! Better luck next time!";
                    break;
                }

                isIntegerHor = Int32.TryParse(Console.ReadLine(), out guessHor);
                isIntegerVer = Int32.TryParse(Console.ReadLine(), out guessVer);

                // Check whether input values are integer or not
                if (isIntegerHor && (guessHor >= 1 && guessHor <= 8) && isIntegerVer && (guessVer >= 1 && guessVer <= 8))
                {
                    btlGame.HitPosition(guessHor, guessVer);
                    btlGame.countHit++;
                }
                else
                {
                    // If no integer values then ends the game with invalid input message
                    btlGame.invalid = true;
                    btlGame.invalidMessage = "That's not a valid numbers!";
                    break;
                }

                if (!btlGame.isShipHit)
                {
                    if (btlGame.nearestCorStatus != string.Empty)
                        Console.WriteLine(btlGame.nearestCorStatus);
                    else
                        Console.WriteLine("Better luck next time!");
                }
            }
            while (!btlGame.isShipHit);

            // Check Wether input is invalid or check the total count reaches 20
            if (!btlGame.invalid)
            {
                Console.WriteLine("\n\nFrom Board: \n{0}", btlGame.shipBoard);
                Console.WriteLine("WOW! You've got a hit and that ship is removed from the board at position {0} & {1}", btlGame.xCor, btlGame.yCor);
            }
            else
            {
                Console.WriteLine(btlGame.invalidMessage);
            }
            Console.WriteLine("Press any key to Exit");
            Console.ReadKey();
        }
    }

    class Battleship
    {
        public int xCor, yCor;
        public string shipBoard = @"| 1 | 2 | 3 |
| 4 | 5 | 6 |
| 7 | 8 |";
        public bool isShipHit = false;
        public string nearestCorStatus = "";
        public int countHit = 0;
        public bool invalid = false;
        public string invalidMessage = "";

        //Choose Random position of the Ship
        public void ShipPosition()
        {
            Random randomNumber = new Random();
            xCor = randomNumber.Next(1, 9); // random number between 1 and 8
            yCor = randomNumber.Next(1, 9);
        }

        public void HitPosition(int hitXcor, int hitYcor)
        {
            if (hitXcor == xCor && hitYcor == yCor)
            {
                // If found the Cordinates then replaced with X and Y
                shipBoard = shipBoard.Replace(hitXcor.ToString(), "X");
                shipBoard = shipBoard.Replace(hitYcor.ToString(), "Y");
                isShipHit = true;
            }
            else
            {
                int number = 0;

                int xCellAway = xCor - hitXcor;
                number = xCellAway < 0 ? -xCellAway : xCellAway;

                int yCellAway = yCor - hitYcor;
                number += yCellAway < 0 ? -yCellAway : yCellAway;

                //Check the how far the co-ordinates are from the ship
                if (number <= 2)
                    nearestCorStatus = "hot";
                else if (number > 2 && number <= 4)
                    nearestCorStatus = "warm";
                else
                    nearestCorStatus = "cold";
            }
        }
    }
}