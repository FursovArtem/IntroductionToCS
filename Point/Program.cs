using System;

namespace Point
{
    class Point
    {
        private int x;
        private int y;
        private readonly char body;
        private ConsoleColor color;
        public Point(int x = 0, int y = 0, char body = '*', ConsoleColor color = ConsoleColor.White)
        {
            this.x = x;
            this.y = y;
            this.body = body;
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
        public ConsoleColor Color
        {
            get { return color; }
            set { color = value; }
        }
        public void Clear()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(' ');
        }
        public void Set()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(body);
        }
        public void Teleport()
        {
            Random rand = new Random();
            //Clear();
            X = rand.Next(0, 120);
            Y = rand.Next(0, 30);
            Console.SetCursorPosition(X, Y);
            Console.Write(body);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            ConsoleKey key;
            Point point = new Point(60, 15, '$');
            do
            {
                Console.ForegroundColor = point.Color;
                point.Set();
                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        {
                            point.Clear();
                            point.Y--;
                            break;
                        }
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        {
                            point.Clear();
                            point.Y++;
                            break;
                        }
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        {
                            point.Clear();
                            point.X--;
                            break;
                        }
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        {
                            point.Clear();
                            point.X++;
                            break;
                        }
                    case ConsoleKey.T:
                        {
                            point.Teleport();
                            break;
                        }
                    case ConsoleKey.Spacebar:
                        {
                            do { point.Color = RandomColor(); } while (point.Color == default);
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            Environment.Exit(0);
                            break;
                        }
                }
            } while (true);
        }
        private static ConsoleColor RandomColor()
        {
            Random rand = new Random();
            var consoleColors = Enum.GetValues(typeof(ConsoleColor));
            return (ConsoleColor)consoleColors.GetValue(rand.Next(consoleColors.Length));
        }
    }
}
