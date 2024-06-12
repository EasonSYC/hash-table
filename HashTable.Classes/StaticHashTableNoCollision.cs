namespace HashTable.Classes;

public class StaticHashTableNoCollision<TKey, TValue> : IHashtable<TKey, TValue>
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

            if (_table[hashIndex] is null || !key!.Equals(_table[hashIndex].Key))
            {
                throw new InvalidOperationException($"Key does not exist: Key {key}.");
            }

            return _table[hashIndex].Value;
        }
        set
        {
            int hashIndex = HashIndex(key);

            if (_table[hashIndex] is not null && !key!.Equals(_table[hashIndex].Key))
            {
                throw new InvalidOperationException($"Hash collision: Key {key}, with Key {_table[hashIndex].Key}");
            }

            _table[hashIndex] = new(key, value);
        }
    }
    #endregion

    #region PrivateFields
    private const int _defaultArrayLength = 97;
    private readonly int _arrayLength;
    private readonly ValuePair[] _table;
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
    public StaticHashTableNoCollision()
    {
        _arrayLength = _defaultArrayLength;
        _table = new ValuePair[_arrayLength];
    }
    #endregion

    #region PublicMethods
    public void Add(TKey key, TValue value)
    {
        int hashIndex = HashIndex(key);

        if (_table[hashIndex] is not null)
        {
            throw new InvalidOperationException($"Hash collision or Key Already Exists: Key {key}, with Key {_table[hashIndex].Key}");
        }

        _table[hashIndex] = new(key, value);
    }
    #endregion
}
