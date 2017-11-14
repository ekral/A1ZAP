using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppFrogClass
{
    class GameBuffer
    {
        public char[,] data;
        public int height;
        public int width;

        public GameBuffer(int height, int width)
        {
            this.height = height;
            this.width = width;
            data = new char[height, width];

            Fill(' ');
        }  
        
        public void Fill(char character)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    data[i, j] = character;
                }
            }
        }
    }
}
