using Core;
using System;

namespace VIP
{
    public static class VipController
    {
        public static void AddSeconds(double seconds)
        {
            var pd = PlayerData.Instance;
            pd.TryGet(new VipTicksKey(), out long t);
            long add = TimeSpan.FromSeconds(seconds).Ticks;
            pd.Set(new VipTicksKey(), t + add);
            DomainEvents.RaiseStoreChanged();
        }
    }
}
