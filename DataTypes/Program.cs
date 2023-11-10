using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"bool: {sizeof(bool)}, values: {true} or {false}");
            Console.WriteLine($"char: {sizeof(char)}"); //unicode
            //short, ushort     2 Byte;
            //int, uint         4 Byte;
            //long, ulong       8 Byte;
            Console.WriteLine($"short занимает {sizeof(short)} байт памяти и принимает значения в диапазоне: {short.MinValue} ... {short.MaxValue}");
            //float             4 Byte, 38 знаков после запятой;
            //double            8 Byte, 308 знаков после запятой;
            //decimal           16 Byte, 28 знаков после запятой;
            //decimal в отличии от остальных вещественных типов является предельно точным
            //это единственный вещественный тип, который подходит для работы с деньгами,
            //поскольку float и double часто хранят неточные значения.

            Console.WriteLine('+'.GetType());
            Console.WriteLine(5.GetType());
            Console.WriteLine(5.5.GetType());
            Console.WriteLine(5.5f.GetType());
            Console.WriteLine(5.5m.GetType()); //m - Money
        }
    }
}
