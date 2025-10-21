using System;
using Core;

namespace VIP
{
    public static class VipController
    {
        public static event Action<long> OnVipChanged; // ticks

        public static void AddSeconds(double seconds)
        {
            var pd = PlayerData.Instance;
            pd.TryGet(new VipTicksKey(), out long t);
            long add = TimeSpan.FromSeconds(seconds).Ticks;
            long next = t + add;
            if (next < 0) next = 0;
            pd.Set(new VipTicksKey(), next);

            OnVipChanged?.Invoke(next);
            DomainEvents.RaiseStoreChanged();
        }
    }
}
