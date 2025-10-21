using System;
using Core;

namespace Gold
{
    public static class GoldController
    {
        public static event Action<int> OnGoldChanged;

        public static void Add(int value)
        {
            var pd = PlayerData.Instance;
            pd.TryGet(new GoldKey(), out int gold);
            gold += value;
            pd.Set(new GoldKey(), gold);

            OnGoldChanged?.Invoke(gold);
            DomainEvents.RaiseStoreChanged();
        }
    }
}
