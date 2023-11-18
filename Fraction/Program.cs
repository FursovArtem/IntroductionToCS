//#define CONSTRUCTORS_CHECK
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fraction
{
    internal class Program
    {
        static void Main(string[] args)
        {
#if CONSTRUCTORS_CHECK
            Fraction A = new Fraction();
            A.Print();
            Fraction B = new Fraction(5);
            B.Print();
            Fraction C = new Fraction(1, 2);
            C.Print();
            Fraction D = new Fraction(2, 3, 4);
            D.Print(); 
#endif
            Fraction A = new Fraction(2, 3, 4);
            Fraction B = new Fraction(3, 4, 5);
            A.Print();
            B.Print();
            Fraction C = new Fraction(A * B);
            C.Print();
            new Fraction(A / B).Print();
        }
    }
}
