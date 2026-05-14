using OrganizationLib;
using QueryLib;

// Отключение параллельного выполнения тестов
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace QueryLibTest
{
    public class QueryLibTest
    {
        private readonly List<Organization> _allOrgs;
        private readonly SortedDictionary<string, Queue<Organization>> _city;

        public QueryLibTest()
        {
            _city = new SortedDictionary<string, Queue<Organization>>();

            var district1 = new Queue<Organization>();
            district1.Enqueue(new Organization("Org1", 1500));
            district1.Enqueue(new Organization("Org2", 2000));
            _city.Add("District1", district1);

            var district2 = new Queue<Organization>();
            district2.Enqueue(new Organization("Org3", 1000));
            district2.Enqueue(new Organization("Org4", 3000));
            district2.Enqueue(new Organization("Org5", 2500));
            _city.Add("District2", district2);

            _allOrgs = _city
                .SelectMany(district => district.Value)
                .ToList();
        }


        // =====================
        // WHERE

        [Fact]
        public void GetWhereQuery()
        {
            var result = Query.GetWhereQuery(_allOrgs).ToList();
            Assert.Equal(3, result.Count);
            Assert.Contains(result, o => o.Name == "Org2");
            Assert.Contains(result, o => o.Name == "Org4");
            Assert.Contains(result, o => o.Name == "Org5");
        }

        [Fact]
        public void GetWhereMethod()
        {
            var result = Query.GetWhereMethod(_allOrgs).ToList();
            Assert.Equal(3, result.Count);
            Assert.Contains(result, o => o.Name == "Org2");
            Assert.Contains(result, o => o.Name == "Org4");
            Assert.Contains(result, o => o.Name == "Org5");
        }


        // =====================
        // UNION

        [Fact]
        public void GetUnion()
        {
            var setA = _allOrgs.Where(o => o.EmployeesCount >= 1500);
            var setB = _allOrgs.Where(o => o.EmployeesCount <= 2500);
            var result = Query.GetUnion(setA, setB).ToList();
            
            Assert.Equal(5, result.Count);
        }


        // =====================
        // INTERSECT

        [Fact]
        public void GetIntersect()
        {
            var setA = _allOrgs.Where(o => o.EmployeesCount >= 1500);
            var setB = _allOrgs.Where(o => o.EmployeesCount <= 2500);
            var result = Query.GetIntersect(setA, setB).ToList();

            Assert.Equal(3, result.Count);
            Assert.Contains(result, o => o.Name == "Org1");
            Assert.Contains(result, o => o.Name == "Org2");
            Assert.Contains(result, o => o.Name == "Org5");
        }


        // =====================
        // EXCEPT

        [Fact]
        public void GetExcept()
        {
            var setA = _allOrgs.Where(o => o.EmployeesCount >= 1500);
            var setB = _allOrgs.Where(o => o.EmployeesCount <= 2500);
            var result = Query.GetExcept(setA, setB).ToList();

            Assert.Single(result);
            Assert.Equal("Org4", result[0].Name);
        }


        // =====================
        // AGGREGATION

        [Fact]
        public void GetSum()
        {
            var result = Query.GetSum(_allOrgs);
            Assert.Equal(10000, result);
        }

        [Fact]
        public void GetMax()
        {
            var result = Query.GetMax(_allOrgs);
            Assert.Equal(3000, result);
        }

        [Fact]
        public void GetMin()
        {
            var result = Query.GetMin(_allOrgs);
            Assert.Equal(1000, result);
        }

        [Fact]
        public void GetAverage()
        {
            var result = Query.GetAverage(_allOrgs);
            Assert.Equal(2000, result);
        }


        // =====================
        // GROUP BY

        [Fact]
        public void GetGroupQuery()
        {
            var result = Query.GetGroupQuery(_allOrgs).ToList();
            Assert.Equal(2, result.Count);
            Assert.Contains(result, g => g.Key == true && g.Count == 2);
            Assert.Contains(result, g => g.Key == false && g.Count == 3);
        }

        [Fact]
        public void GetGroupMethod()
        {
            var result = Query.GetGroupMethod(_allOrgs).ToList();
            Assert.Equal(2, result.Count);
            Assert.Contains(result, g => g.Key == true && g.Count == 2);
            Assert.Contains(result, g => g.Key == false && g.Count == 3);
        }


        // =====================
        // LET

        [Fact]
        public void GetLetQuery()
        {
            var result = Query.GetLetQuery(_allOrgs).ToList();
            Assert.Equal(5, result.Count);
            Assert.Contains(result, x => x.Name == "Org4" && x.Category == "Big");
            Assert.Contains(result, x => x.Name == "Org5" && x.Category == "Big");
            Assert.Contains(result, x => x.Name == "Org1" && x.Category == "Small");
        }


        // =====================
        // JOIN

        [Fact]
        public void GetJoinQuery()
        {
            var districtInfo = new List<(string District, int PeopleCount)>
            {
                ("District1", 10000),
                ("District2", 12000)
            };

            var result = Query
                .GetJoinQuery(_city, districtInfo)
                .ToList();

            Assert.Equal(2, result.Count);

            Assert.Contains(result,
                x => x.District == "District1"
                  && x.OrgCount == 2
                  && x.PeopleCount == 10000);

            Assert.Contains(result,
                x => x.District == "District2"
                  && x.OrgCount == 3
                  && x.PeopleCount == 12000);
        }

        [Fact]
        public void GetJoinMethod()
        {
            var districtInfo = new List<(string District, int PeopleCount)>
            {
                ("District1", 10000),
                ("District2", 12000)
            };

            var result = Query
                .GetJoinMethod(_city, districtInfo)
                .ToList();

            Assert.Equal(2, result.Count);

            Assert.Contains(result,
                x => x.District == "District1"
                  && x.OrgCount == 2
                  && x.PeopleCount == 10000);

            Assert.Contains(result,
                x => x.District == "District2"
                  && x.OrgCount == 3
                  && x.PeopleCount == 12000);
        }
    }
}