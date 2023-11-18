using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fraction
{
    internal class Fraction
    {
        int denominator;
        public int Integer { get; set; }
        public int Numerator { get; set; }
        public int Denominator
        {
            get { return denominator; }
            set
            {
                if (value == 0) value = 1;
                denominator = value;
            }
        }
        public Fraction()
        {
            Integer = 0;
            Numerator = 0;
            Denominator = 1;
            Console.WriteLine("DefConstructor:\t" + GetHashCode());
        }
        public Fraction(int integer)
        {
            Integer = integer;
            Numerator = 0;
            Denominator = 1;
            Console.WriteLine("1ArgConstructor:\t" + GetHashCode());
        }
        public Fraction(int numerator, int denominator)
        {
            Integer = 0;
            Numerator = numerator;
            Denominator = denominator;
            Console.WriteLine("2ArgConstructor:\t" + GetHashCode());
        }
        public Fraction(int integer = 0, int numerator = 0, int denominator = 1)
        {
            Integer = integer;
            Numerator = numerator;
            Denominator = denominator;
            Console.WriteLine("Constructor:\t" + GetHashCode());
        }
        public Fraction(Fraction other)
        {
            this.Integer = other.Integer;
            this.Numerator = other.Numerator;
            this.Denominator = other.Denominator;
            Console.WriteLine("CopyConstructor:\t"+GetHashCode());
        }
        ~Fraction()
        {
            Console.WriteLine("Finalizer:\t" + GetHashCode());
        }
        // OPERATORS
        public static Fraction operator *(Fraction left_operand, Fraction right_operand)
        {
            Fraction left = new Fraction(left_operand);
            Fraction right = new Fraction(right_operand);
            left.ToImproper();
            right.ToImproper();
            return new Fraction(left.Numerator * right.Numerator, left.Denominator * right.Denominator);
        }
        public static Fraction operator /(Fraction left, Fraction right)
            => left * right.Inverted();
        // METHODS
        public Fraction ToImproper()
        {
            Numerator += Integer * Denominator;
            Integer = 0;
            return this;
        }
        public Fraction ToProper()
        {
            Integer = Numerator / Denominator;
            Numerator %= Denominator;
            return this;
        }
        public Fraction Inverted()
        {
            Fraction inverted = new Fraction(this);
            inverted.ToImproper();
            int buffer = inverted.Numerator;
            inverted.Numerator = inverted.Denominator;
            inverted.Denominator = buffer;
            return inverted;
        }
        public void Print()
        {
            if (Integer != 0) Console.Write(Integer);
            if (Numerator != 0)
            {
                if (Integer != 0) Console.Write("(");
                Console.Write($"{Numerator}/{Denominator}");
                if (Integer != 0) Console.Write(")");
            }
            else if (Integer == 0) Console.Write(0);
            Console.WriteLine();
        }
    }
}
