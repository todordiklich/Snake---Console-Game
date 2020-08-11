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
            #region Vars
            Random random = new Random();

            int[] xPosition = new int[50];
            xPosition[0] = random.Next(2, boundaryWidth - 2); ;
            int[] yPosition = new int[50];
            yPosition[0] = random.Next(2, boundaryHeight - 2); ;

            int appleXDim = 10;
            int appleYDim = 10;
            int applesEaten = 0;

            decimal gameSpeed = 150m;

            string userAction = "";

            bool isGameOn = true;
            bool isBoundaryHit = false;
            bool isAppleEaten = false;
            bool isUserInMenu = true;


            Console.CursorVisible = false;
            Console.SetWindowSize((boundaryWidth + 5), (boundaryHeight + 2));
            #endregion

            // Choose direction
            ShowMenu(out userAction);

            do
            {
                switch (userAction)
                {
                    #region Case Directions
                    case "1":
                    case "d":
                    case "directions":
                        Console.Clear();
                        BuildBoundary();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(5, 5);
                        Console.WriteLine("1) Resize the console window, so you can see all.");
                        Console.SetCursorPosition(5, 6);
                        Console.WriteLine(" 4 sides of playing field border.");
                        Console.SetCursorPosition(5, 7);
                        Console.WriteLine("2) Use arrows to move the snake.");
                        Console.SetCursorPosition(5, 8);
                        Console.WriteLine("3) The game will over if the snake hits a wall.");
                        Console.SetCursorPosition(5, 9);
                        Console.WriteLine("4) Collect apples to gain points.");
                        Console.SetCursorPosition(5, 11);
                        Console.WriteLine("Press enter to return to the main menu.");
                        Console.ReadLine();
                        Console.Clear();
                        ShowMenu(out userAction);
                        break;
                    #endregion

                    #region Case Play
                    case "2":
                    case "p":
                    case "play":
                        Console.Clear();
                        #region Game setup
                        //Initial place of the snake on the board
                        RenderSnake(applesEaten, xPosition, yPosition, out xPosition, out yPosition);

                        //Initial place of an apple on the board
                        SetApplePosition(random, out appleXDim, out appleYDim);
                        RenderApple(appleXDim, appleYDim);

                        //Create boundary
                        BuildBoundary();

                        ConsoleKey command = Console.ReadKey().Key;

                        #endregion

                        //Move the snake
                        do
                        {
                            #region Change Direction
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
                            #endregion

                            #region Playing Logic
                            //Render the snake
                            RenderSnake(applesEaten, xPosition, yPosition, out xPosition, out yPosition);

                            isBoundaryHit = DidSnakeHitBoundary(xPosition[0], yPosition[0]);

                            //Check if snake hits a boundary
                            if (isBoundaryHit)
                            {
                                isGameOn = false;
                                Console.SetCursorPosition(28, 20);
                                Console.WriteLine("Game Over");

                                Console.ForegroundColor = ConsoleColor.White;
                                Console.SetCursorPosition(15, 21);
                                Console.Write($"Your score is {applesEaten * 100}!");
                                Console.SetCursorPosition(15, 22);
                                Console.WriteLine("Press enter to continue");
                                applesEaten = 0;
                                Console.ReadLine();
                                Console.Clear();

                                ShowMenu(out userAction);
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
                            #endregion

                        } while (isGameOn);
                        break;
                    #endregion

                    #region Case exit
                    case "3":
                    case "e":
                    case "exit":
                        isUserInMenu = false;
                        Console.Clear();
                        break;
                    #endregion

                    default:
                        Console.WriteLine("Invalid operation, please press enter and try again.");
                        Console.ReadLine();
                        Console.Clear();
                        ShowMenu(out userAction);
                        break;
                }
            } while (isUserInMenu);
        }

        #region Methods

        #region Menu
        private static void ShowMenu(out string userAction)
        {
            string menu1 = "1) Directions\n2) Play\n3) Exit \n\n\n" + @"


           /^\/^\
         _|__|  O|
\/     /~     \_/ \
 \____|__________/  \
        \_______      \
                `\     \                 \
                  |     |                  \
                 /      /                    \
                /     /                       \\
              /      /                         \ \
             /     /                            \  \
           /     /             _----_            \   \
          /     /           _-~      ~-_         |   |
         (      (        _-~    _--_    ~-_     _/   |
          \      ~-____-~    _-~    ~-_    ~-_-~    /
            ~-_           _-~          ~-_       _-~
               ~--______-~                ~-___-~
";
            string menu2 = "1) Directions\n2) Play\n3) Exit \n\n\n" + @"

                  
                 /^\/^\
               _|__|  O|
       /     /~     \_/ \
       \____|__________/  \
              \_______      \
                      `\     \                 \
                        |     |                  \
                       /      /                    \
                      /     /                       \\
                    /      /                         \ \
                   /     /                            \  \
                 /     /             _----_            \   \
                /     /           _-~      ~-_         |   |
               (      (        _-~    _--_    ~-_     _/   |
                \      ~-____-~    _-~    ~-_    ~-_-~    /
                  ~-_           _-~          ~-_       _-~
                     ~--______-~                ~-___-~
";
            string menu3 = "1) Directions\n2) Play\n3) Exit \n\n\n" + @"
            
                     
                      /^\/^\
                    _|__|  O|
            /     /~     \_/ \
            \____|__________/  \
                   \_______      \
                           `\     \                 \
                             |     |                  \
                            /      /                    \
                           /     /                       \\
                         /      /                         \ \
                        /     /                            \  \
                      /     /             _----_            \   \
                     /     /           _-~      ~-_         |   |
                    (      (        _-~    _--_    ~-_     _/   |
                     \      ~-____-~    _-~    ~-_    ~-_-~    /
                       ~-_           _-~          ~-_       _-~
                          ~--______-~                ~-___-~
";
            string menu4 = "1) Directions\n2) Play\n3) Exit \n\n\n" + @"
     
                 
                /^\/^\
              _|__|  O|
     \/     /~     \_/ \
      \____|__________/  \
             \_______      \
                     `\     \                 \
                       |     |                  \
                      /      /                    \
                     /     /                       \\
                   /      /                         \ \
                  /     /                            \  \
                /     /             _----_            \   \
               /     /           _-~      ~-_         |   |
              (      (        _-~    _--_    ~-_     _/   |
               \      ~-____-~    _-~    ~-_    ~-_-~    /
                 ~-_           _-~          ~-_       _-~
                    ~--______-~                ~-___-~
";
            string menu5 = "1) Directions\n2) Play\n3) Exit \n\n\n" + @"

          
           /^\/^\
         _|__|  O|
\/     /~     \_/ \
 \____|__________/  \
        \_______      \
                `\     \                 \
                  |     |                  \
                 /      /                    \
                /     /                       \\
              /      /                         \ \
             /     /                            \  \
           /     /             _----_            \   \
          /     /           _-~      ~-_         |   |
         (      (        _-~    _--_    ~-_     _/   |
          \      ~-____-~    _-~    ~-_    ~-_-~    /
            ~-_           _-~          ~-_       _-~
               ~--______-~                ~-___-~
";
            int sleepSpeed = 100;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(menu1);
            Thread.Sleep(sleepSpeed);
            Console.Clear();
            Console.WriteLine(menu2);
            Thread.Sleep(sleepSpeed);
            Console.Clear();
            Console.WriteLine(menu3);
            Thread.Sleep(sleepSpeed);
            Console.Clear();
            Console.WriteLine(menu4);
            Thread.Sleep(sleepSpeed);
            Console.Clear();
            Console.WriteLine(menu5);
            Thread.Sleep(sleepSpeed);

            userAction = Console.ReadLine().ToLower();
        }
        #endregion
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
        #endregion
    }
}
