using System;
using System.Net.Security;

namespace Snake
{
    public class Field
    {
        public const char UpLeftCorner = '╔';
        public const char DownLeftCorner = '╚';
        public const char UpRightCorner = '╗';
        public const char DownRightCorner = '╝';
        public const char Vertical = '║';
        public const char Horizontal = '═';

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
        private readonly char head;
        private readonly char tail;
        private int tailCount;
        private ConsoleKey prevKey;
        private readonly ConsoleColor color;
        private readonly Field field;
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
            tail = '*';
            tailCount = 0;
        }
        public int X { get { return x; } }
        public int Y { get { return y; } }

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
        private bool GameOverCheck()
        {
            bool flag = false;
            if (x == field.HeightFirst || x == field.HeightLast - 1 ||
                y == field.WidthFirst || y == field.WidthLast - 1 ||
                field.AField[x, y] == tail || field.AField[x, y] == tail)
                flag = true;
            return flag;
        }
        private void GameOver()
        {
            ConsoleKey key;
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
                        tailCount = 0;
                        prevKey = ConsoleKey.Clear;
                        FullClear();
                        Teleport();
                        Start();
                        break;
                    }
                default: Environment.Exit(0); break;
            }
        }
        private void Tail(int x, int y)
        {
            Console.CursorTop = x;
            Console.CursorLeft = y;
            Console.Write(field.AField[x, y] = tail);
        }
        private void TailMoveUp(int x, int y)
        {
            int i = x + 1, c = 0;
            while (c < tailCount)
            {
                if (field.AField[i, y] == tail)
                {
                    break;
                }
                Console.CursorTop = i;
                Console.CursorLeft = y;
                Console.Write(field.AField[i, y] = tail);
                i++; c++;
                Console.CursorTop = i;
                Console.CursorLeft = y;
                Console.Write(field.AField[i, y] = ' ');
            }
        }
        private void TailMoveDown(int x, int y)
        {
            int i = x - 1, c = 0;
            while (c < tailCount)
            {
                if (field.AField[i, y] == tail)
                {
                    break;
                }
                Console.CursorTop = i;
                Console.CursorLeft = y;
                Console.Write(field.AField[i, y] = tail);
                i--; c++;
                Console.CursorTop = i;
                Console.CursorLeft = y;
                Console.Write(field.AField[i, y] = ' ');
            }
        }
        private void TailMoveLeft(int x, int y)
        {
            int i = y + 1, c = 0;
            while (c < tailCount)
            {
                if (field.AField[x, i] == tail)
                {
                    break;
                }
                Console.CursorTop = x;
                Console.CursorLeft = i;
                Console.Write(field.AField[x, i] = tail);
                i++; c++;
                Console.CursorTop = x;
                Console.CursorLeft = i;
                Console.Write(field.AField[x, i] = ' ');
            }
        }
        private void TailMoveRight(int x, int y)
        {
            int i = y - 1, c = 0;
            while (c < tailCount)
            {
                if (field.AField[x, i] == tail)
                {
                    break;
                }
                Console.CursorTop = x;
                Console.CursorLeft = i;
                Console.Write(field.AField[x, i] = tail);
                i--; c++;
                Console.CursorTop = x;
                Console.CursorLeft = i;
                Console.Write(field.AField[x, i] = ' ');
            }
        }
        private void RotateUp(int x, int y)
        {
            if (tailCount > 0)
            {
                if (prevKey == ConsoleKey.D || prevKey == ConsoleKey.RightArrow ||
                    prevKey == ConsoleKey.A || prevKey == ConsoleKey.LeftArrow)
                {
                    Console.CursorTop = x;
                    Console.CursorLeft = y;
                    Console.Write(field.AField[x, y] = tail);
                }
            }
        }
        private void RotateDown(int x, int y)
        {
            if (tailCount > 0)
            {
                if (prevKey == ConsoleKey.D || prevKey == ConsoleKey.RightArrow ||
                    prevKey == ConsoleKey.A || prevKey == ConsoleKey.LeftArrow)
                {
                    Console.CursorTop = x;
                    Console.CursorLeft = y;
                    Console.Write(field.AField[x, y] = tail);
                }
            }
        }
        private void RotateLeft(int x, int y)
        {
            if (tailCount > 0)
            {
                if (prevKey == ConsoleKey.W || prevKey == ConsoleKey.UpArrow ||
                    prevKey == ConsoleKey.S || prevKey == ConsoleKey.DownArrow)
                {
                    Console.CursorTop = x;
                    Console.CursorLeft = y;
                    Console.Write(field.AField[x, y] = tail);
                }
            }
        }
        private void RotateRight(int x, int y)
        {
            if (tailCount > 0)
            {
                if (prevKey == ConsoleKey.W || prevKey == ConsoleKey.UpArrow ||
                    prevKey == ConsoleKey.S || prevKey == ConsoleKey.DownArrow)
                {
                    Console.CursorTop = x;
                    Console.CursorLeft = y;
                    Console.Write(field.AField[x, y] = tail);
                }
            }
        }
        public void Move()
        {
            ConsoleKey key;
            do
            {
                if (GameOverCheck())
                {
                    GameOver();
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
                            if (prevKey == ConsoleKey.S || prevKey == ConsoleKey.DownArrow) break;
                            if (x > field.HeightFirst)
                            {
                                Clear();
                                RotateUp(x, y);
                                x--;
                                if (GameOverCheck())
                                {
                                    Set();
                                    TailMoveUp(x, y);
                                    GameOver();
                                    break;
                                }
                                Set();
                                TailMoveUp(x, y);
                                if (x == food.X && y == food.Y)
                                {
                                    Tail(x + 1, y);
                                    tailCount++;
                                }
                            }
                            prevKey = key;
                            break;
                        }
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        {
                            if (prevKey == ConsoleKey.W || prevKey == ConsoleKey.UpArrow) break;
                            if (x < field.HeightLast - 1)
                            {
                                Clear();
                                RotateDown(x, y);
                                x++;
                                if (GameOverCheck())
                                {
                                    Set();
                                    TailMoveDown(x, y);
                                    GameOver();
                                    break;
                                }
                                Set();
                                TailMoveDown(x, y);
                                if (x == food.X && y == food.Y)
                                {
                                    Tail(x - 1, y);
                                    tailCount++;
                                }
                            }
                            prevKey = key;
                            break;
                        }
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        {
                            if (prevKey == ConsoleKey.D || prevKey == ConsoleKey.RightArrow) break;
                            if (y > field.WidthFirst)
                            {
                                Clear();
                                RotateLeft(x, y);
                                y--;
                                if (GameOverCheck())
                                {
                                    Set();
                                    TailMoveLeft(x, y);
                                    GameOver();
                                    break;
                                }
                                Set();
                                TailMoveLeft(x, y);
                                if (x == food.X && y == food.Y)
                                {
                                    Tail(x, y + 1);
                                    tailCount++;
                                }
                            }
                            prevKey = key;
                            break;
                        }
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        {
                            if (prevKey == ConsoleKey.A || prevKey == ConsoleKey.LeftArrow) break;
                            if (y < field.WidthLast - 1)
                            {
                                Clear();
                                RotateRight(x, y);
                                y++;
                                if (GameOverCheck())
                                {
                                    Set();
                                    TailMoveRight(x, y);
                                    GameOver();
                                    break;
                                }
                                Set();
                                TailMoveRight(x, y);
                                if (x == food.X && y == food.Y)
                                {
                                    Tail(x, y - 1);
                                    tailCount++;
                                }
                            }
                            prevKey = key;
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
        private int x;
        private int y;
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
        public int X { get { return x; } }
        public int Y { get { return y; } }
        public void Draw()
        {
            Random rand = new Random();
            bool flag = true;
            do
            {
                x = rand.Next(field.HeightFirst + 1, field.HeightLast - 1);
                y = rand.Next(field.WidthFirst + 1, field.WidthLast - 1);
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
