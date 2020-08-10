using System;
using System.Threading;

namespace Snake_Console_Game
{
    class Program
    {
        const int boundaryHeight = 40;
        const int boundaryWidth = 70;

        static void Main(string[] args)
        {
            int xPosition = 35;
            int yPosition = 20;
            int gameSpeed = 150;

            bool isGameOn = true;
            bool isBoundaryHit = false;

            Console.CursorVisible = false;
            Console.SetWindowSize(boundaryWidth + 5, boundaryHeight + 5);
            Console.SetCursorPosition(xPosition, yPosition);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine((char)2);

            BuildBoundary();

            ConsoleKey command = Console.ReadKey().Key;

            do
            {
                switch (command)
                {
                    case ConsoleKey.LeftArrow:
                        Console.SetCursorPosition(xPosition, yPosition);
                        Console.Write(" ");
                        xPosition--;
                        break;
                    case ConsoleKey.UpArrow:
                        Console.SetCursorPosition(xPosition, yPosition);
                        Console.Write(" ");
                        yPosition--;
                        break;
                    case ConsoleKey.RightArrow:
                        Console.SetCursorPosition(xPosition, yPosition);
                        Console.Write(" ");
                        xPosition++;
                        break;
                    case ConsoleKey.DownArrow:
                        Console.SetCursorPosition(xPosition, yPosition);
                        Console.Write(" ");
                        yPosition++;
                        break;


                    case ConsoleKey.A:
                        Console.SetCursorPosition(xPosition, yPosition);
                        Console.Write(" ");
                        xPosition--;
                        break;
                    case ConsoleKey.D:
                        Console.SetCursorPosition(xPosition, yPosition);
                        Console.Write(" ");
                        xPosition++;
                        break;
                    case ConsoleKey.S:
                        Console.SetCursorPosition(xPosition, yPosition);
                        Console.Write(" ");
                        yPosition++;
                        break;
                    case ConsoleKey.W:
                        Console.SetCursorPosition(xPosition, yPosition);
                        Console.Write(" ");
                        yPosition--;
                        break;
                }

                Console.SetCursorPosition(xPosition, yPosition);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine((char)2);

                isBoundaryHit = DidSnakeHitBoundary(xPosition, yPosition);

                if (isBoundaryHit)
                {
                    isGameOn = false;
                    Console.SetCursorPosition(28, 20);
                    Console.WriteLine("Game Over");
                }

                if (Console.KeyAvailable)
                {
                    command = Console.ReadKey().Key;
                }

                Thread.Sleep(gameSpeed);

            } while (isGameOn);


        }

        private static bool DidSnakeHitBoundary(int xPosition, int yPosition)
        {
            if (xPosition == 1 || xPosition == boundaryWidth || yPosition == 1 || yPosition == boundaryHeight)
            {
                return true;
            }

            return false;
        }

        private static void BuildBoundary()
        {


            for (int i = 1; i <= boundaryHeight; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(1, i);
                Console.Write('#');
                Console.SetCursorPosition(boundaryWidth, i);
                Console.Write('#');
            }
            for (int i = 1; i <= boundaryWidth; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(i, 1);
                Console.Write('#');
                Console.SetCursorPosition(i, boundaryHeight);
                Console.Write('#');
            }

        }
    }
}
