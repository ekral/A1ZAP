using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppFrogStruct
{

    class Program
    {
        // 1) Dodelejte ukonceni programu pomoci klavesy Q nebo ESC
        // 2) Pridejte pocitani zivotu zelvy, 
        //    pokud ji prejede, auto, tak prijde o zivot
        // 3) Pokud uz nebude mit zivoty, tak vypiste text gameover

        static void Napln(char[,] matice, char znak)
        {
            int height = matice.GetLength(0);
            int width = matice.GetLength(1);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    matice[i, j] = znak;
                }
            }
        }

        static char[,] Nacti(string fileName)
        {
            string[] radky = File.ReadAllLines(fileName);
            int height = radky.Length;
            int width = radky[0].Length;

            char[,] matice = new char[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    matice[i, j] = radky[i][j];
                }
            }

            return matice;
        }

        static void Vykresli(char[,] matice, char[,] maticeAuto, int xoffset, int yoffset)
        {
            int height = matice.GetLength(0);
            int width = matice.GetLength(1);

            int heightAuto = maticeAuto.GetLength(0);
            int widthAuto = maticeAuto.GetLength(1);

            for (int i = 0; i < heightAuto; i++)
            {
                for (int j = 0; j < widthAuto; j++)
                {
                    int xpos = xoffset + j;
                    int ypos = yoffset + i;
                    if ((xpos >= 0) && (xpos < width) && (ypos >= 0) && (ypos < height))
                    {
                        matice[ypos, xpos] = maticeAuto[i, j];
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            bool konec = false;
            int zivoty = 3;
            const int width = 40;
            const int heigth = 20;
            int x = width / 2;
            int y = heigth / 2;

            int xAuto1 = 0;
            int yAuto1 = 2;
            int xAuto2 = width;
            int yAuto2 = 10;

            char[,] matice = new char[heigth, width];

            char[,] maticeAuto1 = Nacti("auto1.txt");

            char[,] maticeAuto2 = Nacti("auto2.txt");

            int heightAuto1 = maticeAuto1.GetLength(0);
            int widthAuto1 = maticeAuto1.GetLength(1);
            int widthAuto2 = maticeAuto2.GetLength(1);

            char[,] maticeFrogUp = Nacti("frog_up.txt");
            char[,] maticeFrogDown = Nacti("frog_down.txt");
            Frog frog = new Frog(0, 0, maticeFrogUp, maticeFrogDown);
            frog.lifes = 3;

            do
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey klavesa = Console.ReadKey(true).Key;

                    switch (klavesa)
                    {
                        case ConsoleKey.LeftArrow:
                            if (x > 0)
                            {
                                --x;
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            if (x < (width - 1))
                            {
                                ++x;
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            maticeFrog = maticeFrogUp;
                            if (y > 0)
                            {
                                --y;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            maticeFrog = maticeFrogDown;
                            if (y < (heigth - 1))
                            {
                                ++y;
                            }
                            break;
                        case ConsoleKey.Q:
                            konec = true;
                            break;
                        case ConsoleKey.Escape:
                            konec = true;
                            break;
                    }

                }

                Napln(matice, ' ');

                Vykresli(matice, maticeFrog, x, y);
                Vykresli(matice, maticeAuto1, xAuto1, yAuto1);
                Vykresli(matice, maticeAuto2, xAuto2, yAuto2);


                Console.SetCursorPosition(0, 0);

                Console.WriteLine($"Zivoty: {zivoty}    ");

                for (int i = 0; i < heigth; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Console.Write(matice[i, j]);
                    }
                    Console.WriteLine();
                }

                if ((y == yAuto1) && (x == xAuto1))
                {
                    if (zivoty > 1)
                    {
                        --zivoty;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Game over");
                        konec = true;
                    }
                }

                ++xAuto1;
                if (xAuto1 >= width)
                {
                    xAuto1 = -widthAuto1;
                }

                --xAuto2;
                if (xAuto2 < -widthAuto2)
                {
                    xAuto2 = width;
                }

                System.Threading.Thread.Sleep(20);

            } while (!konec);

            Console.Read();
        }
    }

}
