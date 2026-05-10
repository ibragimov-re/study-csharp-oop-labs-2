using OrganizationLib;
using MyNewCollectionLib;

class Program
{
    static void Main()
    {
        var collection1 = new MyNewCollection<Organization>();
        collection1.Name = "Collection 1";

        var collection2 = new MyNewCollection<Organization>();
        collection2.Name = "Collection 2";

        var journal1 = new Journal();
        var journal2 = new Journal();


        // Подписки journal1
        // =========================
        collection1.CollectionCountChanged += journal1.CollectionEventHandler;
        collection1.CollectionReferenceChanged += journal1.CollectionEventHandler;


        // Подписки journal2
        // =========================
        collection1.CollectionReferenceChanged += journal2.CollectionEventHandler;
        collection2.CollectionReferenceChanged += journal2.CollectionEventHandler;


        // Добавление элементов
        // =========================
        var org1 = new Factory();
        var org2 = new ShipbuildingCompany();
        var org3 = new InsuranceCompany();
        var org4 = new Library();

        org1.RandomInit();
        org2.RandomInit();
        org3.RandomInit();
        org4.RandomInit();

        collection1.Add(org1);
        collection1.Add(org2);

        collection2.Add(org3);
        collection2.Add(org4);


        // Удаление элементов
        // =========================
        collection1.Remove(0);
        collection2.Remove(0);


        // Замена элементов
        // =========================
        var org5 = new Factory();
        var org6 = new ShipbuildingCompany();

        org5.RandomInit();
        org6.RandomInit();

        collection1[0] = org5;
        collection2[0] = org6;


        // Вывод journal1
        // =========================
        Console.WriteLine("========================= JOURNAL 1 =========================");
        Console.WriteLine(journal1);


        // Вывод journal2
        // =========================
        Console.WriteLine("========================= JOURNAL 2 =========================");
        Console.WriteLine(journal2);
    }
}