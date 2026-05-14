using System;

namespace OrganizationLib
{
    // Производный класс библиотеки
    public class Library : Organization
    {
        private const int MaxBooks = 50_000_000;

        private int booksCount;

        // Свойство для получения ссылки на объект базового класса
        public Organization BaseOrganization
        {
            get => this;
        }

        public int BooksCount
        {
            get => booksCount;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Books count cannot be negative");

                if (value > MaxBooks)
                    throw new ArgumentException($"Books count is too large (maximum {MaxBooks})");

                booksCount = value;
            }
        }

        public Library() : base()
        {
            BooksCount = 0;
        }

        public Library(string name, int employees, int books)
            : base(name, employees)
        {
            BooksCount = books;
        }

        public Library(Library other)
            : base(other)
        {
            BooksCount = other.BooksCount;
        }

        public override void Init()
        {
            base.Init();
            Console.Write("Enter books count: ");

            if (!int.TryParse(Console.ReadLine(), out int count))
                throw new ArgumentException("Books count must be a valid integer");

            BooksCount = count;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($", books count: {BooksCount}");
        }

        public override string ToString()
        {
            return $"{base.ToString()}, books count: {BooksCount}";
        }

        public override void RandomInit()
        {
            base.RandomInit();
            BooksCount = new Random().Next(0, MaxBooks + 1);
        }

        public override bool Equals(object obj)
        {
            if (obj is Library other)
                return base.Equals(other) && BooksCount == other.BooksCount;
            return false;
        }

        public override object Clone()
        {
            return new Library(this);
        }
    }
}
