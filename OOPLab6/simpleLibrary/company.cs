using System;

namespace SimpleClassLibrary
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
}