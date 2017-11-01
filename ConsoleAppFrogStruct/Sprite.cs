using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppFrogStruct
{
    struct Sprite
    {
        public char[,] Nacti(string fileName)
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
    }
}
