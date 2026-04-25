using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace OrganizationLib
{
    public class TestCollections
    {
        private readonly Queue<Library> col1;
        private readonly Queue<string> col2;
        private readonly SortedDictionary<Organization, Library> col3;
        private readonly SortedDictionary<string, Library> col4;

        // Конструктор с параметром количества элементов (по умолчанию 1000)
        public TestCollections(int count = 1000)
        {
            col1 = new Queue<Library>();
            col2 = new Queue<string>();
            col3 = new SortedDictionary<Organization, Library>();
            col4 = new SortedDictionary<string, Library>();

            GenerateAndFillCollections(count);
        }

        // Заполнение коллекций
        private void GenerateAndFillCollections(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Library libr = new Library();
                libr.RandomInit();
                
                Organization org = libr.BaseOrganization;
                string str = libr.ToString();

                // Заполнение всех коллекций одинаковыми элементами
                col1.Enqueue(libr);
                col2.Enqueue(str);
                col3.Add(org, libr);
                col4.Add(str, libr); // Добавляем i для уникального ключа в словаре
            }
        }

        // Добавление элемента во все коллекции
        public void Add(Library libr)
        {
            Organization org = libr.BaseOrganization;
            string str = libr.ToString();

            col1.Enqueue(libr);
            col2.Enqueue(str);
            col3.Add(org, libr);
            col4.Add(str, libr);
        }

        public void Remove(Library libr)
        {
            string str = libr.ToString();

            RemoveFromQueue(col1, libr);
            RemoveFromQueue(col2, libr.ToString());
            col3.Remove(libr.BaseOrganization);
            col4.Remove(str);
        }

        public void MeasureSearchTime()
        {
            Library first = Copy(col1.Peek());
            Library middle = GetMiddle(col1);
            Library last = GetLast(col1);
            Library libNotInCollection = new Library();

            string firstStr = first.ToString();
            string middleStr = middle.ToString();
            string lastStr = last.ToString();

            Organization firstKey = first.BaseOrganization;
            Organization middleKey = middle.BaseOrganization;
            Organization lastKey = last.BaseOrganization;
            Organization baseNotInCollection = new Organization();

            Console.WriteLine("\n>>> Collection 1 - Queue<Library>");
            Measure("first", () => col1.Contains(first));
            Measure("middle", () => col1.Contains(middle));
            Measure("last", () => col1.Contains(last));
            Measure("nonexistent", () => col1.Contains(libNotInCollection));

            Console.WriteLine("\n>>> Collection 2 - Queue<string>");
            Measure("first", () => col2.Contains(firstStr));
            Measure("middle", () => col2.Contains(middleStr));
            Measure("last", () => col2.Contains(lastStr));
            Measure("nonexistent", () => col2.Contains("NONE"));

            Console.WriteLine("\n>>> Collection 3 - SortedDictionary<Organization, Library>");
            Measure("first", () => col3.ContainsKey(firstKey));
            Measure("middle", () => col3.ContainsKey(middleKey));
            Measure("last", () => col3.ContainsKey(lastKey));
            Measure("nonexistent", () => col3.ContainsKey(baseNotInCollection));

            Console.WriteLine("\n>>> Collection 4 - SortedDictionary<string, Library> (By key)");
            Measure("first", () => col4.ContainsKey(firstStr));
            Measure("middle", () => col4.ContainsKey(middleStr));
            Measure("last", () => col4.ContainsKey(lastStr));
            Measure("nonexistent", () => col4.ContainsKey("NONE"));

            Console.WriteLine("\n>>> Collection 4 - SortedDictionary<string, Library> (By value)");
            Measure("first", () => col4.ContainsValue(first));
            Measure("middle", () => col4.ContainsValue(middle));
            Measure("last", () => col4.ContainsValue(last));
            Measure("nonexistent", () => col4.ContainsValue(null));
        }

        private Library Copy(Library libr)
        {
            return new Library
            {
                Name = libr.Name,
                EmployeesCount = libr.EmployeesCount,
                BooksCount = libr.BooksCount
            };
        }

        private Library GetMiddle(Queue<Library> queue)
        {
            return new List<Library>(queue)[queue.Count / 2];
        }

        private Library GetLast(Queue<Library> queue)
        {
            Library last = null;
            foreach (var item in queue)
                last = item;
            return last;
        }

        private void Measure(string label, Func<bool> operation)
        {
            Stopwatch sw = Stopwatch.StartNew();
            bool result = operation();
            sw.Stop();
            Console.WriteLine($"{label}: {sw.ElapsedTicks} ticks, result = {result}");
        }

        private void RemoveFromQueue<T>(Queue<T> queue, T item)
        {
            Queue<T> temp = new Queue<T>();

            while (queue.Count > 0)
            {
                T current = queue.Dequeue();

                // Вернуть элемент во временную очередь, если если не тот
                if (!EqualityComparer<T>.Default.Equals(current, item))
                    temp.Enqueue(current);
            }

            // Вернуть элементы из временной очереди
            while (temp.Count > 0)
                queue.Enqueue(temp.Dequeue());
        }
    }
}
