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
            double result = 0;
            int it = 0;
            while (it < signs.Count)
            {
                if (signs[it] == '*')
                {
                    result = values[it] * values[it + 1];
                    values[it + 1] = result;
                    values.RemoveAt(it);
                    signs.RemoveAt(it);
                }
                else if (signs[it] == '/')
                {
                    result = values[it] / values[it + 1];
                    values[it + 1] = result;
                    values.RemoveAt(it);
                    signs.RemoveAt(it);
                }
                else it++;
            }
            it = 0;
            while (it < signs.Count)
            {
                if (signs[it] == '+')
                {
                    result = values[it] + values[it + 1];
                    values[it + 1] = result;
                    values.RemoveAt(it);
                    signs.RemoveAt(it);
                }
                else if (signs[it] == '-')
                {
                    result = values[it] - values[it + 1];
                    values[it + 1] = result;
                    values.RemoveAt(it);
                    signs.RemoveAt(it);
                }
                else it++;
            }
            Console.WriteLine(result);
        }
    }
}