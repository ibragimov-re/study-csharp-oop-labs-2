using OrganizationLib;

namespace MyCollectionLib
{
    // Элемент хеш-таблицы
    internal class HashNode<T>
    {
        internal int Key => Value?.GetHashCode() ?? 0;
        internal T Value { get; }
        internal HashNode<T> Next { get; set; }

        internal HashNode(T val)
        {
            Value = val;
            Next = null;
        }

        public override string ToString()
        {
            return Key + ":" + Value.ToString();
        }
    }
}
