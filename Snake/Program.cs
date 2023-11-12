using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Snake
{
    public class Field
    {
        const char UpLeftCorner = '╔';
        const char DownLeftCorner = '╚';
        const char UpRightCorner = '╗';
        const char DownRightCorner = '╝';
        const char Vertical = '║';
        const char Horizontal = '═';

        private int width;
        private int height;
        private ConsoleColor borderColor;
        private char[,] field;

        public Field(int width = 100, int height = 25, ConsoleColor borderColor = ConsoleColor.Green)
        {
            this.width = width;
            this.height = height;
            this.borderColor = borderColor;
            this.field = new char[height, width];
        }
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        public ConsoleColor BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }
        public char[,] AField
        {
            get { return field; }
            set { field = value; }
        }
        public void Draw()
        {
            Console.ForegroundColor = borderColor;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 && j == 0) Console.Write(field[i, j] = UpLeftCorner);
                    else if (i == 0 && j == width - 1) Console.Write(field[i, j] = UpRightCorner);
                    else if (i == height - 1 && j == 0) Console.Write(field[i, j] = DownLeftCorner);
                    else if (i == height - 1 && j == width - 1) Console.Write(field[i, j] = DownRightCorner);
                    else if (i == 0 || i == height - 1) Console.Write(field[i, j] = Horizontal);
                    else if (j == 0 || j == width - 1) Console.Write(field[i, j] = Vertical);
                    else Console.Write(field[i, j] = ' ');
                }
                Console.WriteLine();
            }
        }
    };
    class Player
    {
        private Field field;
        private int x;
        private int y;
        private char head;
        private ConsoleColor color;

        public Player(Field field, ConsoleColor color = ConsoleColor.White, int y = 1, int x = 1, char head = (char)164)
        {
            this.field = field;
            this.x = x;
            this.y = y;
            this.head = head;
            this.color = color;
        }
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        public char Head
        {
            get { return head; }
            set { head = value; }
        }
        public ConsoleColor Color
        {
            get { return color; }
            set { color = value; }
        }

        public void Set()
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = color;
            Console.CursorTop = x;
            Console.CursorLeft = y;
            Console.Write(field.AField[x, y] = head);
        }
        public void Clear()
        {
            Console.CursorTop = x;
            Console.CursorLeft = y;
            Console.Write(field.AField[x, y] = ' ');
        }
        public void Teleport()
        {
            Random rand = new Random();
            Clear();
            x = rand.Next(1, field.Height - 1);
            y = rand.Next(1, field.Width - 1);
            Set();
        }
        public void Move()
        {
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        {
                            if (x > 1)
                            {
                                Clear();
                                x--;
                                Set();
                            }
                            break;
                        }
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        {
                            if (x < field.Height - 2)
                            {
                                Clear();
                                x++;
                                Set();
                            }
                            break;
                        }
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        {
                            if (y > 1)
                            {
                                Clear();
                                y--;
                                Set();
                            }
                            break;
                        }
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        {
                            if (y < field.Width - 2)
                            {
                                Clear();
                                y++;
                                Set();
                            }
                            break;
                        }
                    case ConsoleKey.Spacebar:
                        Teleport();
                        break;
                }
            } while (key != ConsoleKey.Escape);
        }
    };
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            int horizontal = 100, vertical = 25;
            Field field = new Field(horizontal, vertical, ConsoleColor.Yellow);
            Player player = new Player(field, ConsoleColor.DarkCyan, rand.Next(1, horizontal - 1), rand.Next(1, vertical - 1));
            field.Draw();
            player.Set();
            player.Move();
        }
    }
}
