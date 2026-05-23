using OrganizationLib;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MyCollectionLib
{
    // Обобщенная хеш-таблица (метод цепочек)
    public class MyCollection<T> : IEnumerable<T>, ICollection
        where T : Organization
    {
        private HashNode<T>[] table;

        public bool IsSynchronized => false; // Коллекция не поддерживает многопоточность
        public object SyncRoot => this; // В качестве объекта синхронизации используется сама коллекция

        public int Count { get; private set; }
        public int Capacity => table?.Length ?? 0;

        public MyCollection()
        {
            table = new HashNode<T>[10];
            Count = 0;
        }

        public MyCollection(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than zero");

            table = new HashNode<T>[capacity];
            Count = 0;
        }

        public MyCollection(MyCollection<T> col)
        {
            table = new HashNode<T>[col.Capacity];
            Count = 0;

            foreach (T item in col)
                Add((T)item.Clone());
        }


        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException();

            int index = GetIndex(item.GetHashCode());

            HashNode<T> newNode = new HashNode<T>(item);

            if (table[index] == null)
                table[index] = newNode;
            else
            {
                HashNode<T> currentNode = table[index];
                while (currentNode.Next != null)
                    currentNode = currentNode.Next;

                currentNode.Next = newNode;
            }

            Count++;
        }


        internal void Update(T oldItem, T newItem)
        {
            Remove(oldItem);
            Add(newItem);
        }


        public void AddRange(params T[] items)
        {
            foreach (T item in items)
                Add(item);
        }


        public bool Remove(T item)
        {
            if (item == null)
                throw new ArgumentNullException();

            int hash = item.GetHashCode();
            int index = GetIndex(hash);

            HashNode<T> currentNode = table[index];
            HashNode<T> previousNode = null;

            while (currentNode != null)
            {
                if (currentNode.Key == hash &&
                    currentNode.Value.Equals(item))
                {
                    if (previousNode == null)
                        // Удаление первого элемента в цепочке
                        table[index] = currentNode.Next;
                    else
                        // Удаление элемента в середине или в конце цепочки
                        previousNode.Next = currentNode.Next;

                    Count--;
                    return true;
                }

                previousNode = currentNode;
                currentNode = currentNode.Next;
            }
            return false;
        }


        public void RemoveRange(params T[] items)
        {
            foreach (T item in items)
                Remove(item);
        }


        public T Find(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException();

            int hash = name.GetHashCode();
            int index = GetIndex(hash);

            var currentNode = table[index];

            while (currentNode != null)
            {
                if (currentNode.Key == hash &&
                    currentNode.Value.Name == name)
                {
                    return currentNode.Value;
                }

                currentNode = currentNode.Next;
            }

            return null;
        }


        public MyCollection<T> DeepCopy()
        {
            return new MyCollection<T>(this);
        }


        public MyCollection<T> ShallowCopy()
        {
            MyCollection<T> copy = new MyCollection<T>(Capacity);

            foreach (T item in this)
                copy.Add(item);

            return copy;
        }


        public void Clear()
        {
            for (int i = 0; i < table.Length; i++)
                table[i] = null;

            Count = 0;
        }


        // Метод обобщенного интерфейса IEnumerable<T>, который возвращает обобщенный объект-нумератор
        public IEnumerator<T> GetEnumerator()
        {
            return new MyEnumerator<T>(this);
        }

        // Метод необобщенного интерфейса IEnumerable, который возвращает объект-нумератор
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (index < 0 || index >= array.Length)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (array.Length - index < Count)
                throw new ArgumentException("Not enough space in target array");

            // Перебор элементов коллекции через нумератор и копирование в массив
            foreach (T item in this)
            {
                array.SetValue(item, index);
                index++;
            }
        }

        // Получение элемента корзины по индексу (для использования в нумераторе)
        internal HashNode<T> GetBucket(int index)
        {
            return table[index];
        }

        private int GetIndex(int hash)
        {
            return (Math.Abs(hash) % Capacity);
        }
    }
}
