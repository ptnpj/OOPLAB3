using System;

namespace Lab3_Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.Write("Введіть кількість рядків (n): ");
            int n = int.Parse(Console.ReadLine());
            Console.Write("Введіть кількість стовпців (m): ");
            int m = int.Parse(Console.ReadLine());

            double[,] matrix = new double[n, m];
            Random rnd = new Random();
            double rangeMin = -42.31;
            double rangeMax = 7.03;

            Console.WriteLine("\nПочаткова матриця:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    double val = rangeMin + rnd.NextDouble() * (rangeMax - rangeMin);
                    matrix[i, j] = Math.Round(val, 2);
                    Console.Write($"{matrix[i, j],8}"); 
                }
                Console.WriteLine();
            }

            int rowsWithoutNegatives = 0;
            for (int i = 0; i < n; i++)
            {
                bool hasNegative = false;
                for (int j = 0; j < m; j++)
                {
                    if (matrix[i, j] < 0)
                    {
                        hasNegative = true;
                        break;
                    }
                }
                if (!hasNegative)
                {
                    rowsWithoutNegatives++;
                }
            }
            Console.WriteLine($"\n1. Кількість рядків без від’ємних елементів: {rowsWithoutNegatives}");

            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n / 2; i++)
                {
                    double temp = matrix[i, j];
                    matrix[i, j] = matrix[n - 1 - i, j];
                    matrix[n - 1 - i, j] = temp;
                }
            }

            Console.WriteLine("\n2. Матриця після реверсу стовпців:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write($"{matrix[i, j],8}");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}