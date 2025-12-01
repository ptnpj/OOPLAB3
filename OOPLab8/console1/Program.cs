using System;

namespace Lab8_Variant6
{

    public class Building
    {
        public string Address { get; set; }
        public string WallMaterial { get; set; }
        public int Floors { get; set; }

        public Building()
        {
            Address = "Невідомо";
            WallMaterial = "Цегла";
            Floors = 1;
        }

        public Building(string address, string material, int floors)
        {
            Address = address;
            WallMaterial = material;
            Floors = floors;
        }

        public Building(Building other)
        {
            Address = other.Address;
            WallMaterial = other.WallMaterial;
            Floors = other.Floors;
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"Адреса: {Address}, Стіни: {WallMaterial}, Поверхів: {Floors}");
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Building b = (Building)obj;
            return Address == b.Address && WallMaterial == b.WallMaterial && Floors == b.Floors;
        }

        public override int GetHashCode()
        {
            return (Address + WallMaterial + Floors).GetHashCode();
        }
    }

    public class ApartmentBuilding : Building
    {
        public int ApartmentsCount { get; set; }

        public ApartmentBuilding() : base()
        {
            ApartmentsCount = 0;
        }

        public ApartmentBuilding(string address, string material, int floors, int aptCount)
            : base(address, material, floors)
        {
            ApartmentsCount = aptCount;
        }

        public ApartmentBuilding(ApartmentBuilding other) : base(other)
        {
            ApartmentsCount = other.ApartmentsCount;
        }

        public override void ShowInfo()
        {
            base.ShowInfo(); 
            Console.WriteLine($"   -> Тип: Житловий, Квартир: {ApartmentsCount}");
        }

        public void ChangeApartmentCount(int newCount)
        {
            ApartmentsCount = newCount;
            Console.WriteLine($"Кількість квартир змінено на {ApartmentsCount}");
        }
    }

    public class Warehouse : Building
    {
        public string WarehouseType { get; set; } 

        public Warehouse() : base()
        {
            WarehouseType = "Закритий";
        }

        public Warehouse(string address, string material, int floors, string type)
            : base(address, material, floors)
        {
            WarehouseType = type;
        }

        public Warehouse(Warehouse other) : base(other)
        {
            WarehouseType = other.WarehouseType;
        }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine($"   -> Тип: Склад, Вид: {WarehouseType}");
        }

        public void ChangeType(string newType)
        {
            WarehouseType = newType;
            Console.WriteLine($"Тип складу змінено на {WarehouseType}");
        }
    }


    public class Fraction
    {
        public int Numerator { get; set; }   // Чисельник
        public int Denominator { get; set; } // Знаменник

        public Fraction(int num, int den)
        {
            if (den == 0) throw new ArgumentException("Знаменник не може бути нулем!");
            Numerator = num;
            Denominator = den;
            Simplify(); 
        }

        private void Simplify()
        {
            int gcd = GetGCD(Math.Abs(Numerator), Math.Abs(Denominator));
            Numerator /= gcd;
            Denominator /= gcd;
            if (Denominator < 0) 
            {
                Numerator = -Numerator;
                Denominator = -Denominator;
            }
        }

        private int GetGCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public static Fraction operator +(Fraction a, Fraction b)
        {
            return new Fraction(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator);
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            return new Fraction(a.Numerator * b.Denominator - b.Numerator * a.Denominator, a.Denominator * b.Denominator);
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            return new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);
        }

        public static Fraction operator /(Fraction a, Fraction b)
        {
            if (b.Numerator == 0) throw new DivideByZeroException("Ділення на нуль!");
            return new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator);
        }

        public static bool operator ==(Fraction a, Fraction b)
        {
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return ReferenceEquals(a, b);
            return a.Numerator == b.Numerator && a.Denominator == b.Denominator;
        }

        public static bool operator !=(Fraction a, Fraction b) => !(a == b);

        public static bool operator >(Fraction a, Fraction b) => (double)a > (double)b;
        public static bool operator <(Fraction a, Fraction b) => (double)a < (double)b;
        public static bool operator >=(Fraction a, Fraction b) => (double)a >= (double)b;
        public static bool operator <=(Fraction a, Fraction b) => (double)a <= (double)b;

        public static explicit operator double(Fraction f)
        {
            return (double)f.Numerator / f.Denominator;
        }

        public override bool Equals(object obj)
        {
            if (obj is Fraction f) return this == f;
            return false;
        }

        public override int GetHashCode() => (Numerator, Denominator).GetHashCode();

        public override string ToString() => $"{Numerator}/{Denominator}";
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\n--- Лабораторна робота №8 (Варіант 6) ---");
                Console.WriteLine("1. Робота з ієрархією (Будинки)");
                Console.WriteLine("2. Робота з дробами (Fraction)");
                Console.WriteLine("0. Вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DemoHierarchy();
                        break;
                    case "2":
                        DemoFractions();
                        break;
                    case "0": return;
                    default: Console.WriteLine("Невірний вибір"); break;
                }
            }
        }

        static void DemoHierarchy()
        {
            Console.WriteLine("\n--- Демонстрація поліморфізму ---");

            Building[] city = new Building[3];

            city[0] = new Building("Головна, 1", "Бетон", 1);
            city[1] = new ApartmentBuilding("Хрещатик, 25", "Цегла", 5, 20);
            city[2] = new Warehouse("Промзона, 5", "Метал", 1, "Ангар");

            foreach (var b in city)
            {
                b.ShowInfo();
                Console.WriteLine("-");
            }

            Console.WriteLine("\n--- Перевірка специфічних методів ---");
            ApartmentBuilding apt = (ApartmentBuilding)city[1];
            apt.ChangeApartmentCount(25);
            apt.ShowInfo();
        }

        static void DemoFractions()
        {
            try
            {
                Console.WriteLine("\n--- Демонстрація дробів ---");
                Console.Write("Введіть чисельник 1-го дробу: "); int n1 = int.Parse(Console.ReadLine());
                Console.Write("Введіть знаменник 1-го дробу: "); int d1 = int.Parse(Console.ReadLine());
                Fraction f1 = new Fraction(n1, d1);

                Console.Write("Введіть чисельник 2-го дробу: "); int n2 = int.Parse(Console.ReadLine());
                Console.Write("Введіть знаменник 2-го дробу: "); int d2 = int.Parse(Console.ReadLine());
                Fraction f2 = new Fraction(n2, d2);

                Console.WriteLine($"\nДріб 1: {f1}");
                Console.WriteLine($"Дріб 2: {f2}");

                Console.WriteLine($"Сума (+): {f1 + f2}");
                Console.WriteLine($"Різниця (-): {f1 - f2}");
                Console.WriteLine($"Множення (*): {f1 * f2}");
                Console.WriteLine($"Ділення (/): {f1 / f2}");

                Console.WriteLine($"Рівність (==): {f1 == f2}");
                Console.WriteLine($"Більше (>): {f1 > f2}");

                Console.WriteLine($"Перетворення у double (f1): {(double)f1}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
    }
}