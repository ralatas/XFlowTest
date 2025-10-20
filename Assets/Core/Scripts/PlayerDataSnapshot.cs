using System;
using System.Collections.Generic;

namespace Core
{
    // Snapshot for transactional apply/rollback
    public sealed class PlayerDataSnapshot
    {
        private readonly Dictionary<Type, object> _copy;

        public PlayerDataSnapshot(Dictionary<Type, object> src)
        {
            _copy = new Dictionary<Type, object>(src);
        }

        public void RestoreInto(Dictionary<Type, object> dst)
        {
            dst.Clear();
            foreach (var kv in _copy)
                dst[kv.Key] = kv.Value;
        }
    }
}
