using System;
using System.Collections.Generic;

namespace Core
{
    // Type-safe heterogeneous store without domain-specific fields
    public sealed class PlayerData : IWritableStore
    {
        private static PlayerData _instance;
        public static PlayerData Instance => _instance ??= new PlayerData();

        // Keyed by the concrete key type (e.g., HealthKey, GoldKey)
        private readonly Dictionary<Type, object> _map = new Dictionary<Type, object>();

        public bool TryGet<T>(IDataKey<T> key, out T value)
        {
            if (_map.TryGetValue(key.GetType(), out var obj))
            {
                value = (T)obj; return true;
            }
            value = default; return false;
        }

        public void Set<T>(IDataKey<T> key, T value)
        {
            _map[key.GetType()] = value;
        }

        public PlayerDataSnapshot CreateSnapshot() => new PlayerDataSnapshot(_map);
        public void Restore(PlayerDataSnapshot snap) => snap.RestoreInto(_map);
    }
}
