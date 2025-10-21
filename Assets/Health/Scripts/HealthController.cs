using System;
using Core;

namespace Health
{
    public static class HealthController
    {
        public static event Action<int> OnHealthChanged;

        public static void Add(int value)
        {
            var pd = PlayerData.Instance;
            pd.TryGet(new HealthKey(), out int hp);
            hp = Math.Max(0, hp + value);
            pd.Set(new HealthKey(), hp);

            OnHealthChanged?.Invoke(hp);
            DomainEvents.RaiseStoreChanged();
        }
    }
}
