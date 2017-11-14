using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppFrogClass
{
    class Sprite
    {
        public int width;
        public int height;

        public char[,] data;

        public Sprite(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);

            height = lines.Length;
            width = lines[0].Length;

            data = new char[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    data[i, j] = lines[i][j];
                }
            }
        }

        
    }
}
