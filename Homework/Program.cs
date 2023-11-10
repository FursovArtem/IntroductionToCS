//#define T1
//#define T2
//#define T3
//#define T4
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    internal class Program
    {
        static void Main(string[] args)
        {
#if T1
            Console.WriteLine("Преобразование числа в денежный формат.");
            Console.Write("Введите дробное число -> ");
            string num = Console.ReadLine().Replace('.', ',');
            string[] split = num.Split(',');
            decimal n = Convert.ToDecimal(num);
            Console.WriteLine($"{n} грн. - это {split[0]} грн. {split[1]} коп.");
#endif

#if T2
            Console.WriteLine("Вычисление стоимости покупки.");
            Console.WriteLine("Введите исходные данные: ");
            Console.Write("Цена тетради (грн.) -> ");
            decimal note_cost = Convert.ToDecimal(Console.ReadLine().Replace('.', ','));
            Console.Write("Количество тетрадей -> ");
            int note_amount = Convert.ToInt32(Console.ReadLine());
            Console.Write("Цена карандаша (грн.) -> ");
            decimal pen_cost = Convert.ToDecimal(Console.ReadLine().Replace('.', ','));
            Console.Write("Количество карандашей -> ");
            int pen_amount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Стоимость покупки: {note_cost * note_amount + pen_cost * pen_amount} грн."); 
#endif

#if T3
            Console.WriteLine("Вычисление стоимости покупки.");
            Console.WriteLine("Введите исходные данные: ");
            Console.Write("Цена тетради (грн.) -> ");
            decimal note_cost = Convert.ToDecimal(Console.ReadLine().Replace('.', ','));
            Console.Write("Цена обложки (грн.) -> ");
            decimal cover_cost = Convert.ToDecimal(Console.ReadLine().Replace('.', ','));
            Console.Write("Количество комплектов (шт.) -> ");
            int amount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Стоимость покупки: {(note_cost + cover_cost) * amount} грн.");
#endif

#if T4
            Console.WriteLine("Вычисление стоимости поездки на дачу и обратно.");
            Console.Write("Расстояние до дачи (км) -> ");
            double km = Convert.ToDouble(Console.ReadLine().Replace('.', ','));
            Console.Write("Расход бензина (литров на 100 км пробега) -> ");
            double consumption = Convert.ToDouble(Console.ReadLine().Replace('.', ','));
            Console.Write("Цена литра бензина (грн.) -> ");
            decimal cost = Convert.ToDecimal(Console.ReadLine().Replace('.', ','));
            Console.WriteLine($"Поездка на дачу и обратно обойдется в {(decimal)(consumption / 100 * km) * cost * 2} грн.");
#endif
        }
    }
}
