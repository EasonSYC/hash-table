using HashTable.Classes;

namespace HashTable.Tests;

[TestClass]
public class TestStaticHashTableNoCollision
{
    [TestMethod]
    public void TestAddNew()
    {
        StaticHashTableNoCollision<int, int> hashTable = new();
        hashTable.Add(12, 9);
        Assert.AreEqual(9, hashTable[12]);
    }
    
    [TestMethod]
    public void TestAddCollision()
    {
        StaticHashTableNoCollision<int, int> hashTable = new();
        hashTable.Add(12, 9);
        Assert.ThrowsException<InvalidOperationException>(() => hashTable.Add(109, 10));
    }
    
    [TestMethod]
    public void TestAddSame()
    {
        StaticHashTableNoCollision<int, int> hashTable = new();
        hashTable.Add(12, 9);
        Assert.ThrowsException<InvalidOperationException>(() => hashTable.Add(12, 10));
    }

    [TestMethod]
    public void TestRetrieveNew()
    {
        StaticHashTableNoCollision<int, int> hashTable = new();
        Assert.ThrowsException<InvalidOperationException>(() => hashTable[12]);
    }

    [TestMethod]
    public void TestRetrieveCollision()
    {
        StaticHashTableNoCollision<int, int> hashTable = new();
        hashTable.Add(12, 9);
        Assert.ThrowsException<InvalidOperationException>(() => hashTable[109]);
    }

    [TestMethod]
    public void TestRetrieveSame()
    {
        StaticHashTableNoCollision<int, int> hashTable = new();
        hashTable.Add(12, 9);
        Assert.AreEqual(9, hashTable[12]);
    }

    [TestMethod]
    public void TestSetNew()
    {
        StaticHashTableNoCollision<int, int> hashTable = new();
        hashTable[12] = 9;
        Assert.AreEqual(9, hashTable[12]);
    }

    [TestMethod]
    public void TestSetCollision()
    {
        StaticHashTableNoCollision<int, int> hashTable = new();
        hashTable[12] = 9;
        Assert.ThrowsException<InvalidOperationException>(() => hashTable[109] = 9);
    }

    [TestMethod]
    public void TestSetSame()
    {
        StaticHashTableNoCollision<int, int> hashTable = new();
        hashTable[12] = 9;
        hashTable[12] = 10;
        Assert.AreEqual(10, hashTable[12]);
    }
}