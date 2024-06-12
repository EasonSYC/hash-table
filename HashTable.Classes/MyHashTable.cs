using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace HashTable.Classes;

public class MyHashTable<TKey, TValue> : IMyHashTable<TKey, TValue>
// Implementation is in line with IDictionary<TKey, TValue>
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2?view=net-8.0
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerable?view=net-8.0
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1?view=net-8.0
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.icollection-1?view=net-8.0
// I added properties int Capacity and double LoadFactor
{
    #region PublicProperties
    public bool IsReadOnly
    {
        get
        {
            return false;
        }
    }
    public int Count
    {
        get; private set;
    }
    public int Capacity
    {
        get
        {
            return _primes[_currentLengthIndexPtr];
        }
    }
    public double LoadFactor
    {
        get
        {
            return (double)Count / Capacity;
        }
    }
    public TValue this[TKey key]
    {
        get
        {
            if (TryGetValue(key, out TValue? value))
            {
                return value;
            }
            else
            {
                throw new KeyNotFoundException($"Key does not exist: Key {key}.");
            }
        }
        set
        {
            int hashIndex = HashIndex(key);

            if (_table[hashIndex] is null || _table[hashIndex].Count == 0)
            {
                _table[hashIndex] = new([new(key, value)]);
                ++Count;
                CheckResize();
                return;
            }

            for (LinkedListNode<KeyValuePair<TKey, TValue>>? linkedListNode = _table[hashIndex].First; linkedListNode is not null; linkedListNode = linkedListNode.Next)
            {
                if (key!.Equals(linkedListNode.Value.Key))
                {
                    linkedListNode.Value = new(linkedListNode.Value.Key, value);
                    return;
                }
            }

            _table[hashIndex].AddLast(new KeyValuePair<TKey, TValue>(key, value));
            ++Count;
            CheckResize();
        }
    }
    public ICollection<TKey> Keys
    {
        get
        {
            ICollection<TKey> keyList = [];
            foreach (LinkedList<KeyValuePair<TKey, TValue>> keyValuePairs in _table)
            {
                if (keyValuePairs is not null)
                {
                    foreach (KeyValuePair<TKey, TValue> keyValuePair in keyValuePairs)
                    {
                        keyList.Add(keyValuePair.Key);
                    }
                }
            }

            return keyList;
        }
    }
    public ICollection<TValue> Values
    {
        get
        {
            ICollection<TValue> valueList = [];
            foreach (LinkedList<KeyValuePair<TKey, TValue>> keyValuePairs in _table)
            {
                if (keyValuePairs is not null)
                {
                    foreach (KeyValuePair<TKey, TValue> keyValuePair in keyValuePairs)
                    {
                        valueList.Add(keyValuePair.Value);
                    }
                }
            }

            return valueList;
        }
    }
    #endregion

    #region PrivateConsts
    private const double _resizeUp = 0.72;
    private const double _resizeDown = 0.10;
    private readonly int[] _primes = [53, 97, 193, 389, 769, 1543, 3079, 6151, 12289, 24593, 49157, 98317, 196613, 393241, 786433, 1572869, 3145739, 6291469, 12582917, 25165843, 50331653, 100663319, 201326611, 402653189, 805306457, 1610612741];
    // https://planetmath.org/goodhashtableprimes
    #endregion

    #region PrivateFields
    private int _currentLengthIndexPtr;
    private LinkedList<KeyValuePair<TKey, TValue>>[] _table;
    #endregion

    #region PrivateMethods
    private int HashIndex(TKey key)
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key), "Key cannot be null");
        }

        int index = key.GetHashCode() % Capacity;
        return index < 0 ? index + Capacity : index;
    }
    private void AddWithoutCheck(KeyValuePair<TKey, TValue> item)
    {
        int hashIndex = HashIndex(item.Key);

        if (_table[hashIndex] is null || _table[hashIndex].Count == 0)
        {
            _table[hashIndex] = new([item]);
            ++Count;
            return;
        }

        foreach (KeyValuePair<TKey, TValue> keyValuePair in _table[hashIndex])
        {
            if (item.Key!.Equals(keyValuePair.Key))
            {
                throw new ArgumentException($"Key already exists: Key {item.Key}");
            }
        }

        _table[hashIndex].AddLast(item);
        ++Count;
    }
    private void CheckResize()
    {
        if (LoadFactor > _resizeUp && _currentLengthIndexPtr + 1 < _primes.Length)
        {
            Resize(_currentLengthIndexPtr + 1);
        }
        else if (LoadFactor < _resizeDown && _currentLengthIndexPtr > 0)
        {
            Resize(_currentLengthIndexPtr - 1);
        }
    }
    private void Resize(int newLengthIndexPtr)
    {
        KeyValuePair<TKey, TValue>[] currentValuePairs = new KeyValuePair<TKey, TValue>[Count];
        CopyTo(currentValuePairs, 0);

        Count = 0;
        _currentLengthIndexPtr = newLengthIndexPtr;
        _table = new LinkedList<KeyValuePair<TKey, TValue>>[Capacity];

        foreach (KeyValuePair<TKey, TValue> keyValuePair in currentValuePairs)
        {
            AddWithoutCheck(keyValuePair);
        }
    }
    #endregion

    #region Constructors
    public MyHashTable()
    {
        Count = 0;
        _currentLengthIndexPtr = 0;
        _table = new LinkedList<KeyValuePair<TKey, TValue>>[Capacity];
    }
    #endregion

    #region PublicMethods
    public void Clear()
    {
        Count = 0;
        _currentLengthIndexPtr = 0;
        _table = new LinkedList<KeyValuePair<TKey, TValue>>[Capacity];
    }
    public void Add(TKey key, TValue value)
    {
        Add(new(key, value));
    }
    public void Add(KeyValuePair<TKey, TValue> item)
    {
        AddWithoutCheck(item);
        CheckResize();
    }
    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        int hashIndex = HashIndex(item.Key);

        if (_table[hashIndex] is null || _table[hashIndex].Count == 0)
        {
            return false;
        }

        return _table[hashIndex].Contains(item);
    }
    public bool ContainsKey(TKey key)
    {
        int hashIndex = HashIndex(key);

        if (_table[hashIndex] is null || _table[hashIndex].Count == 0)
        {
            return false;
        }

        foreach (KeyValuePair<TKey, TValue> keyValuePair in _table[hashIndex])
        {
            if (key!.Equals(keyValuePair.Key))
            {
                return true;
            }
        }

        return false;
    }
    public bool Remove(TKey key)
    {
        int hashIndex = HashIndex(key);

        if (_table[hashIndex] is null || _table[hashIndex].Count == 0)
        {
            return false;
        }

        for (LinkedListNode<KeyValuePair<TKey, TValue>>? linkedListNode = _table[hashIndex].First; linkedListNode is not null; linkedListNode = linkedListNode.Next)
        {
            if (key!.Equals(linkedListNode.Value.Key))
            {
                --Count;
                _table[hashIndex].Remove(linkedListNode);
                CheckResize();
                return true;
            }
        }

        return false;
    }
    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        int hashIndex = HashIndex(item.Key);

        if (_table[hashIndex] is null || _table[hashIndex].Count == 0)
        {
            return false;
        }

        if (_table[hashIndex].Remove(item))
        {
            --Count;
            CheckResize();
            return true;
        }

        return false;
    }
    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
    {
        int hashIndex = HashIndex(key);

        if (_table[hashIndex] is null || _table[hashIndex].Count == 0)
        {
            value = default;
            return false;
        }

        foreach (KeyValuePair<TKey, TValue> keyValuePair in _table[hashIndex])
        {
            if (key!.Equals(keyValuePair.Key))
            {
                value = keyValuePair.Value;
                return true;
            }
        }

        value = default;
        return false;
    }
    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        if (array is null)
        {
            throw new ArgumentNullException(nameof(array), "Array cannot be null.");
        }

        if (arrayIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Array index must not be negative.");
        }

        if (array.Length - arrayIndex < Count)
        {
            throw new ArgumentException("The number of elements is greater than the available space from arrayIndex to the end of the destination array.");
        }

        foreach (LinkedList<KeyValuePair<TKey, TValue>> keyValuePairs in _table)
        {
            if (keyValuePairs is not null)
            {
                foreach (KeyValuePair<TKey, TValue> keyValuePair in keyValuePairs)
                {
                    array[arrayIndex++] = keyValuePair;
                }
            }
        }
    }
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        KeyValuePair<TKey, TValue>[] currentValuePairs = new KeyValuePair<TKey, TValue>[Count];
        CopyTo(currentValuePairs, 0);

        foreach(KeyValuePair<TKey, TValue> keyValuePair in currentValuePairs)
        {
            yield return keyValuePair;
        }
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    #endregion
}