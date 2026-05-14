using System;

namespace OrganizationLib
{
    // Производный класс страховой компании
    public class InsuranceCompany : Organization
    {
        private const int MinTypeLength = 3;
        private const int MaxTypeLength = 15;

        private string insuranceType;

        // Свойство для получения ссылки на объект базового класса
        public Organization BaseOrganization
        {
            get => this;
        }

        public string InsuranceType
        {
            get => insuranceType;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Insurance type cannot be empty");

                if (value.Length < MinTypeLength)
                    throw new ArgumentException($"Insurance type is too short (minimum {MinTypeLength} characters)");

                if (value.Length > MaxTypeLength)
                    throw new ArgumentException($"Insurance type is too long (maximum {MaxTypeLength} characters)");

                insuranceType = value.Trim();
            }
        }

        public InsuranceCompany() : base()
        {
            InsuranceType = "Unknown";
        }

        public InsuranceCompany(string name, int employees, string insuranceType)
            : base(name, employees)
        {
            InsuranceType = insuranceType;
        }

        public InsuranceCompany(InsuranceCompany other)
            : base(other)
        {
            InsuranceType = other.InsuranceType;
        }

        public override void Init()
        {
            base.Init();
            Console.Write("Enter insurance type: ");
            InsuranceType = Console.ReadLine();
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($", insurance type: {InsuranceType}");
        }

        public override string ToString()
        {
            return $"{base.ToString()}, insurance type: {InsuranceType}";
        }

        public override void RandomInit()
        {
            base.RandomInit();
            string[] types = { "Health", "Life", "Car", "Property", "Travel", "Business" };
            InsuranceType = types[new Random().Next(types.Length)];
        }

        public override bool Equals(object obj)
        {
            if (obj is InsuranceCompany other)
                return base.Equals(other) && InsuranceType == other.InsuranceType;
            return false;
        }

        public override object Clone()
        {
            return new InsuranceCompany(this);
        }
    }
}
