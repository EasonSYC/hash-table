# Hash Table

My implementation of hash tables.

`IHashTable` defines basic behaviour of a hash table.

`StaticHashTableNoCollision`, `StaticHashTable`, `DynamicHashTable` implements `IHashTable` and they have different behaviours as their name suggests.

`Student` is a custom class which overrides `Equals` and `GetHashCode` and implements `IEquatable` for tests for custom types.

`IMyHashTable` defines behaviour of a hash table I would expect. This includes `IDictionary`, and two additional properties, `int Capacity` and `double LoadFactor`.

`MyHashTable` is an implementation of `IMyHashTable` and therefore `IDictionary`.
