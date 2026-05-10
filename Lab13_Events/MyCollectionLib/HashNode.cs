using OrganizationLib;

namespace MyCollectionLib
{
    // Элемент хеш-таблицы
    internal class HashNode<T> where T : Organization
    {
        internal int Key { get; }
        internal T Value { get; }
        internal HashNode<T> Next { get; set; }

        internal HashNode(T val)
        {
            Value = val;
            Key = Value.Name.GetHashCode();
            Next = null;
        }

        public override string ToString()
        {
            return Key + ":" + Value.ToString();
        }
    }
}
