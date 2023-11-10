//#define ARRAYS_1
//#define ARRAYS_2
#define JAGGED_ARRAYS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Arrays
{
    internal class Program
    {
        static void Main(string[] args)
        {
#if ARRAYS_1
            Console.Write("Введите размер массива: ");
            int n = Convert.ToInt32(Console.ReadLine());
            int[] arr = new int[n];
            Random rand = new Random(0);
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rand.Next(100, 200);
            }
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + "\t");
            }
            Console.WriteLine();

            Console.WriteLine($"Сумма элементов массива: {arr.Sum()}");
            Console.WriteLine($"Среднее-арифметическое элементов массива: {arr.Average()}");
            Console.WriteLine($"Минимальное значение в массиве: {arr.Min()}");
            Console.WriteLine($"Максимальное значение в массиве: {arr.Max()}");
            Array.Sort(arr);
            foreach (int i in arr)
            {
                Console.Write(i + "\t");
            }
            Console.WriteLine(); 
#endif

#if ARRAYS_2
            Random rand = new Random(0);

            Console.Write("Введите количество строк: ");
            int rows = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите количество элементов строки: ");
            int cols = Convert.ToInt32(Console.ReadLine());
            int[,] arr = new int[rows, cols];
            Console.WriteLine($"Количество измерений массива: {arr.Rank}");
            Console.WriteLine($"Количество строк массива: {arr.GetLength(0)}");
            Console.WriteLine($"Количество элементов строки массива: {arr.GetLength(1)}");
            int sum = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    arr[i, j] = rand.Next(100);
                    Console.Write(arr[i, j] + "\t");
                    sum += arr[i, j];
                }
                Console.WriteLine();
            }
            double avg = (double)sum / (rows * cols);
            int min = arr[0, 0], max = arr[0, 0];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (min > arr[i, j]) min = arr[i, j];
                    if (max < arr[i, j]) max = arr[i, j];
                }
            }
            Console.WriteLine($"Сумма = {sum}, минимальное значение = {min}, максимальное значение = {max}, среднее арифметическое = {avg}"); 
#endif

            int[][] arr = new int[][]
                {
                    new int[]{ 0, 1, 1, 2 },
                    new int[]{ 3, 5, 8 },
                    new int[]{ 13, 21 },
                    new int[]{ 34, 55, 89 }
                };
            int sum = 0, length = 0, min = arr[0][0], max = arr[0][0];
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    sum += arr[i][j];
                    length++;
                    if (min > arr[i][j]) min = arr[i][j];
                    if (max < arr[i][j]) max = arr[i][j];
                    Console.Write(arr[i][j] + "\t");
                }
                Console.WriteLine();
            }
            double avg = (double)sum / length;
            Console.WriteLine("Сумма элементов массива: " + sum);
            Console.WriteLine("Минимальный элемент массива: " + min);
            Console.WriteLine("Максимальный элемент массива: " + max);
            Console.WriteLine("Среднее-арифметическое элементов массива: " + avg);
        }
    }
}
