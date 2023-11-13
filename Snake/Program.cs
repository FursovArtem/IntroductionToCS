using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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

        private int widthFirst;
        private int widthLast;
        private int heightFirst;
        private int heightLast;
        private char[,] field;
        private readonly ConsoleColor borderColor;

        public Field(int widthLast = 100, int heightLast = 25, int widthFirst = 0, int heightFirst = 0,
                    ConsoleColor borderColor = ConsoleColor.Magenta)
        {
            if (widthLast >= 4) this.widthLast = widthLast;
            else this.widthLast = 4;
            if (heightLast >= 3) this.heightLast = heightLast;
            else this.heightLast = 3;
            if (widthFirst >= widthLast) this.widthFirst = this.widthLast - 4;
            else this.widthFirst = widthFirst;
            if (heightFirst >= heightLast) this.heightFirst = this.heightLast - 3;
            else this.heightFirst = heightFirst;
            this.borderColor = borderColor;
            this.field = new char[this.heightLast, this.widthLast];
        }
        public int WidthLast
        {
            get { return widthLast; }
            set { widthLast = value; }
        }
        public int HeightLast
        {
            get { return heightLast; }
            set { heightLast = value; }
        }
        public int WidthFirst
        {
            get { return widthFirst; }
            set { widthFirst = value; }
        }
        public int HeightFirst
        {
            get { return heightFirst; }
            set { heightFirst = value; }
        }
        public char[,] AField
        {
            get { return field; }
            set { field = value; }
        }
        public void Draw()
        {
            Console.ForegroundColor = borderColor;
            Console.CursorTop = heightFirst;
            Console.CursorLeft = widthFirst;
            for (int i = heightFirst; i < heightLast; i++)
            {
                for (int j = widthFirst; j < widthLast; j++)
                {
                    if (i == heightFirst && j == widthFirst) Console.Write(field[i, j] = UpLeftCorner);
                    else if (i == heightFirst && j == widthLast - 1) Console.Write(field[i, j] = UpRightCorner);
                    else if (i == heightLast - 1 && j == widthFirst) Console.Write(field[i, j] = DownLeftCorner);
                    else if (i == heightLast - 1 && j == widthLast - 1) Console.Write(field[i, j] = DownRightCorner);
                    else if (i == heightFirst || i == heightLast - 1) Console.Write(field[i, j] = Horizontal);
                    else if (j == widthFirst || j == widthLast - 1) Console.Write(field[i, j] = Vertical);
                    else Console.Write(field[i, j] = ' ');
                }
                Console.WriteLine();
                Console.CursorLeft = widthFirst;
            }
            Console.CursorTop = heightFirst;
            Console.CursorLeft = widthFirst;
        }
    }
    public class Player
    {
        private int x;
        private int y;
        private readonly Field field;
        private readonly char head;
        private readonly char[,] tail;
        private readonly ConsoleColor color;
        private readonly Food food;

        public Player(Field field, ConsoleColor color = default, char head = (char)164)
        {
            this.field = field;
            x = field.HeightFirst + 1;
            y = field.WidthFirst + 1;
            this.head = head;
            this.color = color;
            food = new Food(field, this);
        }
        public int X { get { return x; } }
        public int Y { get { return y; } }
        //public char[,] Tail { get { return tail; } }

        public void Start()
        {
            Console.ForegroundColor = color;
            Console.CursorVisible = false;
            Teleport();
            food.Draw();
            Console.ForegroundColor = color;
        }
        private void Set()
        {
            Console.CursorTop = x;
            Console.CursorLeft = y;
            Console.Write(field.AField[x, y] = head);
        }
        private void Clear()
        {
            Console.CursorTop = x;
            Console.CursorLeft = y;
            Console.Write(field.AField[x, y] = ' ');
        }
        private void Teleport()
        {
            Random rand = new Random();
            Clear();
            x = rand.Next(field.HeightFirst + 1, field.HeightLast - 1);
            y = rand.Next(field.WidthFirst + 1, field.WidthLast - 1);
            Set();
        }
        public void Move()
        {
            ConsoleKey key;
            do
            {
                if (!Program.CheckIn(field.AField, food.Body))
                {
                    food.Draw();
                    Console.ForegroundColor = color;
                }
                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        {
                            if (x > field.HeightFirst + 1)
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
                            if (x < field.HeightLast - 2)
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
                            if (y > field.WidthFirst + 1)
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
                            if (y < field.WidthLast - 2)
                            {
                                Clear();
                                y++;
                                Set();
                            }
                            break;
                        }
                    case ConsoleKey.T:
                        Teleport();
                        break;
                }
            } while (key != ConsoleKey.Escape);
        }
    }
    public class Food
    {
        private readonly Field field;
        private readonly Player player;
        private readonly char body;
        private readonly ConsoleColor color;

        public Food(Field field, Player player, ConsoleColor color = ConsoleColor.Green, char body = (char)36)
        {
            this.field = field;
            this.player = player;
            this.color = color;
            this.body = body;
            this.color = color;
        }
        public char Body { get { return body; } }
        public void Draw()
        {
            Random rand = new Random();
            bool flag = true;
            do
            {
                int x = rand.Next(field.HeightFirst + 1, field.HeightLast - 1);
                int y = rand.Next(field.WidthFirst + 1, field.WidthLast - 1);
                if (!(x == player.X && y == player.Y))
                {
                    Console.ForegroundColor = color;
                    Console.CursorTop = x;
                    Console.CursorLeft = y;
                    Console.Write(field.AField[x, y] = body);
                    flag = false;
                }
            } while (flag);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            int horizontal = 50, vertical = 20, left_offset = 10, top_offset = 3;
            Field field = new Field(horizontal, vertical, left_offset, top_offset);
            Player player = new Player(field);
            field.Draw();
            player.Start();
            player.Move();
        }
        public static bool CheckIn(char[,] array, char value)
        {
            bool flag = false;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == value) { flag = true; break; }
                }
            }
            return flag;
        }
    }
}
