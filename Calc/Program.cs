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
            #region OLD_WRITELINE
            /*for (int i = 0; i < signs.Count; i++)
            {
                Console.Write($"{values[i]} {signs[i]} ");
            }
            Console.Write($"{values[values.Count - 1]} = ");*/
            #endregion

        }
        private static string Explorer(string expression)
        {
            string changed_expr = expression;
            for (int i = 0; i < changed_expr.Length; i++)
            {
                if (changed_expr[i] == '(')
                {
                    for (int j = i + 1; j < changed_expr.Length - i; j++)
                    {
                        if (changed_expr[j] == '(')
                        {
                            int index = changed_expr.Length - 1;
                            while (changed_expr[index] != ')') index--;
                            Explorer(changed_expr.Substring(j, index - j)); 
                        }
                        if (changed_expr[j] == ')')
                        {
                            changed_expr = Convert.ToString(Calc(changed_expr.Substring(i + 1, i + j - 1)));
                            return expression.Replace(changed_expr.Substring(i, i + j + 1), changed_expr);
                        }
                    }
                }
            }
            return expression;
        }
        private static double Calc(string expression)
        {
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
            return result;
        }
    }
}