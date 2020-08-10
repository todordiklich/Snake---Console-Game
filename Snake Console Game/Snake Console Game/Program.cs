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
            Random random = new Random();

            int[] xPosition = new int[50];
            xPosition[0] = random.Next(2, boundaryWidth - 2); ;
            int[] yPosition = new int[50];
            yPosition[0] = random.Next(2, boundaryHeight - 2); ;

            int appleXDim = 10;
            int appleYDim = 10;
            int applesEaten = 0;

            decimal gameSpeed = 150m;

            bool isGameOn = true;
            bool isBoundaryHit = false;
            bool isAppleEaten = false;


            Console.CursorVisible = false;
            Console.SetWindowSize(boundaryWidth + 5, boundaryHeight + 5);

            //Initial place of the snake on the board
            RenderSnake(applesEaten, xPosition, yPosition, out xPosition, out yPosition);

            //Initial place of an apple on the board
            SetApplePosition(random, out appleXDim, out appleYDim);
            RenderApple(appleXDim, appleYDim);

            //Create boundary
            BuildBoundary();

            //Move the snake
            ConsoleKey command = Console.ReadKey().Key;
            do
            {
                switch (command)
                {
                    case ConsoleKey.LeftArrow:
                        Console.SetCursorPosition(xPosition[0], yPosition[0]);
                        Console.Write(" ");
                        xPosition[0]--;
                        break;

                    case ConsoleKey.UpArrow:
                        Console.SetCursorPosition(xPosition[0], yPosition[0]);
                        Console.Write(" ");
                        yPosition[0]--;
                        break;

                    case ConsoleKey.RightArrow:
                        Console.SetCursorPosition(xPosition[0], yPosition[0]);
                        Console.Write(" ");
                        xPosition[0]++;
                        break;

                    case ConsoleKey.DownArrow:
                        Console.SetCursorPosition(xPosition[0], yPosition[0]);
                        Console.Write(" ");
                        yPosition[0]++;
                        break;
                }

                //Render the snake
                RenderSnake(applesEaten, xPosition, yPosition, out xPosition, out yPosition);

                isBoundaryHit = DidSnakeHitBoundary(xPosition[0], yPosition[0]);

                //Check if snake hits a boundary
                if (isBoundaryHit)
                {
                    isGameOn = false;
                    Console.SetCursorPosition(28, 20);
                    Console.WriteLine("Game Over");
                }

                //Check if apple is eaten
                isAppleEaten = DetermineIfAppleWasEaten(xPosition[0], yPosition[0], appleXDim, appleYDim);

                //Place an apple
                if (isAppleEaten)
                {
                    SetApplePosition(random, out appleXDim, out appleYDim);
                    RenderApple(appleXDim, appleYDim);
                    applesEaten++;
                    gameSpeed *= 0.925m;
                }

                //Check if direction of the snake has beed changed
                if (Console.KeyAvailable)
                {
                    command = Console.ReadKey().Key;
                }

                Thread.Sleep((int)gameSpeed);

            } while (isGameOn);
        }

        private static void RenderSnake(int applesEaten, int[] xPositionIn, int[] yPositionIn, out int[] xPositionOut, out int[] yPositionOut)
        {
            //Render the head
            Console.SetCursorPosition(xPositionIn[0], yPositionIn[0]);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine((char)2);

            //Render the body
            for (int i = 1; i < applesEaten + 1; i++)
            {
                Console.SetCursorPosition(xPositionIn[i], yPositionIn[i]);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("o");
            }

            //Erase last part of the body
            Console.SetCursorPosition(xPositionIn[applesEaten + 1], yPositionIn[applesEaten + 1]);
            Console.WriteLine(" ");

            //Record position of each body part
            for (int i = applesEaten + 1; i > 0; i--)
            {
                xPositionIn[i] = xPositionIn[i - 1];
                yPositionIn[i] = yPositionIn[i - 1];
            }

            xPositionOut = xPositionIn;
            yPositionOut = yPositionIn;
        }

        private static bool DetermineIfAppleWasEaten(int xPosition, int yPosition, int appleXDim, int appleYDim)
        {
            if (xPosition == appleXDim && yPosition == appleYDim)
            {
                return true;
            }

            return false;
        }

        private static void SetApplePosition(Random random, out int appleXDim, out int appleYDim)
        {
            appleXDim = random.Next(2, boundaryWidth - 2);
            appleYDim = random.Next(2, boundaryHeight - 2);
        }

        private static void RenderApple(int appleXDim, int appleYDim)
        {
            Console.SetCursorPosition(appleXDim, appleYDim);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write((char)64);
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
