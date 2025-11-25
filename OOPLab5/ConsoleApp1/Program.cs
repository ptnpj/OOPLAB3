using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CableTVLab
{
    public enum SubscriberType
    {
        Звичайний = 1,
        Пільговий,
        VIP,
        Корпоративний
    }

    public class Subscriber
    {
        public string AccountNumber { get; set; } 
        public string FullName { get; set; }    
        public string Address { get; set; }       
        public string PhoneNumber { get; set; }   
        public string ContractNumber { get; set; } 
        public DateTime ContractDate { get; set; } 
        public bool HasBenefits { get; set; }    
        public SubscriberType Type { get; set; } 
        public string TariffPlan { get; set; }   

        public override string ToString()
        {
            return $"Договір: {ContractNumber} | {FullName} | Тип: {Type} | Тариф: {TariffPlan} | Дата: {ContractDate.ToShortDateString()}";
        }

        public string ToFileString()
        {
            return $"{AccountNumber}|{FullName}|{Address}|{PhoneNumber}|{ContractNumber}|{ContractDate}|{HasBenefits}|{Type}|{TariffPlan}";
        }
    }

    class Program
    {
        static string filePath = "subscribers.txt";
        static List<Subscriber> subscribers = new List<Subscriber>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\n--- МЕНЮ ---");
                Console.WriteLine("1. Додати нового абонента (і записати у файл)");
                Console.WriteLine("2. Зчитати всіх з файлу");
                Console.WriteLine("3. Показати список (поточний)");
                Console.WriteLine("4. Пошук абонента");
                Console.WriteLine("5. Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddSubscriber();
                        break;
                    case "2":
                        ReadFile();
                        break;
                    case "3":
                        PrintList(subscribers);
                        break;
                    case "4":
                        SearchMenu();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }
            }
        }

        static void AddSubscriber()
        {
            Subscriber sub = new Subscriber();

            Console.Write("Введіть номер рахунку: ");
            sub.AccountNumber = Console.ReadLine();

            Console.Write("Введіть П.І.П.: ");
            sub.FullName = Console.ReadLine();

            Console.Write("Введіть адресу: ");
            sub.Address = Console.ReadLine();

            Console.Write("Введіть телефон: ");
            sub.PhoneNumber = Console.ReadLine();

            Console.Write("Введіть номер договору: ");
            sub.ContractNumber = Console.ReadLine();

            Console.Write("Введіть дату договору (рррр-мм-дд): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                sub.ContractDate = date;
            else
                sub.ContractDate = DateTime.Now;

            Console.Write("Чи є пільги (y - так, n - ні): ");
            sub.HasBenefits = Console.ReadLine().ToLower() == "y";

            Console.WriteLine("Виберіть тип абонента:");
            Console.WriteLine("1 - Звичайний, 2 - Пільговий, 3 - VIP, 4 - Корпоративний");
            int typeIndex;
            if (int.TryParse(Console.ReadLine(), out typeIndex) && Enum.IsDefined(typeof(SubscriberType), typeIndex))
            {
                sub.Type = (SubscriberType)typeIndex;
            }
            else
            {
                sub.Type = SubscriberType.Звичайний;                               
            }

            Console.Write("Введіть тарифний план: ");
            sub.TariffPlan = Console.ReadLine();


            subscribers.Add(sub);

            
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8))
                {
                    sw.WriteLine(sub.ToFileString());
                }
                Console.WriteLine("Абонента успішно додано та збережено у файл!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка запису у файл: {ex.Message}");
            }
        }

        static void ReadFile()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл ще не створено.");
                return;
            }

            subscribers.Clear(); 

            try
            {
                using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length >= 9)
                        {
                            Subscriber sub = new Subscriber
                            {
                                AccountNumber = parts[0],
                                FullName = parts[1],
                                Address = parts[2],
                                PhoneNumber = parts[3],
                                ContractNumber = parts[4],
                                ContractDate = DateTime.Parse(parts[5]),
                                HasBenefits = bool.Parse(parts[6]),
                                Type = (SubscriberType)Enum.Parse(typeof(SubscriberType), parts[7]),
                                TariffPlan = parts[8]
                            };
                            subscribers.Add(sub);
                        }
                    }
                }
                Console.WriteLine($"Зчитано {subscribers.Count} абонентів.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка читання файлу: {ex.Message}");
            }
        }

        static void PrintList(List<Subscriber> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список порожній.");
                return;
            }

            foreach (var sub in list)
            {
                Console.WriteLine(sub);
            }
        }
        static void SearchMenu()
        {
            Console.WriteLine("\n--- ПОШУК ---");
            Console.WriteLine("1. За номером договору");
            Console.WriteLine("2. За П.І.П. абонента");
            Console.WriteLine("3. За тарифним планом");
            Console.Write("Оберіть критерій: ");
            string choice = Console.ReadLine();

            Console.Write("Введіть значення для пошуку: ");
            string query = Console.ReadLine().ToLower();

            List<Subscriber> results = new List<Subscriber>();

            switch (choice)
            {
                case "1":
                    results = subscribers.Where(s => s.ContractNumber.ToLower().Contains(query)).ToList();
                    break;
                case "2":
                    results = subscribers.Where(s => s.FullName.ToLower().Contains(query)).ToList();
                    break;
                case "3":
                    results = subscribers.Where(s => s.TariffPlan.ToLower().Contains(query)).ToList();
                    break;
                default:
                    Console.WriteLine("Невірний критерій.");
                    return;
            }

            Console.WriteLine("\nРезультати пошуку:");
            PrintList(results);
        }
    }
}