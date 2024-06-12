using HashTable.Classes;

namespace HashTable.Tests;

[TestClass]
public class TestStaticHashTable
{
    [TestMethod]
    public void TestAddNew()
    {
        StaticHashTable<int, int> hashTable = new();
        hashTable.Add(12, 9);
        Assert.AreEqual(9, hashTable[12]);
    }
    
    [TestMethod]
    public void TestAddCollision()
    {
        StaticHashTable<int, int> hashTable = new();
        hashTable.Add(12, 9);
        hashTable.Add(109, 10);
        Assert.AreEqual(9, hashTable[12]);
        Assert.AreEqual(10, hashTable[109]);
    }
    
    [TestMethod]
    public void TestAddSame()
    {
        StaticHashTable<int, int> hashTable = new();
        hashTable.Add(12, 9);
        Assert.ThrowsException<InvalidOperationException>(() => hashTable.Add(12, 10));
    }

    [TestMethod]
    public void TestRetrieveNew()
    {
        StaticHashTable<int, int> hashTable = new();
        Assert.ThrowsException<InvalidOperationException>(() => hashTable[12]);
    }

    [TestMethod]
    public void TestRetrieveCollision()
    {
        StaticHashTable<int, int> hashTable = new();
        hashTable.Add(12, 9);
        Assert.ThrowsException<InvalidOperationException>(() => hashTable[109]);
    }

    [TestMethod]
    public void TestRetrieveSame()
    {
        StaticHashTable<int, int> hashTable = new();
        hashTable.Add(12, 9);
        Assert.AreEqual(9, hashTable[12]);
    }

    [TestMethod]
    public void TestSetNew()
    {
        StaticHashTable<int, int> hashTable = new();
        hashTable[12] = 9;
        Assert.AreEqual(9, hashTable[12]);
    }

    [TestMethod]
    public void TestSetCollision()
    {
        StaticHashTable<int, int> hashTable = new();
        hashTable[12] = 9;
        hashTable[109] = 10;
        Assert.AreEqual(9, hashTable[12]);
        Assert.AreEqual(10, hashTable[109]);
    }

    [TestMethod]
    public void TestSetSame()
    {
        StaticHashTable<int, int> hashTable = new();
        hashTable[12] = 9;
        hashTable[12] = 10;
        Assert.AreEqual(10, hashTable[12]);
    }

    [TestMethod]
    public void TestCustomType()
    {
        StaticHashTable<Student, int> studentScores = new();
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
        StaticHashTable<int, int> hashTable = new();
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
}