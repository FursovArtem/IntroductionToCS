using System;

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
        private int counter;
        private readonly Field field;
        private readonly char head;
        private readonly char[,] tail;
        private readonly ConsoleColor color;
        private readonly Food food;

        public Player(Field field, ConsoleColor color = ConsoleColor.White, char head = (char)164)
        {
            this.field = field;
            x = field.HeightFirst + 1;
            y = field.WidthFirst + 1;
            this.head = head;
            this.color = color;
            food = new Food(field, this);
            counter = 0;
        }
        public int X { get { return x; } }
        public int Y { get { return y; } }
        //public char[,] Tail { get { return tail; } }

        public void Start()
        {
            field.Draw();
            Counter();
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
        private void FullClear()
        {
            for (int i = field.HeightFirst + 1; i < field.HeightLast - 1; i++)
            {
                for (int j = field.WidthFirst + 1; j < field.WidthLast - 1; j++)
                {
                    Console.Write(field.AField[i, j] = ' ');
                }
            }
        }
        private void Teleport()
        {
            Random rand = new Random();
            Clear();
            x = rand.Next(field.HeightFirst + 1, field.HeightLast - 1);
            y = rand.Next(field.WidthFirst + 1, field.WidthLast - 1);
            Set();
        }
        private void Counter()
        {
            string CounterMessage = "$$$ Counter: ";
            Console.CursorTop = field.HeightLast + 1;
            Console.CursorLeft = (field.WidthFirst + field.WidthLast - CounterMessage.Length - 1) / 2;
            Console.ForegroundColor = food.Color;
            Console.Write(CounterMessage + counter + "   ");
        }
        private bool GameOver()
        {
            bool flag = false;
            if (x == field.HeightFirst || x == field.HeightLast - 1 ||
                y == field.WidthFirst || y == field.WidthLast - 1)
                flag = true;
            return flag;
        }
        public void Move()
        {
            ConsoleKey key;
            do
            {
                if (GameOver())
                {
                    string GameOverMessage = "Вы проиграли. R - reset...";
                    Console.CursorTop = (field.HeightFirst + field.HeightLast) / 2;
                    Console.CursorLeft = (field.WidthFirst + field.WidthLast - GameOverMessage.Length) / 2;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(GameOverMessage);
                    Console.CursorTop = field.HeightLast + 3;
                    Console.ForegroundColor = ConsoleColor.White;
                    key = Console.ReadKey(true).Key;
                    switch (key)
                    {
                        case ConsoleKey.R:
                            {
                                counter = 0;
                                FullClear();
                                Teleport();
                                Start();
                                break;
                            }
                    }
                    if (key != ConsoleKey.R) break;
                }
                if (!Program.CheckIn(field.AField, food.Body))
                {
                    counter++;
                    Counter();
                    food.Draw();
                    Console.ForegroundColor = color;
                }
                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        {
                            if (x > field.HeightFirst)
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
                            if (x < field.HeightLast - 1)
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
                            if (y > field.WidthFirst)
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
                            if (y < field.WidthLast - 1)
                            {
                                Clear();
                                y++;
                                Set();
                            }
                            break;
                        }
                    case ConsoleKey.T: Teleport(); break;
                    case ConsoleKey.R:
                        {
                            counter = 0;
                            FullClear();
                            Teleport();
                            Start();
                            break;
                        }
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
        public ConsoleColor Color { get { return color; } }
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
            int horizontal = 100, vertical = 25, horizontal2 = 20, vertical2 = 5;
            Field field = new Field(horizontal, vertical, horizontal2, vertical2);
            Player player = new Player(field, ConsoleColor.DarkCyan);
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
