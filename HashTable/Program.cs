using HashTable.Classes;
namespace HashTable;
internal class Program
{
    static void Main()
    {
        Console.WriteLine("Test for StaticHashTable<int, int>:");
        StaticHashTable<int, int> hashTable = new();
        hashTable.Add(10, 2);
        Console.WriteLine($"hashTable[10] = {hashTable[10]}, expecting 2");
        hashTable[10] += 3;
        Console.WriteLine($"hashTable[10] = {hashTable[10]}, expecting 5");
        hashTable.Add(21, 9);
        Console.WriteLine($"hashTable[21] = {hashTable[21]}, expecting 9");
        hashTable.Add(118, 10);
        Console.WriteLine($"hashTable[118] = {hashTable[118]}, expecting 10");
        hashTable[126] = 20;
        Console.WriteLine($"hashTable[126] = {hashTable[126]}, expecting 20");

        Console.WriteLine();
        Console.WriteLine("Test for StaticHashTable<Student, int>:");

        StaticHashTable<Student, int> studentScores = new();
        studentScores.Add(new("Adam", "Jackson", new(2007, 12, 15), Year.Sixth), 80);
        Console.WriteLine(studentScores[new("Adam", "Jackson", new(2007, 12, 15), Year.Sixth)]);
        studentScores.Add(new("Louis", "Lau", new(2008, 9, 12), Year.Fifth), 75);
        Console.WriteLine(studentScores[new("Louis", "Lau", new(2008, 9, 12), Year.Fifth)]);
        studentScores[new("Peter", "Bhatia", new(2007, 6, 8), Year.LowerEighth)] = 84;
        Console.WriteLine(studentScores[new("Peter", "Bhatia", new(2007, 6, 8), Year.LowerEighth)]);
        studentScores[new("Adam", "Jackson", new(2007, 12, 15), Year.Sixth)] += 5;
        Console.WriteLine(studentScores[new("Adam", "Jackson", new(2007, 12, 15), Year.Sixth)]);

        Console.WriteLine();
        Console.WriteLine("Test for DynamicHashTable<int, int>:");

        DynamicHashTable<int, int> dynamicHashTable = new();
        Console.WriteLine($"dynamicHashTable.Capacity = {dynamicHashTable.Capacity}");
        dynamicHashTable.Add(10, 2);
        Console.WriteLine($"dynamicHashTable[10] = {dynamicHashTable[10]}, expecting 2");
        dynamicHashTable[10] += 3;
        Console.WriteLine($"dynamicHashTable[10] = {dynamicHashTable[10]}, expecting 5");
        dynamicHashTable.Add(21, 9);
        Console.WriteLine($"dynamicHashTable[21] = {dynamicHashTable[21]}, expecting 9");
        dynamicHashTable.Add(118, 10);
        Console.WriteLine($"dynamicHashTable[118] = {dynamicHashTable[118]}, expecting 10");
        dynamicHashTable[67] = 4;
        Console.WriteLine($"dynamicHashTable[67] = {dynamicHashTable[67]}, expecting 4");
        dynamicHashTable[231] = 28;
        Console.WriteLine($"dynamicHashTable[231] = {dynamicHashTable[231]}, expecting 28");

        Console.WriteLine();
        Console.WriteLine("Load Test for DynamicHashTable<int, int>:");

        DynamicHashTable<int, int> capacityTestHashTable = new();
        Random random = new();
        for(int i = 0; i < 500; ++i)
        {
            capacityTestHashTable[random.Next(0, 500)] = random.Next();
            if (i % 20 == 0)
            {
                Console.WriteLine($"Current Load: {capacityTestHashTable.Count}, Current Capacity: {capacityTestHashTable.Capacity}");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Load Test for MyHashTable<int, int>:");

        MyHashTable<int, int> stressTestHashTable = new();
        for(int i = 0; i < 1000; ++i)
        {
            stressTestHashTable[random.Next(0, 500)] = random.Next();
            if (i % 20 == 0)
            {
                Console.WriteLine($"Current Load: {stressTestHashTable.Count}, Current Capacity: {stressTestHashTable.Capacity}");
            }
        }

        for(int i = 0; i < 1000; ++i)
        {
            int randomKey = random.Next(0, 500);
            if (stressTestHashTable.ContainsKey(randomKey))
            {
                stressTestHashTable.Remove(randomKey);
            }

            if (i % 20 == 0)
            {
                Console.WriteLine($"Current Load: {stressTestHashTable.Count}, Current Capacity: {stressTestHashTable.Capacity}");
            }
        }
    }
}