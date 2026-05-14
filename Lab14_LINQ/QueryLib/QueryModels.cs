namespace QueryLib
{
    public class GroupResult
    {
        public bool Key { get; set; }
        public int Count { get; set; }
    }

    public class LetResult
    {
        public string Name { get; set; }
        public string Category { get; set; }
    }

    public class JoinResult
    {
        public string District { get; set; }
        public int OrgCount { get; set; }
        public int PeopleCount { get; set; }
    }
}