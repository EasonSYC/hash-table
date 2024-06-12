namespace HashTable.Classes;

public class DynamicHashTable<TKey, TValue> : IHashtable<TKey, TValue>
{

    #region PrivateClasses
    private class ValuePair
    {
        internal TKey Key { get; set; }
        internal TValue Value { get; set; }

        internal ValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
    #endregion

    #region PublicProperties
    public TValue this[TKey key]
    {
        get
        {
            int hashIndex = HashIndex(key);

            if (_table[hashIndex] is null)
            {
                throw new InvalidOperationException($"Key does not exist: Key {key}.");
            }

            foreach (ValuePair item in _table[hashIndex])
            {
                if (key!.Equals(item.Key))
                {
                    return item.Value;
                }
            }

            throw new InvalidOperationException($"Key does not exist: Key {key}.");
        }
        set
        {
            int hashIndex = HashIndex(key);

            if (_table[hashIndex] is null)
            {
                _table[hashIndex] = new([new(key, value)]);
                ++_occupiedNumber;
                CheckRehash();
                return;
            }

            foreach (ValuePair item in _table[hashIndex])
            {
                if (key!.Equals(item.Key))
                {
                    item.Value = value;
                    return;
                }
            }

            _table[hashIndex].AddLast(new ValuePair(key, value));
            ++_occupiedNumber;
            CheckRehash();
        }
    }
    
    public int Count
    {
        get
        {
            return _occupiedNumber;
        }
    }
    public int Capacity
    {
        get
        {
            return _currentArrayLength;
        }
    }
    #endregion

    #region PrivateProperties
    private double LoadFactor
    {
        get
        {
            return (double) _occupiedNumber / _currentArrayLength;
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
    private int _currentArrayLength;
    private int _currentLengthIndexPtr;
    private int _occupiedNumber;
    private LinkedList<ValuePair>[] _table;
    #endregion

    #region PrivateMethods
    private int HashIndex(TKey key)
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key), "Key cannot be null");
        }

        int index = key.GetHashCode() % _currentArrayLength;
        return index < 0 ? index + _currentArrayLength : index;
    }

    private void AddWithoutCheck(TKey key, TValue value)
    {
        int hashIndex = HashIndex(key);

        if (_table[hashIndex] is null)
        {
            _table[hashIndex] = new([new(key, value)]);
            ++_occupiedNumber;
            return;
        }

        foreach (ValuePair item in _table[hashIndex])
        {
            if (key!.Equals(item.Key))
            {
                throw new InvalidOperationException($"Key already exists: Key {key}");
            }
        }

        _table[hashIndex].AddLast(new ValuePair(key, value));
        ++_occupiedNumber;
    }
    private void CheckRehash()
    {
        if (LoadFactor > _resizeUp && _currentLengthIndexPtr + 1 < _primes.Length)
        {
            Rehash(_currentLengthIndexPtr + 1);
        }
        else if (LoadFactor < _resizeDown && _currentLengthIndexPtr > 0)
        {
            Rehash(_currentLengthIndexPtr - 1);
        }
    }

    private void Rehash(int newLengthIndexPtr)
    {
        List<ValuePair> currentValuePairs = [];
        foreach (LinkedList<ValuePair> valuePairs in _table)
        {
            if (valuePairs is not null)
            {
                foreach (ValuePair valuePair in valuePairs)
                {
                    currentValuePairs.Add(valuePair);
                }
            }
        }

        _occupiedNumber = 0;
        _currentLengthIndexPtr = newLengthIndexPtr;
        _currentArrayLength = _primes[_currentLengthIndexPtr];
        _table = new LinkedList<ValuePair>[_currentArrayLength];

        foreach (ValuePair valuePair in currentValuePairs)
        {
            AddWithoutCheck(valuePair.Key, valuePair.Value);
        }
    }
    #endregion

    #region Constructors
    public DynamicHashTable()
    {
        _currentLengthIndexPtr = 0;
        _occupiedNumber = 0;
        _currentArrayLength = _primes[_currentLengthIndexPtr];
        _table = new LinkedList<ValuePair>[_currentArrayLength];
    }
    #endregion

    #region PublicMethods
    public void Add(TKey key, TValue value)
    {
        AddWithoutCheck(key, value);
        CheckRehash();
    }
    #endregion
}
