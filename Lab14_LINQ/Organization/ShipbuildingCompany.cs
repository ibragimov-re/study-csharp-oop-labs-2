using System;

namespace OrganizationLib
{
    // Производный класс судостроительной компании
    public class ShipbuildingCompany : Organization
    {
        private const int MaxShips = 50000;

        private int shipCount;

        // Свойство для получения ссылки на объект базового класса
        public Organization BaseOrganization
        {
            get => this;
        }

        public int ShipCount
        {
            get => shipCount;
            set
            {
                if (value < 0)
                    throw new ArgumentException($"Ship count cannot be negative");

                if (value > MaxShips)
                    throw new ArgumentException($"Ship count is too large (maximum {MaxShips})");

                shipCount = value;
            }
        }

        public ShipbuildingCompany() : base()
        {
            ShipCount = 0;
        }

        public ShipbuildingCompany(string name, int employees, int shipCount)
            : base(name, employees)
        {
            ShipCount = shipCount;
        }

        public ShipbuildingCompany(ShipbuildingCompany other)
            : base(other)
        {
            ShipCount = other.ShipCount;
        }

        public override void Init()
        {
            base.Init();
            Console.Write("Enter ship count: ");

            if (!int.TryParse(Console.ReadLine(), out int count))
                throw new ArgumentException("Ship count must be a valid integer");

            ShipCount = count;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($", ship count: {ShipCount}");
        }

        public override string ToString()
        {
            return $"{base.ToString()}, ship count: {ShipCount}";
        }

        public override void RandomInit()
        {
            base.RandomInit();
            ShipCount = new Random().Next(0, MaxShips + 1);
        }

        public override bool Equals(object obj)
        {
            if (obj is ShipbuildingCompany other)
                return base.Equals(other) && ShipCount == other.ShipCount;
            return false;
        }

        public override object Clone()
        {
            return new ShipbuildingCompany(this);
        }
    }
}
