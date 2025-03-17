using System;
using System.Collections.Generic;

namespace LabWork4
{
    // завдання 1: класи для географічних об’єктів

    public class Continent
    {
        public string Name { get; set; }
        public Continent(string name)
        {
            Console.WriteLine("Викликано конструктор Continent");
            Name = name;
        }

        public override bool Equals(object obj)
        {
            Console.WriteLine("Викликано метод Equals класу Continent");
            if (obj is Continent other)
                return Name == other.Name;
            return false;
        }

        public override int GetHashCode()
        {
            Console.WriteLine("Викликано метод GetHashCode класу Continent");
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            Console.WriteLine("Викликано метод ToString класу Continent");
            return $"Continent: {Name}";
        }
    }

    public class Ocean
    {
        public string Name { get; set; }
        public Ocean(string name)
        {
            Console.WriteLine("Викликано конструктор Ocean");
            Name = name;
        }

        public override bool Equals(object obj)
        {
            Console.WriteLine("Викликано метод Equals класу Ocean");
            if (obj is Ocean other)
                return Name == other.Name;
            return false;
        }

        public override int GetHashCode()
        {
            Console.WriteLine("Викликано метод GetHashCode класу Ocean");
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            Console.WriteLine("Викликано метод ToString класу Ocean");
            return $"Ocean: {Name}";
        }
    }

    public class Island
    {
        public string Name { get; set; }
        public Island(string name)
        {
            Console.WriteLine("Викликано конструктор Island");
            Name = name;
        }

        public override bool Equals(object obj)
        {
            Console.WriteLine("Викликано метод Equals класу Island");
            if (obj is Island other)
                return Name == other.Name;
            return false;
        }

        public override int GetHashCode()
        {
            Console.WriteLine("Викликано метод GetHashCode класу Island");
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            Console.WriteLine("Викликано метод ToString класу Island");
            return $"Island: {Name}";
        }
    }

    public class Planet
    {
        public string Name { get; set; }
        public List<Continent> Continents { get; set; }
        public Ocean PlanetOcean { get; set; }
        public Island PlanetIsland { get; set; }

        public Planet(string name, List<Continent> continents, Ocean ocean, Island island)
        {
            Console.WriteLine("Викликано конструктор Planet");
            Name = name;
            Continents = continents;
            PlanetOcean = ocean;
            PlanetIsland = island;
        }

        // метод для виведення назви планети 
        public void DisplayPlanetName()
        {
            Console.WriteLine("Викликано метод DisplayPlanetName()");
            Console.WriteLine("Назва планети: " + Name);
        }

        public void DisplayPlanetName(string prefix)
        {
            Console.WriteLine("Викликано перевантажений метод DisplayPlanetName(string)");
            Console.WriteLine(prefix + " " + Name);
        }

        // метод для виведення назви першого материка
        public void DisplayContinentName()
        {
            Console.WriteLine("Викликано метод DisplayContinentName()");
            if (Continents != null && Continents.Count > 0)
                Console.WriteLine("Назва материка: " + Continents[0].Name);
            else
                Console.WriteLine("Материків немає.");
        }

        // метод для виведення кількості материків
        public void DisplayContinentCount()
        {
            Console.WriteLine("Викликано метод DisplayContinentCount()");
            int count = (Continents != null) ? Continents.Count : 0;
            Console.WriteLine("Кількість материків: " + count);
        }

        public override bool Equals(object obj)
        {
            Console.WriteLine("Викликано метод Equals класу Planet");
            if (obj is Planet other)
                return Name == other.Name;
            return false;
        }

        public override int GetHashCode()
        {
            Console.WriteLine("Викликано метод GetHashCode класу Planet");
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            Console.WriteLine("Викликано метод ToString класу Planet");
            return $"Planet: {Name}, Материків: {Continents.Count}";
        }
    }

    // завдання 2: ієрархія цукерок та подарунок

    // абстрактний клас для цукерок
    public abstract class Candy
    {
        public string Name { get; set; }
        public double Weight { get; set; } // вага в грамах
        public double SugarContent { get; set; } // вміст цукру в грамах

        public Candy(string name, double weight, double sugarContent)
        {
            Console.WriteLine("Викликано конструктор Candy");
            Name = name;
            Weight = weight;
            SugarContent = sugarContent;
        }

        public virtual void PrintInfo()
        {
            Console.WriteLine("Викликано метод PrintInfo() в Candy");
            Console.WriteLine($"Цукерка: {Name}, Вага: {Weight} г, Цукор: {SugarContent} г");
        }

        public override bool Equals(object obj)
        {
            Console.WriteLine("Викликано метод Equals класу Candy");
            if (obj is Candy other)
                return Name == other.Name && Weight == other.Weight && SugarContent == other.SugarContent;
            return false;
        }

        public override int GetHashCode()
        {
            Console.WriteLine("Викликано метод GetHashCode класу Candy");
            return (Name, Weight, SugarContent).GetHashCode();
        }

        public override string ToString()
        {
            Console.WriteLine("Викликано метод ToString класу Candy");
            return $"Цукерка: {Name}, Вага: {Weight} г, Цукор: {SugarContent} г";
        }
    }

    // похідний клас ChocolateCandy
    public class ChocolateCandy : Candy
    {
        public double CocoaPercentage { get; set; }
        public ChocolateCandy(string name, double weight, double sugarContent, double cocoaPercentage)
            : base(name, weight, sugarContent)
        {
            Console.WriteLine("Викликано конструктор ChocolateCandy");
            CocoaPercentage = cocoaPercentage;
        }

        public override void PrintInfo()
        {
            Console.WriteLine("Викликано метод PrintInfo() в ChocolateCandy");
            base.PrintInfo();
            Console.WriteLine($"Вміст какао: {CocoaPercentage}%");
        }

        public override bool Equals(object obj)
        {
            Console.WriteLine("Викликано метод Equals класу ChocolateCandy");
            if (obj is ChocolateCandy other)
                return base.Equals(other) && CocoaPercentage == other.CocoaPercentage;
            return false;
        }

        public override int GetHashCode()
        {
            Console.WriteLine("Викликано метод GetHashCode класу ChocolateCandy");
            return (base.GetHashCode(), CocoaPercentage).GetHashCode();
        }

        public override string ToString()
        {
            Console.WriteLine("Викликано метод ToString класу ChocolateCandy");
            return $"ChocolateCandy: {Name}, Вага: {Weight} г, Цукор: {SugarContent} г, Какао: {CocoaPercentage}%";
        }
    }

    // похідний клас CaramelCandy
    public class CaramelCandy : Candy
    {
        public string Flavor { get; set; }
        public CaramelCandy(string name, double weight, double sugarContent, string flavor)
            : base(name, weight, sugarContent)
        {
            Console.WriteLine("Викликано конструктор CaramelCandy");
            Flavor = flavor;
        }

        public override void PrintInfo()
        {
            Console.WriteLine("Викликано метод PrintInfo() в CaramelCandy");
            base.PrintInfo();
            Console.WriteLine($"Смак: {Flavor}");
        }

        public override bool Equals(object obj)
        {
            Console.WriteLine("Викликано метод Equals класу CaramelCandy");
            if (obj is CaramelCandy other)
                return base.Equals(other) && Flavor == other.Flavor;
            return false;
        }

        public override int GetHashCode()
        {
            Console.WriteLine("Викликано метод GetHashCode класу CaramelCandy");
            return (base.GetHashCode(), Flavor).GetHashCode();
        }

        public override string ToString()
        {
            Console.WriteLine("Викликано метод ToString класу CaramelCandy");
            return $"CaramelCandy: {Name}, Вага: {Weight} г, Цукор: {SugarContent} г, Смак: {Flavor}";
        }
    }

    // клас Gift для збору новорічного подарунку (колекція цукерок)
    public class Gift
    {
        private List<Candy> candies = new List<Candy>();

        public void AddCandy(Candy candy)
        {
            Console.WriteLine("Викликано метод AddCandy() для " + candy.Name);
            candies.Add(candy);
        }

        public double TotalWeight()
        {
            Console.WriteLine("Викликано метод TotalWeight()");
            double total = 0;
            foreach (var candy in candies)
                total += candy.Weight;
            return total;
        }

        public void SortBySugarContent()
        {
            Console.WriteLine("Викликано метод SortBySugarContent()");
            candies.Sort((c1, c2) => c1.SugarContent.CompareTo(c2.SugarContent));
        }

        public List<Candy> FindCandyBySugarRange(double min, double max)
        {
            Console.WriteLine($"Викликано метод FindCandyBySugarRange() для діапазону {min} - {max}");
            return candies.FindAll(c => c.SugarContent >= min && c.SugarContent <= max);
        }

        public void PrintGiftContents()
        {
            Console.WriteLine("Викликано метод PrintGiftContents()");
            foreach (var candy in candies)
                Console.WriteLine(candy.ToString());
        }

        // перевантаження методу PrintGiftContents з заголовком
        public void PrintGiftContents(string header)
        {
            Console.WriteLine("Викликано перевантажений метод PrintGiftContents(string)");
            Console.WriteLine(header);
            PrintGiftContents();
        }
    }

    // головний клас програми з мінімальним консольним меню
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторна робота #4 - Демонстрація");
            Console.WriteLine("Виберіть завдання: 1 - Планета; 2 - Новорічний подарунок; 0 - Вихід");

            string input = Console.ReadLine();
            while (input != "0")
            {
                if (input == "1")
                {
                    RunTask1();
                }
                else if (input == "2")
                {
                    RunTask2();
                }
                else
                {
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                }
                Console.WriteLine("Виберіть завдання: 1 - Планета; 2 - Новорічний подарунок; 0 - Вихід");
                input = Console.ReadLine();
            }
        }

        // метод для демонстрації Завдання 1
        static void RunTask1()
        {
            Console.WriteLine("Запуск Завдання 1: Планета");

            // створення об’єктів класів Материк, Океан, Острів
            Continent continent1 = new Continent("Європа");
            Continent continent2 = new Continent("Азія");

            Ocean ocean = new Ocean("Тихий океан");
            Island island = new Island("Мадагаскар");

            List<Continent> continents = new List<Continent> { continent1, continent2 };

            // створення об'єкта Планета із використанням вищезгаданих класів
            Planet planet = new Planet("Земля", continents, ocean, island);

            // виклик методів
            planet.DisplayPlanetName();
            planet.DisplayPlanetName("Назва планети:");
            planet.DisplayContinentName();
            planet.DisplayContinentCount();

            Console.WriteLine("Об’єкт планети: " + planet.ToString());
        }

        // метод для демонстрації Завдання 2
        static void RunTask2()
        {
            Console.WriteLine("Запуск Завдання 2: Новорічний подарунок");

            // створення об’єктів цукерок
            ChocolateCandy choco1 = new ChocolateCandy("Молочний шоколад", 50, 30, 40);
            CaramelCandy caramel1 = new CaramelCandy("М'яка карамель", 30, 20, "Ванільний");
            ChocolateCandy choco2 = new ChocolateCandy("Темний шоколад", 40, 25, 70);

            // створення подарунку та додавання цукерок
            Gift gift = new Gift();
            gift.AddCandy(choco1);
            gift.AddCandy(caramel1);
            gift.AddCandy(choco2);

            // виведення вмісту подарунку
            gift.PrintGiftContents("Вміст подарунку:");

            // обчислення загальної ваги подарунку
            double totalWeight = gift.TotalWeight();
            Console.WriteLine("Загальна вага подарунку: " + totalWeight + " г");

            // сортування цукерок за вмістом цукру
            gift.SortBySugarContent();
            Console.WriteLine("Після сортування за вмістом цукру:");
            gift.PrintGiftContents();

            // пошук цукерок, що відповідають заданому діапазону вмісту цукру
            Console.WriteLine("Введіть мінімальне значення цукру:");
            double minSugar = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Введіть максимальне значення цукру:");
            double maxSugar = Convert.ToDouble(Console.ReadLine());

            List<Candy> foundCandies = gift.FindCandyBySugarRange(minSugar, maxSugar);
            Console.WriteLine("Цукерки, що відповідають заданому діапазону:");
            foreach (var candy in foundCandies)
            {
                Console.WriteLine(candy.ToString());
            }
        }
    }
}
