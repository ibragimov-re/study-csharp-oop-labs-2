using MyCollectionLib;
using OrganizationLib;

// Отключение параллельного выполнения тестов для избежания конфликтов при перенаправлении консольного ввода/вывода
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace MyCollectionLibTest
{
    public class MyCollectionTests
    {
        [Fact]
        public void DefaultConstructor()
        {
            var collection = new MyCollection<Organization>();
            Assert.NotNull(collection);
            Assert.Equal(10, collection.Capacity);
        }

        [Fact]
        public void ConstructorWithCapacity()
        {
            var collection = new MyCollection<Organization>(20);
            Assert.NotNull(collection);
            Assert.Equal(20, collection.Capacity);
        }

        [Fact]
        public void ConstructorWithNegativeCapacity()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MyCollection<Organization>(-5));
        }

        [Fact]
        public void ConstructorWithZeroCapacity()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MyCollection<Organization>(0));
        }

        [Fact]
        public void copyConstructor()
        {
            var original = new MyCollection<Organization>();
            original.Add(new Organization("Org1", 100));
            original.Add(new Organization("Org2", 200));
            var copy = new MyCollection<Organization>(original);
            Assert.NotNull(copy);
            Assert.Equal(original.Capacity, copy.Capacity);
            Assert.Equal(original.Count, copy.Count);

            foreach (var org in original)
                Assert.Contains(org, copy);
        }

        [Fact]
        public void AddNullItem()
        {
            var collection = new MyCollection<Organization>();
            Assert.Throws<ArgumentNullException>(() => collection.Add(null));
        }

        [Fact]
        public void AddRangeOfItems()
        {
            var collection = new MyCollection<Organization>();
            var org1 = new Organization("Org141441", 100);
            var org2 = new Organization("Org898442", 200);
            var org3 = new Organization("Org09420", 300);
            var org4 = new Organization("Org2411", 400);
            var org5 = new Organization("Org234", 500);
            var org6 = new Organization("Org39582", 600);
            var org7 = new Organization("Org37682", 700);
            var org8 = new Organization("Org2651", 800);
            var org9 = new Organization("Org221", 900);

            collection.AddRange(org1, org2, org3, org4, org5, org6, org7, org8, org9);

            Assert.Equal(9, collection.Count);
        }

        [Fact]
        public void RemoveItem()
        {
            var collection = new MyCollection<Organization>();
            var org = new Organization("Org1", 100);
            collection.Add(org);
            Assert.True(collection.Remove(org));
            Assert.DoesNotContain(org, collection);
        }

        [Fact]
        public void RemoveNonExistentItem()
        {
            var collection = new MyCollection<Organization>();
            var org = new Organization("Org1", 100);
            Assert.False(collection.Remove(org));
        }

        [Fact]
        public void RemoveRangeOfItems()
        {
            var collection = new MyCollection<Organization>();
            var org1 = new Organization("Org141441", 100);
            var org2 = new Organization("Org898442", 200);
            var org3 = new Organization("Org09420", 300);
            var org4 = new Organization("Org2411", 400);
            var org5 = new Organization("Org234", 500);
            var org6 = new Organization("Org39582", 600);
            var org7 = new Organization("Org37682", 700);
            var org8 = new Organization("Org2651", 800);
            var org9 = new Organization("Org221", 900);
            collection.AddRange(org1, org2, org3, org4, org5, org6, org7, org8, org9);
            collection.RemoveRange(org1, org2, org3, org4, org5, org6, org7, org8, org9);
            Assert.Empty(collection);
        }

        [Fact]
        public void Find()
        {
            var collection = new MyCollection<Organization>();
            var org1 = new Organization("Org141441", 100);
            var org2 = new Organization("Org898442", 200);
            var org3 = new Organization("Org09420", 300);
            var org4 = new Organization("Org2411", 400);
            var org5 = new Organization("Org234", 500);
            var org6 = new Organization("Org39582", 600);
            var org7 = new Organization("Org37682", 700);
            var org8 = new Organization("Org2651", 800);
            var org9 = new Organization("Org221", 900);
            collection.AddRange(org1, org2, org3, org4, org5, org6, org7, org8, org9);
            var found = collection.Find("Org09420");
            Assert.NotNull(found);
            Assert.Equal(org3.EmployeesCount, found.EmployeesCount);
        }

        [Fact]
        public void FindNonExistentItem()
        {
            var collection = new MyCollection<Organization>();
            var found = collection.Find("NonExistentOrg");
            Assert.Null(found);
        }

        [Fact]
        public void DeepCopy()
        {
            var original = new MyCollection<Organization>();
            var org1 = new Organization("Org141441", 100);
            var org2 = new Organization("Org898442", 200);
            original.AddRange(org1, org2);

            var copy = original.DeepCopy();
            Assert.NotNull(copy);
            Assert.Equal(original.Capacity, copy.Capacity);
            Assert.Equal(original.Count, copy.Count);
            foreach (var org in original)
                Assert.Contains(org, copy);

            // Изменение оригинала не должно влиять на копию
            org1.EmployeesCount = 150;
            Assert.Equal(100, copy.Find("Org141441").EmployeesCount);
        }

        [Fact]
        public void ShallowCopy()
        {
            var original = new MyCollection<Organization>();
            var org1 = new Organization("Org141441", 100);
            var org2 = new Organization("Org898442", 200);
            original.AddRange(org1, org2);

            var copy = original.ShallowCopy();
            Assert.NotNull(copy);
            Assert.Equal(original.Capacity, copy.Capacity);
            Assert.Equal(original.Count, copy.Count);
            foreach (var org in original)
                Assert.Contains(org, copy);

            // Изменение оригинала должно влиять на копию
            org1.EmployeesCount = 150;
            Assert.Equal(150, copy.Find("Org141441").EmployeesCount);
        }

        [Fact]
        public void Clear()
        {
            var collection = new MyCollection<Organization>();
            var org1 = new Organization("Org141441", 100);
            var org2 = new Organization("Org898442", 200);
            collection.AddRange(org1, org2);

            collection.Clear();

            Assert.Empty(collection);
        }

        [Fact]
        public void CopyToArray()
        {
            var collection = new MyCollection<Organization>();
            var org1 = new Organization("Org141441", 100);
            var org2 = new Organization("Org898442", 200);
            collection.AddRange(org1, org2);

            Organization[] array = new Organization[collection.Count];
            collection.CopyTo(array, 0);

            Assert.Equal(collection.Count, array.Length);
            Assert.Contains(org1, array);
            Assert.Contains(org2, array);
        }

        [Fact]
        public void CopyToArrayWithNullIndex()
        {
            var collection = new MyCollection<Organization>();
            var org1 = new Organization("Org141441", 100);
            var org2 = new Organization("Org898442", 200);
            collection.AddRange(org1, org2);
            Assert.Throws<ArgumentNullException>(() => collection.CopyTo(null, 0));
        }


        [Fact]
        public void CopyToArrayWithInvalidIndex()
        {
            var collection = new MyCollection<Organization>();
            var org1 = new Organization("Org141441", 100);
            var org2 = new Organization("Org898442", 200);
            collection.AddRange(org1, org2);

            Organization[] array = new Organization[collection.Count];
            Assert.Throws<ArgumentOutOfRangeException>(() => collection.CopyTo(array, -1));
        }

        [Fact]
        public void CopyToArrayWithNotEnoughSpace()
        {
            var collection = new MyCollection<Organization>();
            var org1 = new Organization("Org141441", 100);
            var org2 = new Organization("Org898442", 200);
            collection.AddRange(org1, org2);
            Organization[] array = new Organization[1];
            Assert.Throws<ArgumentException>(() => collection.CopyTo(array, 0));
        }
    }
}