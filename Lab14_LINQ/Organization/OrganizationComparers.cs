using System;
using System.Collections;

namespace OrganizationLib
{
    // Сортировка организаций по количеству сотрудников
    public class OrganizationByEmployeesComparer : IComparer
    {
        public int Compare(object obj1, object obj2)
        {
            var org1 = obj1 as Organization;
            var org2 = obj2 as Organization;

            if (org1 == null || org2 == null)
                throw new ArgumentException("Objects must be Organization");

            return org1.EmployeesCount.CompareTo(org2.EmployeesCount);
        }
    }
}
