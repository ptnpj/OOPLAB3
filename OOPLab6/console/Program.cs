using System;
using System.Collections.Generic;

namespace Lab6_Variant6
{
    public class Company
    {
        public string Name { get; set; }
        public string MainOfficeCity { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public bool IsFullTimeEmployee { get; set; }

        public Company()
        {
            Name = "Unknown";
            MainOfficeCity = "Unknown";
            Position = "Intern";
            Salary = 0;
            IsFullTimeEmployee = false;
        }

        public Company(string name, string city, string position, decimal salary, bool fullTime)
        {
            Name = name;
            MainOfficeCity = city;
            Position = position;
            Salary = salary;
            IsFullTimeEmployee = fullTime;
        }

        public Company(string name, string position)
        {
            Name = name;
            MainOfficeCity = "Kyiv";
            Position = position;
            Salary = 10000;
            IsFullTimeEmployee = true;
        }

        public Company(Company other)
        {
            Name = other.Name;
            MainOfficeCity = other.MainOfficeCity;
            Position = other.Position;
            Salary = other.Salary;
            IsFullTimeEmployee = other.IsFullTimeEmployee;
        }

        public override string ToString()
        {
            return $"Компанія: {Name}, Офіс: {MainOfficeCity}, Посада: {Position}, ЗП: {Salary}, Повний день: {(IsFullTimeEmployee ? "Так" : "Ні")}";
        }
    }

    public class Worker
    {
        public string FullName { get; set; }
        public string HomeCity { get; set; }
        public DateTime StartDate { get; set; }
        public Company WorkPlace { get; set; }

        public Worker()
        {
            FullName = "Невідомо";
            HomeCity = "Невідомо";
            StartDate = DateTime.Now;
            WorkPlace = new Company();
        }

        public Worker(string fullName, string homeCity, DateTime startDate, Company workPlace)
        {
            FullName = fullName;
            HomeCity = homeCity;
            StartDate = startDate;
            WorkPlace = workPlace;
        }

        public Worker(string fullName, string homeCity)
        {
            FullName = fullName;
            HomeCity = homeCity;
            StartDate = DateTime.Now;
            WorkPlace = new Company();
        }

        public Worker(Worker other)
        {
            FullName = other.FullName;
            HomeCity = other.HomeCity;
            StartDate = other.StartDate;
            WorkPlace = new Company(other.WorkPlace);
        }

        public int GetWorkExperience()
        {
            DateTime now = DateTime.Now;
            return ((now.Year - StartDate.Year) * 12) + now.Month - StartDate.Month;
        }

        public bool LivesNotFarFromTheMainOffice()
        {
            if (WorkPlace == null) return false;
            return string.Equals(HomeCity, WorkPlace.MainOfficeCity, StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return $"ПІБ: {FullName}\n" +
                   $"Місто проживання: {HomeCity}\n" +
                   $"Дата початку: {StartDate.ToShortDateString()} (Стаж міс.: {GetWorkExperience()})\n" +
                   $"Живе поруч з офісом: {(LivesNotFarFromTheMainOffice() ? "Так" : "Ні")}\n" +
                   $"[Інфо про роботу] -> {WorkPlace}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Worker[] workers = null;

            while (true)
            {
                Console.WriteLine("\n--- МЕНЮ ---");
                Console.WriteLine("1. Ввести дані про працівників (створити масив)");
                Console.WriteLine("2. Вивести інформацію про всіх працівників");
                Console.WriteLine("3. Вивести інформацію про конкретного працівника");
                Console.WriteLine("4. Демонстрація конструктора копіювання");
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
                    case "3":
                        PrintSpecificWorker(workers);
                        break;
                    case "4":
                        DemoCopyConstructor(workers);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                        break;
                }
            }
        } // Тут метод Main закінчується, але клас Program ще ТРИВАЄ

        

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
                    Console.Write("Невірний формат дати. Спробуйте ще раз (рррр-мм-дд): ");
                }

                Console.WriteLine("--- Дані про компанію ---");
                Console.Write("Назва компанії: ");
                string compName = Console.ReadLine();

                Console.Write("Місто головного офісу: ");
                string officeCity = Console.ReadLine();

                Console.Write("Посада: ");
                string position = Console.ReadLine();

                Console.Write("Зарплата: ");
                decimal salary;
                decimal.TryParse(Console.ReadLine(), out salary);

                Console.Write("Повний робочий день (y/n)? ");
                bool isFullTime = Console.ReadLine().ToLower() == "y";

                Company company = new Company(compName, officeCity, position, salary, isFullTime);
                newWorkers[i] = new Worker(name, city, date, company);
            }

            Console.WriteLine("Масив сформовано!");
            return newWorkers;
        }

        public static void PrintAllWorkers(Worker[] workers)
        {
            if (workers == null || workers.Length == 0)
            {
                Console.WriteLine("Список порожній. Спочатку введіть дані.");
                return;
            }

            Console.WriteLine("\n=== СПИСОК ПРАЦІВНИКІВ ===");
            for (int i = 0; i < workers.Length; i++)
            {
                Console.WriteLine($"\nID: {i}");
                Console.WriteLine(workers[i].ToString());
            }
        }

        public static void PrintSpecificWorker(Worker[] workers)
        {
            if (workers == null || workers.Length == 0)
            {
                Console.WriteLine("Список порожній.");
                return;
            }

            Console.Write("Введіть ID (індекс) працівника: ");
            if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 0 && idx < workers.Length)
            {
                Console.WriteLine(workers[idx].ToString());
            }
            else
            {
                Console.WriteLine("Працівника з таким індексом не знайдено.");
            }
        }

        public static void DemoCopyConstructor(Worker[] workers)
        {
            if (workers == null || workers.Length == 0)
            {
                Console.WriteLine("Спочатку створіть хоча б одного працівника.");
                return;
            }

            Console.WriteLine("\n--- Тест конструктора копіювання ---");
            Worker original = workers[0];
            Worker copy = new Worker(original);

            Console.WriteLine("Оригінал: " + original.FullName);
            Console.WriteLine("Копія: " + copy.FullName);

            copy.FullName = "Changed Name";
            copy.WorkPlace.Name = "Changed Company";

            Console.WriteLine("\nПісля зміни копії:");
            Console.WriteLine($"Оригінал (Ім'я): {original.FullName}, Компанія: {original.WorkPlace.Name}");
            Console.WriteLine($"Копія (Ім'я): {copy.FullName}, Компанія: {copy.WorkPlace.Name}");
        }

    } 
} 