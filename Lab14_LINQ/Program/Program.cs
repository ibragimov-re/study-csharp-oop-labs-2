using OrganizationLib;
using QueryLib;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        var city = new SortedDictionary<string, Queue<Organization>>();

        var district1 = new Queue<Organization>();
        district1.Enqueue(new Organization("Org1", 1500));
        district1.Enqueue(new Organization("Org2", 2000));
        city.Add("District1", district1);

        var district2 = new Queue<Organization>();
        district2.Enqueue(new Organization("Org3", 1000));
        district2.Enqueue(new Organization("Org4", 3000));
        district2.Enqueue(new Organization("Org5", 2500));
        city.Add("District2", district2);


        // Объединение всех организаций в один список для удобства работы с LINQ
        var allOrgs = city
            .SelectMany(district => district.Value)
            .ToList();


        // WHERE
        // =====================
        var whereMethod = Query.GetWhereMethod(allOrgs);
        PrintResult("WHERE METHOD", whereMethod);


        // =====================
        // UNION, INTERSECT, EXCEPT

        // Множества для операций
        var setA = allOrgs.Where(o => o.EmployeesCount >= 1500);
        var setB = allOrgs.Where(o => o.EmployeesCount <= 2500);

        var unionMethod = Query.GetUnion(setA, setB);
        PrintResult("UNION METHOD", unionMethod);

        var intersectMethod = Query.GetIntersect(setA, setB);
        PrintResult("INTERSECT METHOD", intersectMethod);

        var exceptMethod = Query.GetExcept(setA, setB);
        PrintResult("EXCEPT METHOD", exceptMethod);


        // =====================
        // AGGREGATION
        var sum = Query.GetSum(allOrgs);
        PrintResult("SUM METHOD", sum);

        var max = Query.GetMax(allOrgs);
        PrintResult("MAX METHOD", max);

        var min = Query.GetMin(allOrgs);
        PrintResult("MIN METHOD", min);

        var avg = Query.GetAverage(allOrgs);
        PrintResult("AVERAGE METHOD", avg);
        Console.WriteLine();


        // =====================
        // GROUP BY
        var groupMethod = Query.GetGroupMethod(allOrgs);

        Console.WriteLine("====== GROUP METHOD RESULT ======");
        foreach (var item in groupMethod)
        {
            Console.WriteLine($"Group: {item.Key}, Count: {item.Count}");
        }
        Console.WriteLine();


        // =====================
        // LET
        // Только в LINQ-запросах, так как в методах расширения нет аналога оператора let
        var letQuery = Query.GetLetQuery(allOrgs);

        Console.WriteLine("====== LET QUERY RESULT ======");
        foreach (var item in letQuery)
        {
            Console.WriteLine($"Organization: {item.Name}, Size: {item.Category}");
        }
        Console.WriteLine();


        // =====================
        // JOIN

        // Коллекция для соединения
        var districtInfo = new List<(string District, int PeopleCount)>
        {
            ("District1", 10000),
            ("District2", 12000)
        };

        var joinMethod = Query.GetJoinMethod(city, districtInfo);

        Console.WriteLine("====== JOIN METHOD RESULT ======");
        foreach (var item in joinMethod)
        {
            Console.WriteLine($"{item.District}, Organizations: {item.OrgCount}, People count: {item.PeopleCount}");
        }
        Console.WriteLine();


        // =====================
        // PERFORMANCE TEST
        Console.WriteLine($"====== PERFORMANCE TEST - LINQ vs FOR ======");
        var sw = new Stopwatch();

        // LINQ
        sw.Start();
        var linqTest = whereMethod.ToList(); // ToList() для выполнения запроса прямо сейчас
        sw.Stop();
        Console.WriteLine("LINQ ticks: " + sw.ElapsedTicks);

        // FOR
        sw.Restart();
        var forTest = new List<Organization>();
        foreach (var district in city)
        {
            foreach (var org in district.Value)
            {
                if (org.EmployeesCount >= 2000)
                {
                    forTest.Add(org);
                }
            }
        }
        sw.Stop();
        Console.WriteLine("FOR ticks: " + sw.ElapsedTicks);
    }


    static void PrintResult<T>(string title, IEnumerable<T> data)
    {
        Console.WriteLine($"====== {title} RESULT ======");

        foreach (var item in data)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
    }

    static void PrintResult<T>(string title, T number)
    {
        Console.WriteLine($"{title} RESULT: {number}");
    }
}