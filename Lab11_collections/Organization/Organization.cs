using System;

namespace OrganizationLib
{
    // Базовый класс организации
    public class Organization : IInit, IComparable
    {
        // Константы для проверки
        protected const int MinNameLength = 3;
        protected const int MaxNameLength = 50;
        protected const int MaxEmployeesCount = 5_000_000;

        // Поля
        protected string name;
        protected int employeesCount;

        // Свойства, предусматривающие проверку ограничений
        // =====================================================================
        public string Name
        {
            get => name;
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Organization name cannot be empty");

                if (value.Length > MaxNameLength)
                    throw new ArgumentException($"Organization name is too long (maximum {MaxNameLength} characters)");

                if (value.Length < MinNameLength)
                    throw new ArgumentException($"Organization name is too short (minimum {MinNameLength} characters)");

                // Удаление случайных пробелов в начале и конце строки
                name = value.Trim();
            }
        }

        public int EmployeesCount
        {
            get => employeesCount;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Employee count cannot be negative");

                if (value > MaxEmployeesCount)
                    throw new ArgumentException($"Employee count is too large (maximum {MaxEmployeesCount})");

                employeesCount = value;
            }
        }

        // Конструкторы
        // =====================================================================
        // Без параметров
        public Organization()
        {
            Name = "Unknown";
            EmployeesCount = 0;
        }

        // С параметрами
        public Organization(string name, int employees)
        {
            Name = name;
            EmployeesCount = employees;
        }

        // Копирования
        public Organization(Organization other)
        {
            Name = other.Name;
            EmployeesCount = other.EmployeesCount;
        }

        // Методы
        // =====================================================================
        // Просмотр свойств объекта. Невиртуальная функция (без переопределения в производных классах)
        public void ShowNonVirtual()
        {
            Console.WriteLine($"Name: {Name}, employees: {EmployeesCount}");
        }

        // Просмотр свойств объекта. Виртуальная функция (с переопределением в производных классах)
        public virtual void Show()
        {
            Console.Write($"Name: {Name}, employees: {EmployeesCount}");
        }

        // Привести к строке с информацией
        public override string ToString()
        {
            return Name;
        }

        // Ввод свойств объекта с клавиатуры
        public virtual void Init()
        {
            Console.Write("Enter organization name: ");
            Name = Console.ReadLine();

            Console.Write("Enter employee count: ");
            if (!int.TryParse(Console.ReadLine(), out int count))
                throw new ArgumentException("Employee count must be a valid integer");
            EmployeesCount = count;
        }

        // Инициализация свойств объекта с помощью датчика случайных чисел (ДСЧ)
        public virtual void RandomInit()
        {
            Random rand = new Random();
            Name = "Org_" + Guid.NewGuid();
            EmployeesCount = rand.Next(1, MaxEmployeesCount);
        }

        public override bool Equals(object obj)
        {
            if (obj is Organization org)
                return Name == org.Name && EmployeesCount == org.EmployeesCount;
            return false;
        }

        public int CompareTo(object obj)
        {
            var other = obj as Organization;

            if (other == null)
                throw new ArgumentException("Object is not an Organization");

            // Ordinal - сравнение символов по Unicode кодам
            return string.Compare(Name, other.Name, StringComparison.Ordinal);
        }
    }
}
