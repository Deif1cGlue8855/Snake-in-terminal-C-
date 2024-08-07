using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using static System.Formats.Asn1.AsnWriter;

public static class Program
{
    public static void SaveScoreB(int score)
    {
        Console.Clear();
        Console.SetCursorPosition(89, 8);
        Console.WriteLine("  __  __  __  ___  __ __  ____");
        Console.SetCursorPosition(89, 9);
        Console.WriteLine(" (( \\ ||\\ || // \\\\ || // ||   ");
        Console.SetCursorPosition(89, 10);
        Console.WriteLine("  \\\\  ||\\\\|| ||=|| ||<<  ||== ");
        Console.SetCursorPosition(89, 11);
        Console.WriteLine(" \\_)) || \\|| || || || \\\\ ||___");

        //Write HIGH SCORE
        Console.SetCursorPosition(99, 15);
        Console.WriteLine("HIGH SCORES \n");
        //Prepares the lists
        List<string> scores = new List<string>();
        List<string> names = new List<string>();
        // Get all data from files
        StreamReader readers = new StreamReader("scores.txt");
        string currentLine;

        while (true)
        {
            currentLine = readers.ReadLine();
            if (currentLine == null) break;
            scores.Add(currentLine);
        }
        readers.Close();


        StreamReader readern = new StreamReader("names.txt");

        while (true)
        {
            currentLine = readern.ReadLine();
            if (currentLine == null) break;
            names.Add(currentLine);
        }
        readern.Close();

        //Prints out all scores
        int max = 10;
        if (names.Count() > 10)
        {
            max = 10;
        }
        else
        {
            max = names.Count();
        }
        for (int i = 0; i < max; i++)
        {
            Console.SetCursorPosition(99, 17 + i);
            Console.WriteLine(names[i] + "   " + scores[i]);
        }

        //Enter name
        Console.SetCursorPosition(95, 30);
        string name = "";
        Console.WriteLine("Enter a 3 letter name: ");
        bool allow = false;
        while (allow != true)
        {
            Console.SetCursorPosition(99, 32);
            name = Console.ReadLine();
            int length = name.Length;
            if (length == 3)
            {
                allow = true;
            }
            else
            {
                Console.SetCursorPosition(99, 32);
                Console.WriteLine("INVALID NAME");
                System.Threading.Thread.Sleep(1000);
                Console.SetCursorPosition(99, 32);
                Console.WriteLine("                                  ");
                Console.SetCursorPosition(99, 32);
                Console.WriteLine("Enter AGAIN");
                System.Threading.Thread.Sleep(1000);
                Console.SetCursorPosition(99, 32);
                Console.WriteLine("                                  ");

            }
        }

        //instert score and name int list
        foreach (string s in scores)
        {
            if (Convert.ToInt32(score) > Convert.ToInt32(s))
            {
                int index = scores.IndexOf(s);
                scores.Insert(index, Convert.ToString(score));
                names.Insert(index, name);
                break;

            }
        }

        //delete all file contents
        StreamWriter writers = new StreamWriter("scores.txt");
        writers.Write("");
        writers.Close();
        StreamWriter writern = new StreamWriter("names.txt");
        writern.Write("");
        writern.Close();

        //write all name and score
        StreamWriter writers1 = new StreamWriter("scores.txt", true);
        foreach (string s in scores)
        {
            writers1.WriteLine(s);
        }
        writers1.Close();
        StreamWriter writern1 = new StreamWriter("names.txt", true);
        foreach (string s in names)
        {
            writern1.WriteLine(s);
        }
        writern1.Close();

        Console.Clear();
        MainMenuB();
    }
    public static void SnakeGameB(int difficulty)
    {
        //Console.Write("█");
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.CursorVisible = false;

        Console.WindowHeight = 54;
        Console.WindowWidth = 209;
        Console.BufferHeight = 54;
        Console.BufferWidth = 209;
        Console.Clear();

        int playerX = Console.WindowWidth / 2;
        int playerY = Console.WindowHeight / 2;
        List<int> playerPosX = new List<int>();
        List<int> playerPosY = new List<int>();
        //playerPosX.Add(1);
        //playerPosY.Add(1);

        int spx = playerX;
        int spy = playerY;
        List<string> moves = new List<string>();

        int playerMX = 0;
        int playerMY = 0;

        int foodX = 0;
        int foodY = 0;
        bool foodEat = true;

        //Draw map
        int score = 0;
        int[] ArenaX = { 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118 };
        int[] ArenaY = { 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42 };
        //SET FPS FROM DIFFICULTY
        int sleepTime = 50 * difficulty;

        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(89, 7);
        Console.WriteLine("  __  __  __  ___  __ __  ____");
        Console.SetCursorPosition(89, 8);
        Console.WriteLine(" (( \\ ||\\ || // \\\\ || // ||   ");
        Console.SetCursorPosition(89, 9);
        Console.WriteLine("  \\\\  ||\\\\|| ||=|| ||<<  ||== ");
        Console.SetCursorPosition(89, 10);
        Console.WriteLine(" \\_)) || \\|| || || || \\\\ ||___");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        //Console.BackgroundColor = ConsoleColor.White;
        int cont = 0;
        foreach (int x in ArenaX)
        {
            System.Threading.Thread.Sleep(20);
            Console.SetCursorPosition(x, ArenaY[cont]);
            Console.Write("█");
            cont++;
        }
        Console.BackgroundColor = ConsoleColor.Green;
        for (int x = 91; x < 118; x++)
        {
            for (int y = 13; y < 42; y++)
            {
                //System.Threading.Thread.Sleep(1);
                Console.SetCursorPosition(x, y);
                Console.Write(" ");
            }
        }
        Console.ResetColor();
        //Environment.Exit(0);
        foodEat = true;

        bool GameOver = false;
        while (GameOver == false)
        {
            Console.ForegroundColor = ConsoleColor.White;
            // Move speed
            if (playerMX != 0)
            {
                System.Threading.Thread.Sleep(sleepTime);
            }
            else if (playerMY != 0)
            {
                System.Threading.Thread.Sleep(sleepTime + 30);
            }
            else
            {
                System.Threading.Thread.Sleep(10);
            }

            //Score and position display
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.SetCursorPosition(91, 42);
            Console.WriteLine("Score: ");
            Console.SetCursorPosition(97, 42);
            Console.WriteLine(score);
            Console.SetCursorPosition(114, 42);
            if (difficulty == 3)
            {
                Console.WriteLine("EASY");
            }
            else if (difficulty == 2)
            {
                Console.WriteLine("MEDI");
            }
            else if (difficulty == 1)
            {
                Console.WriteLine("HARD");
            }
            Console.SetCursorPosition(17, 20);
            //Console.WriteLine("Y: " + playerY);

            //Save positions
            if (playerMX != 0 || playerMY != 0)
            {
                playerPosX.Add(playerX);
                playerPosY.Add(playerY);
            }

            //Player display
            playerX += playerMX;
            playerY += playerMY;
            //player destroy
            cont = 0;
            foreach (int x in ArenaX)
            {
                if (playerX == x && playerY == ArenaY[cont])
                {
                    GameOver = true;
                }
                cont++;
            }
            if (playerMX != 0 || playerMY != 0)
            {
                cont = 0;
                foreach (int x in playerPosX)
                {
                    if (playerX == x && playerY == playerPosY[cont])
                    {
                        GameOver = true;
                    }
                    cont++;
                }
            }
            //place the character visualy
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.SetCursorPosition(playerX, playerY);
            Console.Write("□");

            // Character controller
            if (Console.KeyAvailable == true)
            {

                ConsoleKeyInfo keyinfo = Console.ReadKey(true);


                if (keyinfo.Key == ConsoleKey.RightArrow && playerMX == 0)
                {
                    playerMX = 1;
                    playerMY = 0;

                }
                if (keyinfo.Key == ConsoleKey.LeftArrow && playerMX == 0)
                {
                    playerMX = -1;
                    playerMY = 0;

                }
                if (keyinfo.Key == ConsoleKey.UpArrow && playerMY == 0)
                {
                    playerMY = -1;
                    playerMX = 0;

                }
                if (keyinfo.Key == ConsoleKey.DownArrow && playerMY == 0)
                {
                    playerMY = 1;
                    playerMX = 0;

                }
                if (keyinfo.Key == ConsoleKey.Escape)
                {
                    for (int i = 0; i < playerPosX.Count; i++)
                    {
                        Console.SetCursorPosition(0, i);
                        Console.Write(i + ")" + playerPosX[i] + " " + playerPosY[i] + "   .");
                    }
                    Environment.Exit(0);
                }


            }
            // Check if Food eat
            if (playerX == foodX && playerY == foodY)
            {
                score++;
                //Console.Beep();
                foodEat = true;
            }
            //Move the scrubber
            if (playerMX == 1)
            {
                moves.Add("R");
            }
            else if (playerMX == -1)
            {
                moves.Add("L");
            }
            else if (playerMY == 1)
            {
                moves.Add("D");
            }
            else if (playerMY == -1)
            {
                moves.Add("U");
            }
            else
            {
                moves.Add("N");
            }
            //Remove from list
            if (playerMX != 0 && playerPosX.Count != 0 || playerMY != 0 && playerPosX.Count != 0)
            {
                if (!foodEat)
                {
                    playerPosX.RemoveAt(0);
                    playerPosY.RemoveAt(0);
                }
            }
            //Grow mechanic / Pause scrubber / move him
            if (foodEat != true)
            {
                Console.SetCursorPosition(spx, spy);
                Console.Write(" ");

                if (moves[0] == "U")
                {
                    spy -= 1;
                }
                else if (moves[0] == "D")
                {
                    spy += 1;
                }
                else if (moves[0] == "R")
                {
                    spx += 1;
                }
                else if (moves[0] == "L")
                {
                    spx -= 1;
                }
                else if (moves[0] == "N")
                {
                    spx = spx;
                }
                moves.RemoveAt(0);
            }
            Random rnd = new Random();
            // Food draw
            if (foodEat == true)
            {
                foodX = rnd.Next(91, 117);
                foodY = rnd.Next(13, 41);
                Console.SetCursorPosition(foodX, foodY);
                Console.Write("*");

                foodEat = false;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(foodX, foodY);
            Console.Write("•");

            if (playerMY == 0 && playerMX == 0)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.SetCursorPosition(playerX, playerY);
                Console.Write("□");
            }
            Console.ResetColor();
        }
        //GameOverScreen
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();

        Console.SetCursorPosition(98, 20);
        Console.Write("GAME OVER");
        //Console.Beep(1000, 1200);
        //Console.Beep(500, 1200);
        //Console.Beep(250, 1200);
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key != null)
            {
                SaveScoreB(score);
                break;
            }
        }

    }
    public static void InfoB()
    {
        Console.SetCursorPosition(89, 8);
        Console.WriteLine("  __  __  __  ___  __ __  ____");
        Console.SetCursorPosition(89, 9);
        Console.WriteLine(" (( \\ ||\\ || // \\\\ || // ||   ");
        Console.SetCursorPosition(89, 10);
        Console.WriteLine("  \\\\  ||\\\\|| ||=|| ||<<  ||== ");
        Console.SetCursorPosition(89, 11);
        Console.WriteLine(" \\_)) || \\|| || || || \\\\ ||___");

        Console.SetCursorPosition(58, 14);
        Console.WriteLine("This is the baisic snake game we all know and love where you go around, eat apples and grow");
        Console.SetCursorPosition(54, 15);
        Console.WriteLine("This is my best attempt to recreate it in the console using c# and make it seem more like an arcade game");
        Console.SetCursorPosition(89, 17);
        Console.WriteLine("Controls: just the arrow keys");

        Console.SetCursorPosition(93, 18);
        Console.WriteLine("Press any key to exit");

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key != null)
            {
                MainMenuB();
                break;
            }
        }
    }
    public static void DifficultyB()
    {
        Console.Clear();
        Console.SetCursorPosition(89, 8);
        Console.WriteLine("  __  __  __  ___  __ __  ____");
        Console.SetCursorPosition(89, 9);
        Console.WriteLine(" (( \\ ||\\ || // \\\\ || // ||   ");
        Console.SetCursorPosition(89, 10);
        Console.WriteLine("  \\\\  ||\\\\|| ||=|| ||<<  ||== ");
        Console.SetCursorPosition(89, 11);
        Console.WriteLine(" \\_)) || \\|| || || || \\\\ ||___");

        Console.SetCursorPosition(94, 15);
        Console.WriteLine("CHOOSE YOUR DIFFICULTY");
        Console.SetCursorPosition(84, 16);
        Console.WriteLine("1) Easy - ARE YOU REALLY THAT SCARED OF THIS GAME !?");
        Console.SetCursorPosition(84, 17);
        Console.WriteLine("2) Medium - GO FOR THE CHALLENGE !!!");
        Console.SetCursorPosition(84, 18);
        Console.WriteLine("3) Hard - ONLY FOR THE BEST OF THE BEST !!!");

        ConsoleKeyInfo keyInfo = Console.ReadKey();
        if (keyInfo.Key == ConsoleKey.D1)
        {
            SnakeGameB(3);
        }
        else if (keyInfo.Key == ConsoleKey.D2)
        {
            SnakeGameB(2);
        }
        else if (keyInfo.Key == ConsoleKey.D3)
        {
            SnakeGameB(1);
        }
        Console.Clear();
    }
    public static void MainMenuB()
    {
        Console.Clear();
        Console.WindowHeight = 54;
        Console.WindowWidth = 209;
        Console.BufferHeight = 54;
        Console.BufferWidth = 209;
        Console.CursorVisible = false;
        using (StreamWriter writern = new StreamWriter("names.txt", true))
        {
            writern.Write("");
        }
        using (StreamWriter writern = new StreamWriter("scores.txt", true))
        {
            writern.Write("");
        }
        bool quit = false;
        while (!quit)
        {
            Console.SetCursorPosition(89, 8);
            Console.WriteLine("  __  __  __  ___  __ __  ____");
            Console.SetCursorPosition(89, 9);
            Console.WriteLine(" (( \\ ||\\ || // \\\\ || // ||   ");
            Console.SetCursorPosition(89, 10);
            Console.WriteLine("  \\\\  ||\\\\|| ||=|| ||<<  ||== ");
            Console.SetCursorPosition(89, 11);
            Console.WriteLine(" \\_)) || \\|| || || || \\\\ ||___");
            Console.SetCursorPosition(102, 13);
            Console.WriteLine("PRESS");
            Console.SetCursorPosition(100, 14);
            Console.WriteLine("1) Start");
            Console.SetCursorPosition(100, 15);
            Console.WriteLine("2) Info");

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.D1)
            {
                DifficultyB();
            }
            else if (keyInfo.Key == ConsoleKey.D2)
            {
                Console.Clear();
                InfoB();
            }
            Console.Clear();
        }
    }

    public static void SaveScoreS(int score)
    {
        Console.Clear();
        Console.SetCursorPosition(65, 4);
        Console.WriteLine("  __  __  __  ___  __ __  ____");
        Console.SetCursorPosition(65, 5);
        Console.WriteLine(" (( \\ ||\\ || // \\\\ || // ||   ");
        Console.SetCursorPosition(65, 6);
        Console.WriteLine("  \\\\  ||\\\\|| ||=|| ||<<  ||== ");
        Console.SetCursorPosition(65, 7);
        Console.WriteLine(" \\_)) || \\|| || || || \\\\ ||___");

        //Write HIGH SCORE
        Console.SetCursorPosition(99 - 24, 15 - 3);
        Console.WriteLine("HIGH SCORES \n");
        //Prepares the lists
        List<string> scores = new List<string>();
        List<string> names = new List<string>();
        // Get all data from files
        StreamReader readers = new StreamReader("scores.txt");
        string currentLine;

        while (true)
        {
            currentLine = readers.ReadLine();
            if (currentLine == null) break;
            scores.Add(currentLine);
        }
        readers.Close();


        StreamReader readern = new StreamReader("names.txt");

        while (true)
        {
            currentLine = readern.ReadLine();
            if (currentLine == null) break;
            names.Add(currentLine);
        }
        readern.Close();

        //Prints out all scores
        int max = 10;
        if (names.Count() > 10)
        {
            max = 10;
        }
        else
        {
            max = names.Count();
        }
        for (int i = 0; i < max; i++)
        {
            Console.SetCursorPosition(99 - 24, (17 + i) - 3);
            Console.WriteLine(names[i] + "   " + scores[i]);
        }

        //Enter name
        Console.SetCursorPosition(75, 25);
        Console.Write("SCORE: " + score);
        Console.SetCursorPosition(95 - 26, 30 - 3);
        string name = "";
        Console.WriteLine("ENTER A 3 LETTER NAME: ");
        bool allow = false;
        while (allow != true)
        {
            Console.SetCursorPosition(99 - 24, 32 - 3);
            name = Console.ReadLine();
            int length = name.Length;
            if (length == 3)
            {
                allow = true;
            }
            else
            {
                Console.SetCursorPosition(99 - 24, 32 - 3);
                Console.WriteLine("INVALID NAME");
                System.Threading.Thread.Sleep(1000);
                Console.SetCursorPosition(99 - 24, 32 - 3);
                Console.WriteLine("                                  ");
                Console.SetCursorPosition(99 - 24, 32 - 3);
                Console.WriteLine("Enter AGAIN");
                System.Threading.Thread.Sleep(1000);
                Console.SetCursorPosition(99 - 24, 32 - 3);
                Console.WriteLine("                                  ");

            }
        }

        //instert score and name int list
        foreach (string s in scores)
        {
            if (Convert.ToInt32(score) > Convert.ToInt32(s))
            {
                int index = scores.IndexOf(s);
                scores.Insert(index, Convert.ToString(score));
                names.Insert(index, name);
                break;

            }
        }

        //delete all file contents
        StreamWriter writers = new StreamWriter("scores.txt");
        writers.Write("");
        writers.Close();
        StreamWriter writern = new StreamWriter("names.txt");
        writern.Write("");
        writern.Close();

        //write all name and score
        StreamWriter writers1 = new StreamWriter("scores.txt", true);
        foreach (string s in scores)
        {
            writers1.WriteLine(s);
        }
        writers1.Close();
        StreamWriter writern1 = new StreamWriter("names.txt", true);
        foreach (string s in names)
        {
            writern1.WriteLine(s);
        }
        writern1.Close();

        Console.Clear();

    }
    public static void SnakeGameS(int difficulty)
    {
        //Console.Write("█");
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.CursorVisible = false;

        Console.WindowHeight = 42;
        Console.WindowWidth = 156;
        Console.BufferHeight = 100;
        Console.BufferWidth = 156;
        Console.Clear();

        int playerX = 80;
        int playerY = 25;
        List<int> playerPosX = new List<int>();
        List<int> playerPosY = new List<int>();
        //playerPosX.Add(1);
        //playerPosY.Add(1);

        int spx = playerX;
        int spy = playerY;
        List<string> moves = new List<string>();

        int playerMX = 0;
        int playerMY = 0;

        int foodX = 0;
        int foodY = 0;
        bool foodEat = true;

        //Draw map
        int score = 0;
        int[] ArenaX = { 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118, 118 };
        int[] ArenaY = { 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 42, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42 };
        //SET FPS FROM DIFFICULTY
        int sleepTime = 50 * difficulty;

        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(65, 4);
        Console.WriteLine("  __  __  __  ___  __ __  ____");
        Console.SetCursorPosition(65, 5);
        Console.WriteLine(" (( \\ ||\\ || // \\\\ || // ||   ");
        Console.SetCursorPosition(65, 6);
        Console.WriteLine("  \\\\  ||\\\\|| ||=|| ||<<  ||== ");
        Console.SetCursorPosition(65, 7);
        Console.WriteLine(" \\_)) || \\|| || || || \\\\ ||___");
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        //Console.BackgroundColor = ConsoleColor.White;
        int cont = 0;
        foreach (int x in ArenaX)
        {
            System.Threading.Thread.Sleep(20);
            Console.SetCursorPosition(x - 24, ArenaY[cont] - 3);
            Console.Write("█");
            cont++;
        }
        //Environment.Exit(0);
        Console.BackgroundColor = ConsoleColor.Green;
        for (int x = 67; x < 94; x++)
        {
            for (int y = 13 - 3; y < 42 - 3; y++)
            {
                //System.Threading.Thread.Sleep(1);
                Console.SetCursorPosition(x, y);
                Console.Write(" ");
            }
        }
        Console.ResetColor();
        //Environment.Exit(0);
        foodEat = true;

        bool GameOver = false;
        while (GameOver == false)
        {
            Console.ForegroundColor = ConsoleColor.White;
            // Move speed
            if (playerMX != 0)
            {
                System.Threading.Thread.Sleep(sleepTime);
            }
            else if (playerMY != 0)
            {
                System.Threading.Thread.Sleep(sleepTime + 30);
            }
            else
            {
                System.Threading.Thread.Sleep(10);
            }

            //Score and position display
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.SetCursorPosition(91 - 24, 42 - 3);
            Console.WriteLine("Score: ");
            Console.SetCursorPosition(97 - 24, 42 - 3);
            Console.WriteLine(score);
            Console.SetCursorPosition(114 - 24, 42 - 3);
            if (difficulty == 3)
            {
                Console.WriteLine("EASY");
            }
            else if (difficulty == 2)
            {
                Console.WriteLine("MEDI");
            }
            else if (difficulty == 1)
            {
                Console.WriteLine("HARD");
            }
            Console.SetCursorPosition(17, 20);
            //Console.WriteLine("Y: " + playerY);

            //Save positions
            if (playerMX != 0 || playerMY != 0)
            {
                playerPosX.Add(playerX);
                playerPosY.Add(playerY);
            }

            //Player display
            playerX += playerMX;
            playerY += playerMY;
            //player destroy
            cont = 0;
            foreach (int x in ArenaX)
            {
                if (playerX == x - 24 && playerY == ArenaY[cont] - 3)
                {
                    GameOver = true;
                }
                cont++;
            }
            if (playerMX != 0 || playerMY != 0)
            {
                cont = 0;
                foreach (int x in playerPosX)
                {
                    if (playerX == x && playerY == playerPosY[cont])
                    {
                        GameOver = true;
                    }
                    cont++;
                }
            }
            //place the character visualy
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.SetCursorPosition(playerX, playerY);
            Console.Write("□");

            // Character controller
            if (Console.KeyAvailable == true)
            {

                ConsoleKeyInfo keyinfo = Console.ReadKey(true);


                if (keyinfo.Key == ConsoleKey.RightArrow && playerMX == 0)
                {
                    playerMX = 1;
                    playerMY = 0;

                }
                if (keyinfo.Key == ConsoleKey.LeftArrow && playerMX == 0)
                {
                    playerMX = -1;
                    playerMY = 0;

                }
                if (keyinfo.Key == ConsoleKey.UpArrow && playerMY == 0)
                {
                    playerMY = -1;
                    playerMX = 0;

                }
                if (keyinfo.Key == ConsoleKey.DownArrow && playerMY == 0)
                {
                    playerMY = 1;
                    playerMX = 0;

                }
                if (keyinfo.Key == ConsoleKey.Escape)
                {
                    for (int i = 0; i < playerPosX.Count; i++)
                    {
                        Console.SetCursorPosition(0, i);
                        Console.Write(i + ")" + playerPosX[i] + " " + playerPosY[i] + "   .");
                    }
                    Environment.Exit(0);
                }


            }
            // Check if Food eat
            if (playerX == foodX && playerY == foodY)
            {
                score++;
                //Console.Beep();
                foodEat = true;
            }
            //Move the scrubber
            if (playerMX == 1)
            {
                moves.Add("R");
            }
            else if (playerMX == -1)
            {
                moves.Add("L");
            }
            else if (playerMY == 1)
            {
                moves.Add("D");
            }
            else if (playerMY == -1)
            {
                moves.Add("U");
            }
            else
            {
                moves.Add("N");
            }
            //Remove from list
            if (playerMX != 0 && playerPosX.Count != 0 || playerMY != 0 && playerPosX.Count != 0)
            {
                if (!foodEat)
                {
                    playerPosX.RemoveAt(0);
                    playerPosY.RemoveAt(0);
                }
            }
            //Grow mechanic / Pause scrubber / move him
            if (foodEat != true)
            {
                Console.SetCursorPosition(spx, spy);
                Console.Write(" ");

                if (moves[0] == "U")
                {
                    spy -= 1;
                }
                else if (moves[0] == "D")
                {
                    spy += 1;
                }
                else if (moves[0] == "R")
                {
                    spx += 1;
                }
                else if (moves[0] == "L")
                {
                    spx -= 1;
                }
                else if (moves[0] == "N")
                {
                    spx = spx;
                }
                moves.RemoveAt(0);
            }
            Random rnd = new Random();
            // Food draw
            if (foodEat == true)
            {
                foodX = rnd.Next(91 - 24, 117 - 24);
                foodY = rnd.Next(13 - 3, 41 - 3);
                Console.SetCursorPosition(foodX, foodY);
                Console.Write("*");

                foodEat = false;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(foodX, foodY);
            Console.Write("•");

            if (playerMY == 0 && playerMX == 0)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.SetCursorPosition(playerX, playerY);
                Console.Write("□");
            }
            Console.ResetColor();
        }
        //GameOverScreen
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();

        Console.SetCursorPosition(98 - 24, 20 - 3);
        Console.Write("GAME OVER");
        Console.ForegroundColor= ConsoleColor.Gray;
        Console.ForegroundColor= ConsoleColor.DarkGray;
        Console.SetCursorPosition(98 - 28, 20 - 1);
        Console.Write("PRESS TO CONTINUE");
        Console.ForegroundColor = ConsoleColor.White;
        //Console.Beep(1000, 1200);
        //Console.Beep(500, 1200);
        //Console.Beep(250, 1200);
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key != null)
            {
                SaveScoreS(score);
                break;
            }
        }

    }
    public static void InfoS()
    {
        Console.SetCursorPosition(65, 4);
        Console.WriteLine("  __  __  __  ___  __ __  ____");
        Console.SetCursorPosition(65, 5);
        Console.WriteLine(" (( \\ ||\\ || // \\\\ || // ||   ");
        Console.SetCursorPosition(65, 6);
        Console.WriteLine("  \\\\  ||\\\\|| ||=|| ||<<  ||== ");
        Console.SetCursorPosition(65, 7);
        Console.WriteLine(" \\_)) || \\|| || || || \\\\ ||___");

        Console.SetCursorPosition(38, 9);
        Console.WriteLine("This is the baisic snake game we all know and love where you go around, eat apples and grow");
        Console.SetCursorPosition(32, 10);
        Console.WriteLine("This is my best attempt to recreate it in the console using c# and make it seem more like an arcade game");
        Console.SetCursorPosition(65, 12);
        Console.WriteLine("Controls: just the arrow keys");

        Console.SetCursorPosition(69, 14);
        Console.WriteLine("Press any key to exit");

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key != null)
            {
                MainMenuS();
                break;
            }
        }
    }
    public static void DifficultyS()
    {
        Console.Clear();
        Console.SetCursorPosition(65, 4);
        Console.WriteLine("  __  __  __  ___  __ __  ____");
        Console.SetCursorPosition(65, 5);
        Console.WriteLine(" (( \\ ||\\ || // \\\\ || // ||   ");
        Console.SetCursorPosition(65, 6);
        Console.WriteLine("  \\\\  ||\\\\|| ||=|| ||<<  ||== ");
        Console.SetCursorPosition(65, 7);
        Console.WriteLine(" \\_)) || \\|| || || || \\\\ ||___");

        Console.SetCursorPosition(70, 9);
        Console.WriteLine("CHOOSE YOUR DIFFICULTY");
        Console.SetCursorPosition(60, 10);
        Console.WriteLine("1) Easy - ARE YOU REALLY THAT SCARED OF THIS GAME !?");
        Console.SetCursorPosition(60, 11);
        Console.WriteLine("2) Medium - GO FOR THE CHALLENGE !!!");
        Console.SetCursorPosition(60, 12);
        Console.WriteLine("3) Hard - ONLY FOR THE BEST OF THE BEST !!!");

        ConsoleKeyInfo keyInfo = Console.ReadKey();
        if (keyInfo.Key == ConsoleKey.D1)
        {
            SnakeGameS(3);
        }
        else if (keyInfo.Key == ConsoleKey.D2)
        {
            SnakeGameS(2);
        }
        else if (keyInfo.Key == ConsoleKey.D3)
        {
            SnakeGameS(1);
        }
        Console.Clear();
    }
    public static void MainMenuS()
    {
        Console.Clear();
        Console.WindowHeight = 42;
        Console.WindowWidth = 156;
        Console.BufferHeight = 42;
        Console.BufferWidth = 156;
        Console.CursorVisible = false;
        using (StreamWriter writern = new StreamWriter("names.txt", true))
        {
            writern.Write("");
        }
        using (StreamWriter writern = new StreamWriter("scores.txt", true))
        {
            writern.Write("");
        }
        bool quit = false;
        while (!quit)
        {
            Console.SetCursorPosition(65, 4);
            Console.WriteLine("  __  __  __  ___  __ __  ____");
            Console.SetCursorPosition(65, 5);
            Console.WriteLine(" (( \\ ||\\ || // \\\\ || // ||   ");
            Console.SetCursorPosition(65, 6);
            Console.WriteLine("  \\\\  ||\\\\|| ||=|| ||<<  ||== ");
            Console.SetCursorPosition(65, 7);
            Console.WriteLine(" \\_)) || \\|| || || || \\\\ ||___");
            Console.SetCursorPosition(78, 9);
            Console.WriteLine("PRESS");
            Console.SetCursorPosition(76, 10);
            Console.WriteLine("1) Start");
            Console.SetCursorPosition(76, 11);
            Console.WriteLine("2) Info");

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.D1)
            {
                DifficultyS();
            }
            else if (keyInfo.Key == ConsoleKey.D2)
            {
                Console.Clear();
                InfoS();
            }
            Console.Clear();
        }
    }


    public static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("1) LAPTOP \nOR \n2) COMPUTER SCREEN \n(THIS IS VERY IMPORTANT)");
        string choice = Console.ReadLine();
        if (choice == "1")
        {
            MainMenuS();
        }
        else if (choice == "2")
        {
            MainMenuB();
        }
        else
        {
            Console.WriteLine("TRY INPUTTING AGAIN");
            Environment.Exit(0);    
        }
    }
}