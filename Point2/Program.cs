using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Point A = new Point(2, 3);
            Point B = new Point(7, 8);
            Point C = new Point(B - A);
            ++C;
            C.Print();
        }
    }
}
