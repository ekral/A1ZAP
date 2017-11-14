using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppFrogClass
{
    class GameObject
    {
        public Position position;
        public int width;
        public int height;

        private Sprite sprite;

        public GameObject(Position position, Sprite sprite)
        {
            this.position = position;
            SetSprite(sprite);
        }

        public GameObject(int x, int y, Sprite sprite)
        {
            position = new Position(x, y);
            SetSprite(sprite);
        }

        public void Write(GameBuffer buffer)
        {
            for (int i = 0; i < sprite.height; i++)
            {
                for (int j = 0; j < sprite.width; j++)
                {
                    int x = position.x + j;
                    int y = position.y + i;

                    if ((x >= 0) && (x < buffer.width) && (y >= 0) && (y < buffer.height))
                    {
                        char c = sprite.data[i, j];
                        if (c != 'x')
                        {
                            buffer.data[y, x] = c;
                        }
                    }
                }
            }
        }

        public void SetSprite(Sprite sprite)
        {
            this.sprite = sprite;

            width = sprite.width;
            height = sprite.height;
        }

        public bool Intersect(GameObject other)
        {
            int x = position.x;
            int y = position.y;
            int a = x + width - 1;
            int b = y + height -1;

            int x1 = other.position.x;
            int y1 = other.position.y;
            int a1 = x1 + other.width -1;
            int b1 = y1 + other.height -1;

            bool intersect = !(a < x1 || a1 < x || b < y1 || b1 < y);

            return intersect;

        }
    }
}
