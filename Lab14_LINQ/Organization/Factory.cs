using System;

namespace OrganizationLib
{
    // Производный класс завода
    public class Factory : Organization
    {
        private const int MinTypeLength = 3;
        private const int MaxTypeLength = 20;

        private string productionType;

        // Свойство для получения ссылки на объект базового класса
        public Organization BaseOrganization
        {
            get => this;
        }

        public string ProductionType
        {
            get => productionType;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Production type cannot be empty");

                if (value.Length < MinTypeLength)
                    throw new ArgumentException($"Production type is too short (minimum {MinTypeLength} characters)");

                if (value.Length > MaxTypeLength)
                    throw new ArgumentException($"Production type is too long (maximum {MaxTypeLength} characters)");

                productionType = value.Trim();
            }
        }

        public Factory() : base()
        {
            ProductionType = "Unknown";
        }

        public Factory(string name, int employees, string type)
            : base(name, employees)
        {
            ProductionType = type;
        }

        public Factory(Factory other)
            : base(other)
        {
            ProductionType = other.ProductionType;
        }

        public override void Init()
        {
            base.Init();
            Console.Write("Enter production type: ");
            ProductionType = Console.ReadLine();
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($", production type: {ProductionType}");
        }

        public override string ToString()
        {
            return $"{base.ToString()}, production type: {ProductionType}";
        }

        public override void RandomInit()
        {
            base.RandomInit();
            string[] types = { "Electronics", "Food", "Machinery", "Furniture", "Chemicals" };
            ProductionType = types[new Random().Next(types.Length)];
        }

        public override bool Equals(object obj)
        {
            if (obj is Factory other)
                return base.Equals(other) && ProductionType == other.ProductionType;
            return false;
        }

        public override object Clone()
        {
            return new Factory(this);
        }
    }
}
