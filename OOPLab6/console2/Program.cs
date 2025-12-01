using System;
using System.Collections;
using System.Collections.Generic; 
using SimpleClassLibrary; 

namespace Lab10_Variant6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\n=== Лабораторна №10 (Варіант 6) ===");
                Console.WriteLine("1. Робота з SortedList (різні типи значень)");
                Console.WriteLine("2. Робота з Dictionary (колекція Worker)");
                Console.WriteLine("0. Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": DemoSortedList(); break;
                    case "2": DemoDictionary(); break;
                    case "0": return;
                    default: Console.WriteLine("Невірний вибір."); break;
                }
            }
        }

        static void DemoSortedList()
        {
            Console.WriteLine("\n--- Демонстрація SortedList ---");

    
            SortedList sortedList = new SortedList();

            Console.WriteLine("[Add] Додаємо елементи різних типів...");
            sortedList.Add(3, "Рядок (String)");    
            sortedList.Add(1, 125);                 
            sortedList.Add(2, 45.67);               
            sortedList.Add(4, new DateTime(2023, 1, 1));

            Console.WriteLine("Вміст колекції (відсортовано за ключем):");
            foreach (DictionaryEntry item in sortedList)
            {
                Console.WriteLine($"Key: {item.Key}, Value: {item.Value} (Тип: {item.Value.GetType().Name})");
            }

            int searchKey = 2;
            Console.WriteLine($"\n[Search] Чи містить колекція ключ {searchKey}? {sortedList.ContainsKey(searchKey)}");
            if (sortedList.ContainsKey(searchKey))
            {
                Console.WriteLine($"Значення по ключу {searchKey}: {sortedList[searchKey]}");
            }

            Console.WriteLine($"[Count] Кількість елементів: {sortedList.Count}");

            Console.WriteLine("\n[Remove] Видаляємо елемент з ключем 1...");
            sortedList.Remove(1);
            Console.WriteLine($"Кількість після видалення: {sortedList.Count}");

           
            Console.WriteLine("[Clear] Очищення колекції...");
            sortedList.Clear();
            Console.WriteLine($"Кількість після очищення: {sortedList.Count}");
        }

        static void DemoDictionary()
        {
            Console.WriteLine("\n--- Демонстрація Dictionary<int, Worker> ---");

            Dictionary<int, Worker> personnel = new Dictionary<int, Worker>();

            Company comp1 = new Company("Google", "Kyiv", "Dev", 2000, true);
            Worker w1 = new Worker("Іваненко І.І.", "Kyiv", new DateTime(2020, 1, 1), comp1);

            Company comp2 = new Company("Microsoft", "Lviv", "Manager", 1500, true);
            Worker w2 = new Worker("Петренко П.П.", "Lviv", new DateTime(2021, 5, 20), comp2);

            Worker w3 = new Worker("Сидоренко С.С.", "Odesa", new DateTime(2019, 3, 10), comp1);

            Console.WriteLine("[Add] Додаємо працівників у словник...");
            personnel.Add(101, w1);
            personnel.Add(102, w2);
            personnel[103] = w3; 

          
            foreach (var item in personnel)
            {
                Console.WriteLine($"ID: {item.Key} -> {item.Value.FullName}");
            }

            int searchId = 102;
            Console.WriteLine($"\n[Search] Пошук працівника з ID {searchId}...");
            if (personnel.TryGetValue(searchId, out Worker foundWorker))
            {
                Console.WriteLine($"Знайдено: {foundWorker.FullName}, Компанія: {foundWorker.WorkPlace.Name}");
            }
            else
            {
                Console.WriteLine("Працівника не знайдено.");
            }

            Console.WriteLine($"[Count] Всього працівників: {personnel.Count}");

            Console.WriteLine($"\n[Remove] Звільняємо працівника з ID {searchId}...");
            personnel.Remove(searchId);
            Console.WriteLine($"Чи існує ID {searchId} тепер? {personnel.ContainsKey(searchId)}");

            Console.WriteLine("[Clear] Розформування штату...");
            personnel.Clear();
            Console.WriteLine($"Кількість працівників: {personnel.Count}");
        }
    }
}