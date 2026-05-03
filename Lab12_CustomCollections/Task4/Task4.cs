using MyCollectionLib;
using OrganizationLib;

class Task4
{
    static void Main()
    {
        var orgCol = new MyCollection<Organization>();
        InitializeAndPrintCollection(orgCol);

        while (true)
        {
            PrintMenu();

            Console.Write("Enter a number: ");
            if (!int.TryParse(Console.ReadLine(), out int operationNumber))
            {
                Console.WriteLine("Unknown option, try again\n");
                continue;
            }
            Console.WriteLine();

            switch (operationNumber)
            {
                case 1:
                    PrintCollection(orgCol);
                    break;

                case 2:
                    AddRandomOrganization(orgCol);
                    break;

                case 3:
                    RemoveOrganizationByName(orgCol);
                    break;

                case 4:
                    FindAndPrintOrganizationByName(orgCol);
                    break;

                case 5:
                    ShallowCopyDemo(orgCol);
                    break;

                case 6:
                    DeepCopyDemo(orgCol);
                    break;

                case 7:
                    ClearCollection(orgCol);
                    break;

                case 0:
                    return;

                default:
                    Console.WriteLine("Unknown option, try again\n");
                    continue;
            }
            Console.WriteLine("\n");
        }
    }


    static void InitializeAndPrintCollection(MyCollection<Organization> col)
    {
        Console.WriteLine("Collection with different organization types");

        var org1 = new Factory();
        var org2 = new InsuranceCompany();
        var org3 = new Library();
        var org4 = new ShipbuildingCompany();

        org1.RandomInit();
        org2.RandomInit();
        org3.RandomInit();
        org4.RandomInit();

        col.AddRange(org1, org2, org3, org4);

        foreach (Organization org in col)
        {
            org.Show();
        }

        Console.WriteLine("");
    }


    static void PrintMenu()
    {
        Console.WriteLine("===== LAB12 TASK4 MENU =====");
        Console.WriteLine("1 - Print collection");
        Console.WriteLine("2 - Add random organization");
        Console.WriteLine("3 - Remove organization");
        Console.WriteLine("4 - Find and print organization");
        Console.WriteLine("5 - Shallow copy demo");
        Console.WriteLine("6 - Deep copy demo");
        Console.WriteLine("7 - Clear collection");
        Console.WriteLine("0 - Exit");
    }


    static void PrintCollection(MyCollection<Organization> col)
    {
        if (col.Count < 1)
        {
            Console.WriteLine("Collection is empty");
        }

        foreach (Organization org in col)
        {
            org.Show();
        }
    }


    static void AddRandomOrganization(MyCollection<Organization> col)
    {
        Organization org = new Organization();

        int orgType = new Random().Next(1, 4 + 1);
        switch (orgType)
        {
            case 1:
                org = new Factory();
                break;

            case 2:
                org = new InsuranceCompany();
                break;

            case 3:
                org = new Library();
                break;

            case 4:
                org = new ShipbuildingCompany();
                break;

            default:
                org = new Organization();
                break;
        }

        org.RandomInit();
        col.Add(org);

        Console.WriteLine($"Added organization: {org.Name}");
        Console.WriteLine("Collection:");
        PrintCollection(col);
    }


    static void RemoveOrganizationByName(MyCollection<Organization> col)
    {
        Console.Write("Enter organization name: ");
        string? orgName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(orgName))
        {
            Console.WriteLine("Invalid name");
            return;
        }

        var org = col.Find(orgName);
        col.Remove(org);

        Console.WriteLine("Collection after removal:");
        PrintCollection(col);
    }


    static void FindAndPrintOrganizationByName(MyCollection<Organization> col)
    {
        Console.Write("Enter organization name: ");
        string? orgName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(orgName))
        {
            Console.WriteLine("Invalid name");
            return;
        }

        var org = col.Find(orgName);

        if (org == null)
        {
            Console.WriteLine($"Organization not found");
            return;
        }

        org.Show();
    }


    static void ShallowCopyDemo(MyCollection<Organization> col)
    {
        var shallowCopy = col.ShallowCopy();
        Console.WriteLine("Shallow copy created. Set the employees count in each organization to 100...");

        if (col.Count > 0)
        {
            foreach (Organization org in shallowCopy)
            {
                org.EmployeesCount = 100;
            }
        }

        Console.WriteLine("Original collection:");
        PrintCollection(col);
        Console.WriteLine("Shallow copy:");
        PrintCollection(shallowCopy);
    }


    static void DeepCopyDemo(MyCollection<Organization> col)
    {
        var deepCopy = col.DeepCopy();
        Console.WriteLine("Deep copy created. Set the employees count in each organization to 200...");
        if (col.Count > 0)
        {
            foreach (Organization org in deepCopy)
            {
                org.EmployeesCount = 200;
            }
        }
        Console.WriteLine("Original collection:");
        PrintCollection(col);
        Console.WriteLine("Deep copy:");
        PrintCollection(deepCopy);
    }


    static void ClearCollection(MyCollection<Organization> col)
    {
        col.Clear();
        PrintCollection(col);
    }
}