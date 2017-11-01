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
        static Matrix MatrixFromFile(string fileName)
        {
            string[] radky = File.ReadAllLines(fileName);
            int height = radky.Length;
            int width = radky[0].Length;
   
            Matrix matice = new Matrix(height, width);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    matice.data[i, j] = radky[i][j];
                }
            }

            return matice;
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            bool gameOver = false;
            int lifes = 3;
            const int width = 40;
            const int heigth = 20;

            Position frogPos = new Position(width / 2, heigth / 2);
            Position car1Pos = new Position(0, 2);
            Position car2Pos = new Position(width - 1, 10);

            Matrix bufferMatrix = new Matrix(heigth, width);
            Matrix car1Matrix = MatrixFromFile("auto1.txt");
            Matrix car2Matrix = MatrixFromFile("auto2.txt");
            Matrix matrixFrogUp = MatrixFromFile("frog_up.txt");
            Matrix matrixFrogDown = MatrixFromFile("frog_down.txt");
            Matrix matrixFrog = matrixFrogUp;
 
            do
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.LeftArrow:
                            if (frogPos.x > 0)
                            {
                                --frogPos.x;
                            }
                            break;
                        case ConsoleKey.RightArrow:
                            if (frogPos.x < (width - 1))
                            {
                                ++frogPos.x;
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            matrixFrog = matrixFrogUp;
                            if (frogPos.y > 0)
                            {
                                --frogPos.y;
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            matrixFrog = matrixFrogDown;
                            if (frogPos.y < (heigth - 1))
                            {
                                ++frogPos.y;
                            }
                            break;
                        case ConsoleKey.Q:
                            gameOver = true;
                            break;
                        case ConsoleKey.Escape:
                            gameOver = true;
                            break;
                    }

                }

                bufferMatrix.Fill(' ');
                bufferMatrix.Write(matrixFrog, frogPos);
                bufferMatrix.Write(car1Matrix, car1Pos);
                bufferMatrix.Write(car2Matrix, car2Pos);

                Console.SetCursorPosition(0, 0);

                Console.WriteLine($"Lifes: {lifes}    ");

                for (int i = 0; i < heigth; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Console.Write(bufferMatrix.data[i, j]);
                    }
                    Console.WriteLine();
                }

                // TODO: life counting
                
                ++car1Pos.x;
                if (car1Pos.x >= bufferMatrix.width)
                {
                    car1Pos.x = -car1Matrix.width;
                }

                --car2Pos.x;
                if (car2Pos.x < -car2Matrix.width)
                {
                    car2Pos.x = bufferMatrix.width;
                }

                System.Threading.Thread.Sleep(20);

            } while (!gameOver);

            Console.Read();
        }
    }

}
