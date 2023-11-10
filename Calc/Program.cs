using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calc
{
    internal class Program
    {
        static void Calc(ref List<double> values, ref List<char> signs)
        {
            double result;
            int i = 0;
            while (i < signs.Count)
            {
                if (signs[i] == '*')
                {
                    result = values[i] * values[i + 1];
                    values[i + 1] = result;
                    values.RemoveAt(i);
                    signs.RemoveAt(i);
                }
                else if (signs[i] == '/')
                {
                    result = values[i] / values[i + 1];
                    values[i + 1] = result;
                    values.RemoveAt(i);
                    signs.RemoveAt(i);
                }
                else i++;
            }
        }
        static double Calc(List<double> values, List<char> signs)
        {
            double result = 0;
            int i = 0;
            while (i < signs.Count)
            {
                if (signs[i] == '+')
                {
                    result = values[i] + values[i + 1];
                    values[i + 1] = result;
                    values.RemoveAt(i);
                    signs.RemoveAt(i);
                }
                else if (signs[i] == '-')
                {
                    result = values[i] - values[i + 1];
                    values[i + 1] = result;
                    values.RemoveAt(i);
                    signs.RemoveAt(i);
                }
                else i++;
            }
            return result;
        }
        static void Main(string[] args)
        {
            Console.Write("Введите выражение: ");
            string expression = Console.ReadLine().Replace(".", ",");
            String[] numbers = expression.Split('+', '-', '*', '/');
            List<double> values = new List<double>();
            List<char> signs = new List<char>();
            for (int i = 0; i < numbers.Length; i++) values.Add(Convert.ToDouble(numbers[i]));
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '+') signs.Add('+');
                if (expression[i] == '-') signs.Add('-');
                if (expression[i] == '*') signs.Add('*');
                if (expression[i] == '/') signs.Add('/');
            }

            for (int i = 0; i < signs.Count; i++)
            {
                Console.Write($"{values[i]} {signs[i]} ");
            }
            Console.Write($"{values[values.Count - 1]} = ");
            Calc(ref values, ref signs);
            double result = Calc(values, signs);
            Console.WriteLine(result);
        }
    }
}