using System;
using System.Collections.Generic;

namespace Calc
{
    class ReversePolishNotation
    {
        private readonly List<string> operators;
        private readonly List<string> standart_operators = new List<string>(new string[] { "(", ")", "+", "-", "*", "/", "^" });
        public ReversePolishNotation()
        {
            operators = new List<string>(standart_operators);
        }

        private IEnumerable<string> Parse(string expr)
        {
            int position = 0;
            while (position < expr.Length)
            {
                string s = string.Empty + expr[position];
                if (!standart_operators.Contains(expr[position].ToString()))
                {
                    if (Char.IsDigit(expr[position]))
                    {
                        for (int i = position + 1; i < expr.Length && Char.IsDigit(expr[i]) || expr[i] == ','; i++)
                            s += expr[i];
                    }
                    else if (Char.IsLetter(expr[position]))
                    {
                        for (int i = position + 1; i < expr.Length && Char.IsLetter(expr[i]) || Char.IsDigit(expr[i]); i++)
                            s += expr[i];
                    }
                }
                yield return s;
                position += s.Length;
            }
        }
        private byte GetPriority(string s)
        {
            switch (s)
            {
                case "(":
                case ")":
                    return 0;
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
                case "^":
                    return 3;
                default: return 4;
            }
        }
        public string[] ConvertToPolishNotation(string expr)
        {
            List<string> outParsed = new List<string>();
            Stack<string> stack = new Stack<string>();
            foreach (string c in Parse(expr))
            {
                if (operators.Contains(c))
                {
                    if (stack.Count > 0 && !c.Equals("("))
                    {
                        if (c.Equals(")"))
                        {
                            string s = stack.Pop();
                            while (s != "(")
                            {
                                outParsed.Add(s);
                                s = stack.Pop();
                            }
                        }
                        else if (GetPriority(c) > GetPriority(stack.Peek())) stack.Push(c);
                        else
                        {
                            while (stack.Count > 0 && GetPriority(c) <= GetPriority(stack.Peek()))
                                outParsed.Add(stack.Pop());
                            stack.Push(c);
                        }
                    }
                    else stack.Push(c);
                }
                else outParsed.Add(c);
            }
            if (stack.Count > 0)
            {
                foreach (string c in stack) outParsed.Add(c);
            }
            return outParsed.ToArray();
        }
        public decimal Calc(string expr)
        {
            Stack<string> stack = new Stack<string>();
            Queue<string> queue = new Queue<string>(ConvertToPolishNotation(expr));
            string str = queue.Dequeue();
            while (queue.Count >= 0)
            {
                if (!operators.Contains(str))
                {
                    stack.Push(str);
                    str = queue.Dequeue();
                }
                else
                {
                    decimal sum = 0;
                    switch (str)
                    {
                        case "+":
                            {
                                decimal a = Convert.ToDecimal(stack.Pop());
                                decimal b = Convert.ToDecimal(stack.Pop());
                                sum = a + b;
                                break;
                            }
                        case "-":
                            {
                                decimal a = Convert.ToDecimal(stack.Pop());
                                decimal b = Convert.ToDecimal(stack.Pop());
                                sum = b - a;
                                break;
                            }
                        case "*":
                            {
                                decimal a = Convert.ToDecimal(stack.Pop());
                                decimal b = Convert.ToDecimal(stack.Pop());
                                sum = a * b;
                                break;
                            }
                        case "/":
                            {
                                decimal a = Convert.ToDecimal(stack.Pop());
                                decimal b = Convert.ToDecimal(stack.Pop());
                                sum = b / a;
                                break;
                            }
                        case "^":
                            {
                                decimal a = Convert.ToDecimal(stack.Pop());
                                decimal b = Convert.ToDecimal(stack.Pop());
                                sum = Convert.ToDecimal(Math.Pow(Convert.ToDouble(b), Convert.ToDouble(a)));
                                break;
                            }
                    }
                    stack.Push(sum.ToString());
                    if (queue.Count > 0) str = queue.Dequeue();
                    else break;
                }
            }
            return Convert.ToDecimal(stack.Pop());
        }
    }
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
            if (expression.Contains("(") || expression.Contains(")"))
            {
                ReversePolishNotation rpn = new ReversePolishNotation();
                Console.WriteLine(rpn.Calc(expression));
            }
            else Console.WriteLine(Calc(expression));
        }
        #region OLD_CALC
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
        #endregion
    }
}