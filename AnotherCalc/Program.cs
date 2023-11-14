using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введие выражение: ");
            string expression = "55+(22+33)/4-5*2*3+3";//Console.ReadLine();
            expression = expression.Replace(".", ",");
            Console.WriteLine(expression);
            String[] numbers = expression.Split('+', '-', '*', '/');
            /*double a = Convert.ToDouble(numbers[0]);
			double b = Convert.ToDouble(numbers[1]);
			if (expression.Contains("+")) Console.WriteLine($"{a} + {b} = {a + b}");
			else if (expression.Contains("-")) Console.WriteLine($"{a} - {b} = {a - b}");
			else if (expression.Contains("*")) Console.WriteLine($"{a} * {b} = {a * b}");
			else if (expression.Contains("/")) Console.WriteLine($"{a} / {b} = {a / b}");*/
            String[] operations = expression.Split('0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ' ', ',');
            operations = operations.Where(val => val != "").ToArray();
            //foreach (String i in numbers) Console.Write(i + "\t");			Console.WriteLine();
            //foreach (String i in operations) Console.Write(i + "\t");		Console.WriteLine();
            double[] values = new double[numbers.Length];
            for (int i = 0; i < numbers.Length; i++)
            {
                values[i] = Convert.ToDouble(numbers[i]);
            }
            foreach (double i in values) Console.Write(i + "\t"); Console.WriteLine();
            foreach (String i in operations) Console.Write(i + "\t"); Console.WriteLine();

            //do
            {
                for (int i = 0; i < operations.Length; i++)
                {
                    if (operations[i] == "*") values[i] *= values[i + 1];
                    if (operations[i] == "/") values[i] /= values[i + 1];
                    if (operations[i] == "*" || operations[i] == "/")
                    {
                        for (int j = i + 1; j < operations.Length; j++)
                        {
                            values[j] = values[j + 1];
                            operations[j - 1] = operations[j];
                        }
                        values[values.Length - 1] = 0;
                        operations[operations.Length - 1] = null;
                        i--;
                    }
                }
            }//while (operations.Contains("*") || operations.Contains("/"));
             //do
            {
                for (int i = 0; i < operations.Length; i++)
                {
                    if (operations[i] == "+") values[i] += values[i + 1];
                    if (operations[i] == "-") values[i] -= values[i + 1];
                    if (operations[i] == "+" || operations[i] == "-")
                    {
                        for (int j = i + 1; j < operations.Length; j++)
                        {
                            values[j] = values[j + 1];
                            operations[j - 1] = operations[j];
                        }
                        values[values.Length - 1] = 0;
                        operations[operations.Length - 1] = null;
                        i--;
                    }
                }
                //if (operations[1] == null) operations[0] = null;
            } //while (operations.Contains("+") || operations.Contains("-"));
            foreach (double i in values) Console.Write(i + "\t"); Console.WriteLine();
            foreach (String i in operations) Console.Write(i + "\t"); Console.WriteLine();
        }
    }
}