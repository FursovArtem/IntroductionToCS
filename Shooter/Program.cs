using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow: case ConsoleKey.W: Console.WriteLine("Up"); break;
                    case ConsoleKey.DownArrow: case ConsoleKey.S: Console.WriteLine("Down"); break;
                    case ConsoleKey.LeftArrow: case ConsoleKey.A: Console.WriteLine("Left"); break;
                    case ConsoleKey.RightArrow: case ConsoleKey.D: Console.WriteLine("Right"); break;
                    case ConsoleKey.Spacebar: Console.WriteLine("Jump"); break;
                    case ConsoleKey.Enter: Console.WriteLine("Fire"); break;
                }
            } while (key != ConsoleKey.Escape);
        }
    }
}
