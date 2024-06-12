namespace HashTable.Classes;
public interface IHashtable<TKey, TValue>
{
    public void Add(TKey key, TValue value);
    public TValue this[TKey key] { get; set; }
}