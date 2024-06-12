namespace HashTable.Classes;

public interface IMyHashTable<TKey, TValue> : IDictionary<TKey, TValue>
{
    public double LoadFactor { get; }
    public int Capacity { get; }
}
