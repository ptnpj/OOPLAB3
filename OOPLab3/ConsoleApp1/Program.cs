using System;
using System.Linq;

namespace Lab3_Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.Write("Введіть кількість елементів (n): ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
            {
                Console.WriteLine("Некоректне число.");
                return;
            }

            double[] arr = new double[n];
            Random rnd = new Random();
            double minVal = -7.51;
            double maxVal = 3.59;

            Console.WriteLine("\nЗгенерований масив:");
            for (int i = 0; i < n; i++)
            {
                double val = minVal + rnd.NextDouble() * (maxVal - minVal);
                arr[i] = Math.Round(val, 2);
                Console.Write(arr[i] + "\t");
            }
            Console.WriteLine();

            double sumModules = 0;
            foreach (var item in arr)
            {
                if ((Math.Abs(item) % 1) < 0.5)
                {
                    sumModules += Math.Abs(item);
                }
            }
            Console.WriteLine($"\n1. Сума модулів елементів (дроб. частина < 0.5): {Math.Round(sumModules, 2)}");


            double currentMin = arr[0];
            int minIndex = 0;
            for (int i = 1; i < n; i++)
            {
                if (arr[i] < currentMin)
                {
                    currentMin = arr[i];
                    minIndex = i;
                }
            }
            Console.WriteLine($"\nМінімальний елемент: {currentMin} (індекс {minIndex})");

            if (minIndex < n - 1)
            {
                int lengthToSort = n - (minIndex + 1);
                double[] tempArr = new double[lengthToSort];
                Array.Copy(arr, minIndex + 1, tempArr, 0, lengthToSort);

                Array.Sort(tempArr);
                Array.Reverse(tempArr);

                Array.Copy(tempArr, 0, arr, minIndex + 1, lengthToSort);
            }

            Console.WriteLine("\n2. Масив після сортування частини після мінімуму:");
            foreach (var item in arr)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}