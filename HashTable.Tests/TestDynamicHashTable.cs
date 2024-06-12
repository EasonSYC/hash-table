using HashTable.Classes;

namespace HashTable.Tests;

[TestClass]
public class TestDynamicHashTable
{
    [TestMethod]
    public void TestAddNew()
    {
        DynamicHashTable<int, int> hashTable = new();
        hashTable.Add(12, 9);
        Assert.AreEqual(9, hashTable[12]);
    }
    
    [TestMethod]
    public void TestAddCollision()
    {
        DynamicHashTable<int, int> hashTable = new();
        hashTable.Add(12, 9);
        hashTable.Add(65, 10);
        Assert.AreEqual(9, hashTable[12]);
        Assert.AreEqual(10, hashTable[65]);
    }
    
    [TestMethod]
    public void TestAddSame()
    {
        DynamicHashTable<int, int> hashTable = new();
        hashTable.Add(12, 9);
        Assert.ThrowsException<InvalidOperationException>(() => hashTable.Add(12, 10));
    }

    [TestMethod]
    public void TestRetrieveNew()
    {
        DynamicHashTable<int, int> hashTable = new();
        Assert.ThrowsException<InvalidOperationException>(() => hashTable[12]);
    }

    [TestMethod]
    public void TestRetrieveCollision()
    {
        DynamicHashTable<int, int> hashTable = new();
        hashTable.Add(12, 9);
        Assert.ThrowsException<InvalidOperationException>(() => hashTable[65]);
    }

    [TestMethod]
    public void TestRetrieveSame()
    {
        DynamicHashTable<int, int> hashTable = new();
        hashTable.Add(12, 9);
        Assert.AreEqual(9, hashTable[12]);
    }

    [TestMethod]
    public void TestSetNew()
    {
        DynamicHashTable<int, int> hashTable = new();
        hashTable[12] = 9;
        Assert.AreEqual(9, hashTable[12]);
    }

    [TestMethod]
    public void TestSetCollision()
    {
        DynamicHashTable<int, int> hashTable = new();
        hashTable[12] = 9;
        hashTable[65] = 10;
        Assert.AreEqual(9, hashTable[12]);
        Assert.AreEqual(10, hashTable[65]);
    }

    [TestMethod]
    public void TestSetSame()
    {
        DynamicHashTable<int, int> hashTable = new();
        hashTable[12] = 9;
        hashTable[12] = 10;
        Assert.AreEqual(10, hashTable[12]);
    }

    [TestMethod]
    public void TestCustomType()
    {
        DynamicHashTable<Student, int> studentScores = new();
        studentScores.Add(new("Adam", "Jackson", new(2007, 12, 15), Year.Sixth), 80);
        Assert.AreEqual(80, studentScores[new("Adam", "Jackson", new(2007, 12, 15), Year.Sixth)]);
        studentScores.Add(new("Louis", "Lau", new(2008, 9, 12), Year.Fifth), 75);
        Assert.AreEqual(75, studentScores[new("Louis", "Lau", new(2008, 9, 12), Year.Fifth)]);
        studentScores[new("Peter", "Bhatia", new(2007, 6, 8), Year.LowerEighth)] = 84;
        Assert.AreEqual(84, studentScores[new("Peter", "Bhatia", new(2007, 6, 8), Year.LowerEighth)]);
        studentScores[new("Adam", "Jackson", new(2007, 12, 15), Year.Sixth)] += 5;
        Assert.AreEqual(85, studentScores[new("Adam", "Jackson", new(2007, 12, 15), Year.Sixth)]);
    }

    [TestMethod]
    public void TestGeneralBehaviour()
    {
        DynamicHashTable<int, int> hashTable = new();
        hashTable.Add(10, 2);
        Assert.AreEqual(2, hashTable[10]);
        hashTable[10] += 3;
        Assert.AreEqual(5, hashTable[10]);
        hashTable.Add(21, 9);
        Assert.AreEqual(9, hashTable[21]);
        hashTable.Add(118, 10);
        Assert.AreEqual(10, hashTable[118]);
        hashTable[126] = 20;
        Assert.AreEqual(20, hashTable[126]);
    }

    [TestMethod]
    public void TestLoadResize()
    {
        DynamicHashTable<int, int> capacityTestHashTable = new();
        Random random = new();
        for(int i = 0; i < 1000; ++i)
        {
            capacityTestHashTable[random.Next()] = random.Next();
            Assert.IsTrue((double) capacityTestHashTable.Count / capacityTestHashTable.Capacity <= 0.72);
        }
    }
}