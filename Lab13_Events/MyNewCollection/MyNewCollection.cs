using MyCollectionLib;
using OrganizationLib;

namespace MyNewCollectionLib
{
    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);

    public class MyNewCollection<T> : MyCollection<T>
        where T : Organization
    {
        public string Name { get; set; } = "";

        public event CollectionHandler? CollectionCountChanged;
        public event CollectionHandler? CollectionReferenceChanged;

        public MyNewCollection() : base() { }

        public MyNewCollection(int capacity) : base(capacity) { }


        public new void Add(T item)
        {
            base.Add(item);

            OnCollectionCountChanged("Added", item);
        }


        public void Add(params T[] items)
        {
            foreach (T item in items)
            {
                Add(item);
            }
        }


        public new bool Remove(T item)
        {
            bool result = base.Remove(item);

            if (result)
                OnCollectionCountChanged("Removed", item);

            return result;
        }


        public bool Remove(int index)
        {
            if (index < 0 || index >= Count)
                return false;

            int i = 0;

            foreach (T item in this)
            {
                if (i == index)
                {
                    bool result = base.Remove(item);

                    if (result)
                        OnCollectionCountChanged("Removed", item);

                    return result;
                }

                i++;
            }

            return false;
        }


        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException();

                int i = 0;

                foreach (T item in this)
                {
                    if (i == index)
                        return item;

                    i++;
                }

                throw new InvalidOperationException();
            }

            set
            {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException();

                int i = 0;

                foreach (T item in this)
                {
                    if (i == index)
                    {
                        // Удаление старого элемента
                        base.Remove(item);

                        // Добавление нового элемента
                        base.Add(value);

                        OnCollectionReferenceChanged("Replaced", value);

                        return;
                    }
                    i++;
                }
            }
        }

        protected void OnCollectionCountChanged(string message, object item)
        {
            CollectionCountChanged?.Invoke(this,
                new CollectionHandlerEventArgs(Name, message, item));
        }

        protected void OnCollectionReferenceChanged(string message, object item)
        {
            CollectionReferenceChanged?.Invoke(this,
                new CollectionHandlerEventArgs(Name, message, item));
        }
    }
}
