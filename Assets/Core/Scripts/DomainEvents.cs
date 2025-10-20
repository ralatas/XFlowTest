using System;

namespace Core
{
    public static class DomainEvents
    {
        public static event Action StoreChanged;
        public static void RaiseStoreChanged() => StoreChanged?.Invoke();
    }
}
