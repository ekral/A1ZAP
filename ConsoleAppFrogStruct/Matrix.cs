using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppFrogStruct
{
    struct Matrix
    {
        public char[,] data;
        public int height;
        public int width;

        public Matrix(int height, int width)
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
                    data[i, j] = ' ';
                }
            }
        }

        public void Write(Matrix other, Position position)
        {
            for (int i = 0; i < other.height; i++)
            {
                for (int j = 0; j < other.width; j++)
                {
                    int x = position.x + j;
                    int y = position.y + i;

                    if ((x >= 0) && (x < width) && (y >= 0) && (y < height))
                    {
                        data[y, x] = other.data[i, j];
                    }
                }
            }
        }
    }
}
