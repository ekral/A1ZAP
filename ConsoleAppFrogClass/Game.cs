using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppFrogClass
{
    class Game
    {
        bool gameOver = false;
        int lifes = 3;
        const int width = 40;
        const int heigth = 20;

        private GameBuffer buffer;
        
        GameObject car1;
        GameObject car2;
        GameObject frog;

        private Sprite frogUp;
        private Sprite frogDown;
        private Position frogInitPosition;

        public Game()
        {
            buffer = new GameBuffer(heigth, width);

            car1 = new GameObject(0,2, new Sprite("auto1.txt"));
            car2 = new GameObject(width - 1, 10, new Sprite("auto2.txt"));
    
            frogUp = new Sprite("frog_up.txt");
            frogDown = new Sprite("frog_down.txt");

            frogInitPosition = new Position((width / 2) - (frogUp.width / 2), heigth - frogUp.height);
            frog = new GameObject(frogInitPosition, frogUp);

            Console.CursorVisible = false;
        }

        public void Run()
        {
            while(!gameOver)
            {
                ProcesInput();
                Update();
                Render();

                System.Threading.Thread.Sleep(20);
            } 
        }

        private void ProcesInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        if (frog.position.x > 0)
                        {
                            --frog.position.x;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (frog.position.x < (width - 1))
                        {
                            ++frog.position.x;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        frog.SetSprite(frogUp);
                        if (frog.position.y > 0)
                        {
                            --frog.position.y;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        frog.SetSprite(frogDown);
                        if (frog.position.y < (heigth - 1))
                        {
                            ++frog.position.y;
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
        }

        private void Update()
        {
            ++car1.position.x;
            if (car1.position.x >= buffer.width)
            {
                car1.position.x = -car1.width;
            }

            --car2.position.x;
            if (car2.position.x < -car2.width)
            {
                car2.position.x = buffer.width;
            }

            if(frog.Intersect(car1) || frog.Intersect(car2))
            {
                --lifes;

                frog.position = frogInitPosition;
                frog.SetSprite(frogUp);
            }
        }

        private void Render()
        {
            buffer.Fill('-');
            frog.Write(buffer);
            car1.Write(buffer);
            car2.Write(buffer);

            Console.SetCursorPosition(0, 0);

            Console.WriteLine($"Lifes: {lifes}    ");

            for (int i = 0; i < heigth; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(buffer.data[i, j]);
                }
                Console.WriteLine();
            }
        }

       
    }
}
