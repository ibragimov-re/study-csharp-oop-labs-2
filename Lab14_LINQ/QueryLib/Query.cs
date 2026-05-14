using OrganizationLib;

namespace QueryLib
{
    public static class Query
    {
        // =====================
        // WHERE

        // Query syntax
        public static IEnumerable<Organization> GetWhereQuery(IEnumerable<Organization> orgs)
        {
            return
                from org in orgs
                where org.EmployeesCount >= 2000
                select org;
        }

        // Method syntax
        public static IEnumerable<Organization> GetWhereMethod(IEnumerable<Organization> orgs)
        {
            return orgs.Where(org => org.EmployeesCount >= 2000);
        }


        // =====================
        // UNION, INTERSECT, EXCEPT
        // Только methods syntax, так как query syntax не поддерживает эти операции

        public static IEnumerable<Organization> GetUnion(IEnumerable<Organization> setA, IEnumerable<Organization> setB)
        {
            return setA.Union(setB);
        }

        public static IEnumerable<Organization> GetIntersect(IEnumerable<Organization> setA, IEnumerable<Organization> setB)
        {
            return setA.Intersect(setB);
        }

        public static IEnumerable<Organization> GetExcept(IEnumerable<Organization> setA, IEnumerable<Organization> setB)
        {
            return setA.Except(setB);
        }


        // =====================
        // AGGREGATION
        // Только methods syntax, так как query syntax не поддерживает эти операции

        public static int GetSum(IEnumerable<Organization> orgs)
        {
            return orgs.Sum(org => org.EmployeesCount);
        }

        public static int GetMax(IEnumerable<Organization> orgs)
        {
            return orgs.Max(org => org.EmployeesCount);
        }

        public static int GetMin(IEnumerable<Organization> orgs)
        {
            return orgs.Min(org => org.EmployeesCount);
        }

        public static double GetAverage(IEnumerable<Organization> orgs)
        {
            return orgs.Average(org => org.EmployeesCount);
        }


        // =====================
        // GROUP BY

        // Query syntax
        public static IEnumerable<GroupResult> GetGroupQuery(IEnumerable<Organization> orgs)
        {
            return
                from org in orgs
                group org by org.EmployeesCount >= 2500 into grp
                select new GroupResult
                {
                    Key = grp.Key,
                    Count = grp.Count()
                };
        }

        // Method syntax
        public static IEnumerable<GroupResult> GetGroupMethod(IEnumerable<Organization> orgs)
        {
            return orgs
                .GroupBy(org => org.EmployeesCount >= 2500)
                .Select(grp => new GroupResult
                {
                    Key = grp.Key,
                    Count = grp.Count()
                });
        }


        // =====================
        // LET
        // Только LINQ query syntax, так как methods syntax не поддерживает эту операцию
        public static IEnumerable<LetResult> GetLetQuery(IEnumerable<Organization> orgs)
        {
            return
                from org in orgs
                let category = org.EmployeesCount >= 2500 ? "Big" : "Small"
                select new LetResult
                {
                    Name = org.Name,
                    Category = category
                };
        }


        // =====================
        // JOIN

        // Query syntax
        public static IEnumerable<JoinResult> GetJoinQuery(SortedDictionary<string, Queue<Organization>> city, List<(string District, int PeopleCount)> districtInfo)
        {
            return
                from district in city
                join info in districtInfo
                on district.Key equals info.District
                select new JoinResult
                {
                    District = district.Key,
                    OrgCount = district.Value.Count,
                    PeopleCount = info.PeopleCount
                };
        }

        // Methods syntax
        public static IEnumerable<JoinResult> GetJoinMethod(SortedDictionary<string, Queue<Organization>> city, List<(string District, int PeopleCount)> districtInfo)
        {
            return city.Join(
                districtInfo,
                district => district.Key,
                info => info.District,
                (district, info) => new JoinResult
                {
                    District = district.Key,
                    OrgCount = district.Value.Count,
                    PeopleCount = info.PeopleCount
                });
        }
    }
}