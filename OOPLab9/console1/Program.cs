using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab9_Variant6
{

    public interface ITrigonometricFigure
    {
        double GetSurfaceArea(); 
    }

    public abstract class GeometricBody : ITrigonometricFigure
    {
        public string Name { get; set; }

        public GeometricBody(string name)
        {
            Name = name;
        }

        public abstract double GetSurfaceArea();

        public override string ToString()
        {
            return $"{Name} | Площа поверхні: {GetSurfaceArea():F2}";
        }
    }

    public class Cube : GeometricBody
    {
        public double Side { get; set; }

        public Cube(double side) : base("Куб")
        {
            Side = side;
        }

        public override double GetSurfaceArea()
        {
            return 6 * Math.Pow(Side, 2); 
        }
    }

    public class Cylinder : GeometricBody
    {
        public double Radius { get; set; }
        public double Height { get; set; }

        public Cylinder(double radius, double height) : base("Циліндр")
        {
            Radius = radius;
            Height = height;
        }

        public override double GetSurfaceArea()
        {
            return 2 * Math.PI * Radius * (Radius + Height);
        }
    }

    public class Cone : GeometricBody
    {
        public double Radius { get; set; }
        public double Height { get; set; }

        public Cone(double radius, double height) : base("Конус")
        {
            Radius = radius;
            Height = height;
        }

        public override double GetSurfaceArea()
        {
            double slantHeight = Math.Sqrt(Math.Pow(Radius, 2) + Math.Pow(Height, 2));
            return Math.PI * Radius * (Radius + slantHeight);
        }
    }


    public class Document : IComparable<Document>
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public int Importance { get; set; }

        public Document(string title, int pages, int importance)
        {
            Title = title;
            PageCount = pages;
            Importance = importance;
        }

        public int CompareTo(Document other)
        {
            if (other == null) return 1;
            return this.PageCount.CompareTo(other.PageCount);
        }

        public override string ToString()
        {
            return $"Dokument: '{Title}', Pages: {PageCount}, Priority: {Importance}";
        }
    }

    public class DocumentComparer : IComparer<Document>
    {
        public int Compare(Document x, Document y)
        {
            if (x == null || y == null) return 0;

       
            int result = x.PageCount.CompareTo(y.PageCount);

            if (result == 0)
            {
                return x.Importance.CompareTo(y.Importance);
            }
            return result;
        }
    }

    public class DocumentLibrary : IEnumerable
    {
        private Document[] _documents;

        public DocumentLibrary(Document[] docs)
        {
            _documents = docs;
        }

        public IEnumerator GetEnumerator()
        {
            return _documents.GetEnumerator();
        }

      
        public void SortByPages()
        {
            Array.Sort(_documents); 
        }

        public void SortComplex()
        {
            Array.Sort(_documents, new DocumentComparer()); 
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\n=== Лабораторна №9 (Варіант 6) ===");
                Console.WriteLine("1. Завдання 1 (Фігури: Cylinder, Cube, Cone)");
                Console.WriteLine("2. Завдання 2 (Документи: Comparable, Comparer, Enumerable)");
                Console.WriteLine("0. Вихід");
                Console.Write("Ваш вибір: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": RunTask1(); break;
                    case "2": RunTask2(); break;
                    case "0": return;
                    default: Console.WriteLine("Невірний вибір."); break;
                }
            }
        }

        static void RunTask1()
        {
            Console.WriteLine("\n--- Геометричні фігури ---");

            GeometricBody[] shapes = new GeometricBody[]
            {
                new Cube(5),
                new Cylinder(3, 10),       
                new Cone(3, 4)               
            };

            foreach (var shape in shapes)
            {
                Console.WriteLine(shape.ToString());
            }
        }

        static void RunTask2()
        {
            Console.WriteLine("\n--- Робота з документами ---");

            Document[] docs = new Document[]
            {
                new Document("Звіт", 15, 1),
                new Document("Курсова", 45, 2),
                new Document("Заява", 1, 5),
                new Document("Книга", 300, 3),
                new Document("Стаття", 15, 10) 
            };

            DocumentLibrary library = new DocumentLibrary(docs);

            Console.WriteLine("\n1. Список до сортування (foreach через IEnumerable):");
            foreach (Document d in library)
            {
                Console.WriteLine(d);
            }

            Console.WriteLine("\n2. Сортування за кількістю сторінок (IComparable):");
            library.SortByPages();
            foreach (Document d in library)
            {
                Console.WriteLine(d);
            }

            Console.WriteLine("\n3. Сортування за сторінками + важливістю (IComparer):");
            library.SortComplex();
            foreach (Document d in library)
            {
                Console.WriteLine(d);
            }
        }
    }
}