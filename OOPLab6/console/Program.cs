using System;
using SimpleClassLibrary;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Worker[] workers = null;

            while (true)
            {
                Console.WriteLine("\n--- МЕНЮ (Лб 7) ---");
                Console.WriteLine("1. Ввести дані про працівників");
                Console.WriteLine("2. Вивести інформацію про всіх працівників");
                Console.WriteLine("0. Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        workers = CreateWorkersArray();
                        break;
                    case "2":
                        PrintAllWorkers(workers);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }
            }
        }

        public static Worker[] CreateWorkersArray()
        {
            Console.Write("Введіть кількість працівників: ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
            {
                Console.WriteLine("Некоректне число.");
                return new Worker[0];
            }

            Worker[] newWorkers = new Worker[n];

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\n--- Працівник #{i + 1} ---");
                Console.Write("ПІБ: ");
                string name = Console.ReadLine();

                Console.Write("Місто проживання: ");
                string city = Console.ReadLine();

                Console.Write("Дата початку роботи (рррр-мм-дд): ");
                DateTime date;
                while (!DateTime.TryParse(Console.ReadLine(), out date))
                {
                    Console.Write("Невірний формат дати (рррр-мм-дд): ");
                }

                Console.WriteLine("- Компанія -");
                Console.Write("Назва: "); string compName = Console.ReadLine();
                Console.Write("Місто офісу: "); string officeCity = Console.ReadLine();
                Console.Write("Посада: "); string position = Console.ReadLine();
                Console.Write("Зарплата: "); decimal salary = decimal.Parse(Console.ReadLine());
                Console.Write("Повний день (y/n)? "); bool isFullTime = Console.ReadLine().ToLower() == "y";

                Company company = new Company(compName, officeCity, position, salary, isFullTime);
                Worker worker = new Worker(name, city, date, company);

                Console.WriteLine("\n--- Введення премії ---");
                Console.Write("Введіть суму премії: ");
                decimal bonusAmount;
                while (!decimal.TryParse(Console.ReadLine(), out bonusAmount) || bonusAmount < 0)
                {
                    Console.Write("Будь ласка, введіть коректну суму: ");
                }

                Console.WriteLine("Оберіть валюту введення:");
                Console.WriteLine("1 - Гривня (UAH)");
                Console.WriteLine("2 - Долар (USD)");
                Console.WriteLine("3 - Євро (EUR)");
                Console.Write("Ваш вибір: ");

                int currencyChoice;
                int.TryParse(Console.ReadLine(), out currencyChoice);

                worker.SetBonus(bonusAmount, currencyChoice);

                newWorkers[i] = worker;
            }

            Console.WriteLine("Масив сформовано!");
            return newWorkers;
        }

        public static void PrintAllWorkers(Worker[] workers)
        {
            if (workers == null || workers.Length == 0)
            {
                Console.WriteLine("Список порожній.");
                return;
            }

            Console.WriteLine("\n=== СПИСОК ПРАЦІВНИКІВ ===");
            foreach (var w in workers)
            {
                Console.WriteLine("----------------");
                Console.WriteLine(w.ToString());
            }
        }
    }
}