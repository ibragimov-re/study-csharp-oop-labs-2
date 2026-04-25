using System;

namespace OrganizationLib
{
    // Обертка для имени. Необходима для демонтрации различия ShallowCopy от Clone,
    // так как string и int неизменяемые типы и копируются даже в MemberwiseClone
    public class MutableName
    {
        public string Value { get; set; }
    }

    // Класс вне иерархии Organization, но реализует IInit
    public class School : IInit, ICloneable
    {
        protected const int MinNameLength = 3;
        protected const int MaxNameLength = 30;
        protected const int MaxStudentsCount = 5000;

        protected MutableName name = new MutableName();
        protected int studentsCount;

        public string Name
        {
            get => name.Value;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty");

                if (value.Length > MaxNameLength)
                    throw new ArgumentException($"Name is too long (maximum {MaxNameLength} characters)");

                if (value.Length < MinNameLength)
                    throw new ArgumentException($"Name is too short (minimum {MinNameLength} characters)");

                name.Value = value.Trim();
            }
        }

        public int StudentsCount
        {
            get => studentsCount;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Students count cannot be negative");

                if (value > MaxStudentsCount)
                    throw new ArgumentException($"Students count is too large (maximum {MaxStudentsCount})");

                studentsCount = value;
            }
        }

        public School()
        {
            Name = "Unknown";
            StudentsCount = 0;
        }

        public School(string name, int students)
        {
            Name = name;
            StudentsCount = students;
        }

        public School(School other)
        {
            Name = other.Name;
            StudentsCount = other.StudentsCount;
        }

        public virtual void Show()
        {
            Console.WriteLine($"Name: {Name}, students: {StudentsCount}");
        }

        public virtual void Init()
        {
            Console.Write("Enter school name: ");
            Name = Console.ReadLine();

            Console.Write("Enter students count: ");
            if (!int.TryParse(Console.ReadLine(), out int count))
                throw new ArgumentException("Students count must be a valid integer");
            StudentsCount = count;
        }

        public virtual void RandomInit()
        {
            Random rand = new Random();
            Name = "School" + rand.Next(1, 1000 + 1);
            StudentsCount = rand.Next(10, MaxStudentsCount + 1);
        }

        public override bool Equals(object obj)
        {
            if (obj is School school)
                return Name == school.Name && StudentsCount == school.StudentsCount;
            return false;
        }

        // Поверхностное копирование
        public School ShallowCopy()
        {
            return (School)this.MemberwiseClone();
        }

        // Глубокое копирование
        public object Clone()
        {
            return new School("Clone of " + this.Name, this.studentsCount);
        }
    }
}