using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using SequenceApp;

namespace SequenceApp
{
    // класік для роботи з послідовністю цілих чисел 
    public class Sequence
    {
        public List<int> Numbers { get; set; }

        // конструктор з параметрами
        public Sequence(List<int> numbers)
        {
            Numbers = numbers;
        }

        // параметричний конструктор для десеріалізації
        public Sequence()
        {
            Numbers = new List<int>();
        }

        //  чи є послідовність строго зростаючою???
        public bool IsStrictlyIncreasing()
        {
            for (int i = 0; i < Numbers.Count - 1; i++)
            {
                if (Numbers[i] >= Numbers[i + 1])
                    return false;
            }
            return true;
        }

        // чи є послідовність строго спадною???
        public bool IsStrictlyDecreasing()
        {
            for (int i = 0; i < Numbers.Count - 1; i++)
            {
                if (Numbers[i] <= Numbers[i + 1])
                    return false;
            }
            return true;
        }

        // чи є послідовність неспадною (кожен наступний не менший за попередній)???
        public bool IsNonDecreasing()
        {
            for (int i = 0; i < Numbers.Count - 1; i++)
            {
                if (Numbers[i] > Numbers[i + 1])
                    return false;
            }
            return true;
        }

        // чи є послідовність незростаючою (кожен наступний не більший за попередній)????
        public bool IsNonIncreasing()
        {
            for (int i = 0; i < Numbers.Count - 1; i++)
            {
                if (Numbers[i] < Numbers[i + 1])
                    return false;
            }
            return true;
        }

        // чи є послідовність арифметичною прогресією????
        public bool IsArithmeticProgression()
        {
            if (Numbers.Count < 2) return true;
            int diff = Numbers[1] - Numbers[0];
            for (int i = 1; i < Numbers.Count - 1; i++)
            {
                if (Numbers[i + 1] - Numbers[i] != diff)
                    return false;
            }
            return true;
        }

        // чи є послідовність геометричною прогресією??
        public bool IsGeometricProgression()
        {
            if (Numbers.Count < 2) return true;
            // іф всі числа рівні 0, вважаємо, що послідовність геометрична
            if (Numbers.All(n => n == 0)) return true;
            // іф хоча б один з попередніх елементів рівний 0 (але не всі), неможливо визначити співвідношення
            for (int i = 0; i < Numbers.Count - 1; i++)
            {
                if (Numbers[i] == 0)
                    return false;
            }
            double ratio = (double)Numbers[1] / Numbers[0];
            for (int i = 1; i < Numbers.Count - 1; i++)
            {
                double currentRatio = (double)Numbers[i + 1] / Numbers[i];
                if (Math.Abs(currentRatio - ratio) > 1e-9)
                    return false;
            }
            return true;
        }

        //  належність елемента до послідовності??
        public bool Contains(int element)
        {
            return Numbers.Contains(element);
        }

        // рівність двох послідовностей (поелементно)???
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Sequence))
                return false;
            Sequence other = (Sequence)obj;
            if (this.Numbers.Count != other.Numbers.Count)
                return false;
            for (int i = 0; i < Numbers.Count; i++)
            {
                if (this.Numbers[i] != other.Numbers[i])
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            foreach (int num in Numbers)
            {
                hash = hash * 23 + num.GetHashCode();
            }
            return hash;
        }

        // Повертає макс значення послідовності
        public int GetMax()
        {
            if (Numbers.Count == 0)
                throw new InvalidOperationException("Послідовність порожня.");
            return Numbers.Max();
        }

        // Повертає мін значення послідовності
        public int GetMin()
        {
            if (Numbers.Count == 0)
                throw new InvalidOperationException("Послідовність порожня.");
            return Numbers.Min();
        }

        // повертає список локальних макс (значення, що перевищують сусідні)
        public List<int> GetLocalMaxima()
        {
            List<int> localMaxima = new List<int>();
            if (Numbers.Count == 0) return localMaxima;
            for (int i = 0; i < Numbers.Count; i++)
            {
                if (i == 0)
                {
                    if (Numbers.Count > 1 && Numbers[i] > Numbers[i + 1])
                        localMaxima.Add(Numbers[i]);
                }
                else if (i == Numbers.Count - 1)
                {
                    if (Numbers[i] > Numbers[i - 1])
                        localMaxima.Add(Numbers[i]);
                }
                else
                {
                    if (Numbers[i] > Numbers[i - 1] && Numbers[i] > Numbers[i + 1])
                        localMaxima.Add(Numbers[i]);
                }
            }
            return localMaxima;
        }

        // повертає список локальних мін (значення, що менші за сусідні)
        public List<int> GetLocalMinima()
        {
            List<int> localMinima = new List<int>();
            if (Numbers.Count == 0) return localMinima;
            for (int i = 0; i < Numbers.Count; i++)
            {
                if (i == 0)
                {
                    if (Numbers.Count > 1 && Numbers[i] < Numbers[i + 1])
                        localMinima.Add(Numbers[i]);
                }
                else if (i == Numbers.Count - 1)
                {
                    if (Numbers[i] < Numbers[i - 1])
                        localMinima.Add(Numbers[i]);
                }
                else
                {
                    if (Numbers[i] < Numbers[i - 1] && Numbers[i] < Numbers[i + 1])
                        localMinima.Add(Numbers[i]);
                }
            }
            return localMinima;
        }

        // розбиває послідовність на підпослідовності за допомогою локальних екстремумів
        // ретурн список підпослідовностей (кожна підпослідовність – List<int>)
        public List<List<int>> GetSubsequencesByDelimiters()
        {
            List<int> delimiters = new List<int>();
            if (Numbers.Count > 0)
            {
                // add початковий індекс
                delimiters.Add(0);
                // add індекси локальних максимумів і мінімумів (крім початкового та кінцевого)
                for (int i = 1; i < Numbers.Count - 1; i++)
                {
                    if ((Numbers[i] > Numbers[i - 1] && Numbers[i] > Numbers[i + 1]) ||
                        (Numbers[i] < Numbers[i - 1] && Numbers[i] < Numbers[i + 1]))
                    {
                        delimiters.Add(i);
                    }
                }
                // add останній індекс
                delimiters.Add(Numbers.Count - 1);
            }
            // уникальні значення та сортування
            delimiters = delimiters.Distinct().OrderBy(x => x).ToList();

            List<List<int>> subsequences = new List<List<int>>();
            for (int i = 0; i < delimiters.Count - 1; i++)
            {
                int start = delimiters[i];
                int end = delimiters[i + 1];
                List<int> subseq = Numbers.GetRange(start, end - start + 1);
                subsequences.Add(subseq);
            }
            return subsequences;
        }

        // повертає найбільшу підпослідовність за довжиною
        public List<int> GetLargestSubsequence()
        {
            var subsequences = GetSubsequencesByDelimiters();
            if (subsequences.Count == 0)
                return new List<int>();
            return subsequences.OrderByDescending(s => s.Count).First();
        }

        // повертає найменшу підпослідовність за довжиною
        public List<int> GetSmallestSubsequence()
        {
            var subsequences = GetSubsequencesByDelimiters();
            if (subsequences.Count == 0)
                return new List<int>();
            return subsequences.OrderBy(s => s.Count).First();
        }

        // метод для збереження об’єкта у JSON файл
        public void SaveToJson(string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(this, options);
            File.WriteAllText(filePath, jsonString);
        }

        // метод для створення об’єкта з JSON файлу
        public static Sequence LoadFromJson(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Sequence>(jsonString);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // створення об’єкта Sequence з прикладовою послідовністю
            Sequence seq = new Sequence(new List<int> { 1, 3, 5, 7, 6, 4, 8, 10, 9 });

            Console.WriteLine("Послідовність: " + string.Join(", ", seq.Numbers));
            Console.WriteLine("Строго зростаюча: " + seq.IsStrictlyIncreasing());
            Console.WriteLine("Строго спадна: " + seq.IsStrictlyDecreasing());
            Console.WriteLine("Неспадна: " + seq.IsNonDecreasing());
            Console.WriteLine("Незростаюча: " + seq.IsNonIncreasing());
            Console.WriteLine("Арифметична прогресія: " + seq.IsArithmeticProgression());
            Console.WriteLine("Геометрична прогресія: " + seq.IsGeometricProgression());

            int elementToCheck = 7;
            Console.WriteLine("Послідовність містить " + elementToCheck + ": " + seq.Contains(elementToCheck));

            // створення ще однієї послідовності для перевірки рівності
            Sequence seq2 = new Sequence(new List<int> { 1, 3, 5, 7, 6, 4, 8, 10, 9 });
            Console.WriteLine("Послідовність дорівнює seq2: " + seq.Equals(seq2));

            Console.WriteLine("Максимальне значення: " + seq.GetMax());
            Console.WriteLine("Мінімальне значення: " + seq.GetMin());

            var localMaxima = seq.GetLocalMaxima();
            Console.WriteLine("Локальні максимуми: " + (localMaxima.Count > 0 ? string.Join(", ", localMaxima) : "відсутні"));

            var localMinima = seq.GetLocalMinima();
            Console.WriteLine("Локальні мінімуми: " + (localMinima.Count > 0 ? string.Join(", ", localMinima) : "відсутні"));

            var subsequences = seq.GetSubsequencesByDelimiters();
            Console.WriteLine("Підпослідовності (розбиті за локальними екстремумами):");
            foreach (var subseq in subsequences)
            {
                Console.WriteLine("[" + string.Join(", ", subseq) + "]");
            }

            var largestSubseq = seq.GetLargestSubsequence();
            Console.WriteLine("Найбільша підпослідовність: [" + string.Join(", ", largestSubseq) + "]");

            var smallestSubseq = seq.GetSmallestSubsequence();
            Console.WriteLine("Найменша підпослідовність: [" + string.Join(", ", smallestSubseq) + "]");

            // Демонстрація серіалізації у JSON
            string filePath = "sequence.json";
            seq.SaveToJson(filePath);
            Console.WriteLine("Об’єкт збережено у JSON файл: " + filePath);

            // Демонстрація десеріалізації з JSON файлу
            Sequence loadedSeq = Sequence.LoadFromJson(filePath);
            Console.WriteLine("Об’єкт, завантажений з JSON: " + string.Join(", ", loadedSeq.Numbers));

            // Чекаємо натискання клавіші для завершення
            Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}






