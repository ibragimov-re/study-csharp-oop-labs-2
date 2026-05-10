using MyCollectionLib;
using System.Collections;
using System.Collections.Generic;

namespace OrganizationLib
{
    // Enumerator для хеш-таблицы (метод цепочек)
    internal class MyEnumerator<T> : IEnumerator<T> where T : Organization
    {
        private MyCollection<T> collection;
        private int index; // Индекс текущей корзины хеш-таблицы
        private HashNode<T> currentNode; // Текущий узел цепочки при переборе коллекции

        public MyEnumerator(MyCollection<T> col)
        {
            collection = col;
            index = -1;
            currentNode = null;
        }

        public T Current => currentNode.Value;

        // Свойство, которое реализует интерфейс IEnumerator и преобразует Т в object
        object IEnumerator.Current => Current;

        // Переход к следующему элементу списка, реализует интерфейс IEnumerator
        public bool MoveNext()
        {
            // Первый запуск или после Reset
            if (currentNode == null)
            {
                index = 0;

                while (index < collection.Capacity)
                {
                    currentNode = collection.GetBucket(index);
                    if (currentNode != null)
                        return true;

                    index++;
                }

                return false;
            }

            if (currentNode.Next != null)
            {
                // Переход к следующему элементу коллекции
                currentNode = currentNode.Next;
                return true;
            }

            // Переход к следующей корзине
            index++;

            while (index < collection.Capacity)
            {
                if (collection.GetBucket(index) != null)
                {
                    // Установить текущий элемент на начало новой корзины
                    currentNode = collection.GetBucket(index);
                    return true;
                }
                index++;
            }

            return false;
        }

        // Метод, который ставит текущий элемент на начало коллекции, реализует интерфейс IEnumerator
        public void Reset()
        {
            index = -1;
            currentNode = null;
        }

        // Метод для удаления ресурсов нумератора, реализует интерфейс IEnumerator<T>
        public void Dispose() { }
    }
}
