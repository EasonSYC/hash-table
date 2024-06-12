namespace HashTable.Classes;

public class StaticHashTable<TKey, TValue> : IHashtable<TKey, TValue>
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
        }
    }
    #endregion

    #region PrivateFields
    private const int _defaultArrayLength = 97;
    private readonly int _arrayLength;
    private readonly LinkedList<ValuePair>[] _table;
    #endregion

    #region PrivateMethods
    private int HashIndex(TKey key)
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key), "Key cannot be null");
        }

        int index = key.GetHashCode() % _arrayLength;
        return index < 0 ? index + _arrayLength : index;
    }
    #endregion

    #region Constructors
    public StaticHashTable()
    {
        _arrayLength = _defaultArrayLength;
        _table = new LinkedList<ValuePair>[_arrayLength];
    }
    #endregion

    #region PublicMethods
    public void Add(TKey key, TValue value)
    {
        int hashIndex = HashIndex(key);

        if (_table[hashIndex] is null)
        {
            _table[hashIndex] = new([new(key, value)]);
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
    }
    #endregion
}
