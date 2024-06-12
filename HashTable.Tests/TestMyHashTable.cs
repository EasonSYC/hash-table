using HashTable.Classes;

namespace HashTable.Tests;

[TestClass]
public class TestMyHashTable
{
    [TestMethod]
    public void TestGeneralBehaviour()
    {
        MyHashTable<int, int> hashTable = [];
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
    public void TestLoadSize()
    {
        MyHashTable<int, int> capacityTestHashTable = [];
        Random random = new();
        for (int i = 0; i < 1000; ++i)
        {
            capacityTestHashTable[random.Next()] = random.Next();
            Assert.AreEqual(capacityTestHashTable.LoadFactor, (double)capacityTestHashTable.Count / capacityTestHashTable.Capacity);
            Assert.IsTrue((double)capacityTestHashTable.Count / capacityTestHashTable.Capacity <= 0.72);
        }

        for (int i = 0; i < 1000; ++i)
        {
            int randomKey = random.Next();
            if (capacityTestHashTable.ContainsKey(randomKey))
            {
                capacityTestHashTable.Remove(randomKey);
            }
            Assert.AreEqual(capacityTestHashTable.LoadFactor, (double)capacityTestHashTable.Count / capacityTestHashTable.Capacity);
            Assert.IsTrue((double)capacityTestHashTable.Count / capacityTestHashTable.Capacity >= 0.10);
        }
    }

    [TestMethod]
    public void TestAddKeyValue()
    {
        MyHashTable<int, string> myHashTable = [];
        myHashTable.Add(1, "one");

        Assert.AreEqual("one", myHashTable[1]);
    }

    [TestMethod]
    public void TestAddKeyValuePair()
    {
        MyHashTable<int, string> myHashTable = [];
        myHashTable.Add(new(1, "one"));

        Assert.AreEqual("one", myHashTable[1]);
    }

    [TestMethod]
    public void TestAddDuplicateKeyValue()
    {
        MyHashTable<int, string> myHashTable = [];
        myHashTable.Add(1, "one");

        Assert.ThrowsException<ArgumentException>(() => myHashTable.Add(1, "another one"));
    }

    [TestMethod]
    public void TestAddDuplicateKeyValuePair()
    {
        MyHashTable<int, string> myHashTable = [];
        myHashTable.Add(1, "one");

        Assert.ThrowsException<ArgumentException>(() => myHashTable.Add(new(1, "another one")));
    }

    [TestMethod]
    public void TestRemoveKeyValue()
    {
        MyHashTable<int, string> myHashTable = [];
        myHashTable.Add(1, "one");

        Assert.IsTrue(myHashTable.Remove(1));
        Assert.IsFalse(myHashTable.ContainsKey(1));
        Assert.IsFalse(myHashTable.Remove(1));
    }

    [TestMethod]
    public void TestRemoveNonexsistantKeyValue()
    {
        MyHashTable<int, string> myHashTable = [];

        Assert.IsFalse(myHashTable.Remove(1));
    }

    [TestMethod]
    public void TestRemoveKeyValuePair()
    {
        MyHashTable<int, string> myHashTable = [];
        myHashTable.Add(1, "one");

        Assert.IsTrue(myHashTable.Remove(new KeyValuePair<int, string>(1, "one")));
        Assert.IsFalse(myHashTable.ContainsKey(1));
        Assert.IsFalse(myHashTable.Remove(1));
    }

    [TestMethod]
    public void TestRemoveNonexsistantKeyValuePAir()
    {
        MyHashTable<int, string> myHashTable = [];

        Assert.IsFalse(myHashTable.Remove(new KeyValuePair<int, string>(1, "one")));
    }

    [TestMethod]
    public void TestContainsKey()
    {
        MyHashTable<int, string> myHashTable = [];
        myHashTable.Add(1, "one");

        Assert.IsTrue(myHashTable.ContainsKey(1));
    }

    [TestMethod]
    public void TestContainsKeyNonexsistant()
    {
        MyHashTable<int, string> myHashTable = [];
        Assert.IsFalse(myHashTable.ContainsKey(1));
    }

    [TestMethod]
    public void TestContains()
    {
        MyHashTable<int, string> myHashTable = [];
        myHashTable.Add(1, "one");

        Assert.IsTrue(myHashTable.Contains(new(1, "one")));
    }

    [TestMethod]
    public void TestContainsNonexsistant()
    {
        MyHashTable<int, string> myHashTable = [];
        myHashTable.Add(1, "one");

        Assert.IsFalse(myHashTable.Contains(new(1, "another one")));
    }

    [TestMethod]
    public void TestCount()
    {
        MyHashTable<int, string> myHashTable = [];
        myHashTable.Add(1, "one");
        myHashTable.Add(2, "two");
        Assert.AreEqual(2, myHashTable.Count);
        myHashTable.Remove(2);
        Assert.AreEqual(1, myHashTable.Count);
        myHashTable.Remove(new KeyValuePair<int, string>(1, "another one"));
        Assert.AreEqual(1, myHashTable.Count);
        myHashTable.Remove(new KeyValuePair<int, string>(1, "one"));
        Assert.AreEqual(0, myHashTable.Count);
    }

    [TestMethod]
    public void TestKeys()
    {
        MyHashTable<int, string> myHashTable = [];
        myHashTable.Add(1, "one");
        myHashTable.Add(2, "two");

        int[] keys = [.. myHashTable.Keys];
        int[] expected = [1, 2];

        CollectionAssert.AreEquivalent(expected, keys);
    }

    [TestMethod]
    public void TestValues()
    {
        MyHashTable<int, string> myHashTable = [];
        myHashTable.Add(1, "one");
        myHashTable.Add(2, "two");

        string[] values = [.. myHashTable.Values];
        string[] expected = ["one", "two"];

        CollectionAssert.AreEquivalent(expected, values);
    }

    [TestMethod]
    public void TestClear()
    {
        MyHashTable<int, string> myHashTable = [];
        myHashTable.Add(1, "one");
        myHashTable.Add(2, "two");
        myHashTable.Clear();

        Assert.AreEqual(0, myHashTable.Count);
        Assert.IsFalse(myHashTable.ContainsKey(1));
        Assert.IsFalse(myHashTable.ContainsKey(2));
    }

    [TestMethod]
    public void TestReadOnly()
    {
        MyHashTable<int, string> myHashTable = [];
        Assert.IsFalse(myHashTable.IsReadOnly);
    }

    [TestMethod]
    public void TestTryGetValue()
    {
        MyHashTable<int, string> myHashTable = [];
        myHashTable.Add(1, "one");

        Assert.IsTrue(myHashTable.TryGetValue(1, out string? value));
        Assert.AreEqual("one", value);
    }

    [TestMethod]
    public void TestTryGetValueNonExsistant()
    {
        MyHashTable<int, string> myHashTable = [];

        Assert.IsFalse(myHashTable.TryGetValue(1, out string? value));
        Assert.IsNull(value);
    }

    [TestMethod]
    public void TestIndexerGet()
    {
        MyHashTable<int, string> myHashTable = [];
        myHashTable.Add(1, "one");
        Assert.AreEqual("one", myHashTable[1]);
    }

    [TestMethod]
    public void TestIndexerGetNonExsistant()
    {
        MyHashTable<int, string> myHashTable = [];
        Assert.ThrowsException<KeyNotFoundException>(() => myHashTable[1]);
    }

    [TestMethod]
    public void TestIndexerSet()
    {
        MyHashTable<int, string> myHashTable = [];
        myHashTable[1] = "one";
        Assert.AreEqual("one", myHashTable[1]);
    }

    [TestMethod]
    public void TestIndexerSetUpdate()
    {
        MyHashTable<int, string> myHashTable = [];
        myHashTable.Add(1, "one");
        myHashTable[1] = "updated one";
        Assert.AreEqual("updated one", myHashTable[1]);
    }

    [TestMethod]
    public void TestCopyTo()
    {
        MyHashTable<int, string> myHashTable = [];
        myHashTable.Add(1, "one");
        myHashTable.Add(2, "two");

        KeyValuePair<int, string>[] actual = new KeyValuePair<int, string>[2];
        myHashTable.CopyTo(actual, 0);

        KeyValuePair<int, string>[] expected = [
            new(1, "one"), new(2, "two")
        ];

        CollectionAssert.AreEquivalent(expected, actual);
    }

    [TestMethod]
    public void TestGetEnumerator()
    {
        MyHashTable<int, string> myHashTable = [];
        myHashTable.Add(1, "one");
        myHashTable.Add(2, "two");

        var enumerator = myHashTable.GetEnumerator();
        List<KeyValuePair<int, string>> actual = [];

        List<KeyValuePair<int, string>> expected = [new(1, "one"), new(2, "two")];

        while (enumerator.MoveNext())
        {
            actual.Add(enumerator.Current);
        }

        CollectionAssert.AreEquivalent(expected, actual);
    }

    [TestMethod]
    public void TestIEnumerableGetEnumerator()
    {
        MyHashTable<int, string> myHashTable = [];
        myHashTable.Add(1, "one");
        myHashTable.Add(2, "two");

        var enumerator = ((System.Collections.IEnumerable)myHashTable).GetEnumerator();
        List<KeyValuePair<int, string>> actual = [];

        List<KeyValuePair<int, string>> expected = [new(1, "one"), new(2, "two")];

        while (enumerator.MoveNext())
        {
            actual.Add((KeyValuePair<int, string>)enumerator.Current);
        }

        CollectionAssert.AreEquivalent(expected, actual);
    }

    [TestMethod]
    public void TestICollectionBehaviour()
    {
        MyHashTable<int, string> myHashTable = [new(1, "one"), new(2, "two")];

        List<KeyValuePair<int, string>> expected = [new(1, "one"), new(2, "two")];

        List<KeyValuePair<int, string>> actual = [];
        foreach (KeyValuePair<int, string> keyValuePair in myHashTable)
        {
            actual.Add(keyValuePair);
        }

        CollectionAssert.AreEquivalent(expected, actual);
    }
}
