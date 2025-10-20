using UnityEngine;
using Core;

namespace VIP
{
    public class VipBootstrapper : MonoBehaviour
    {
        public double defaultVipSeconds = 0;
        void Awake()
        {
            var pd = PlayerData.Instance;
            if (!pd.TryGet(new VipTicksKey(), out long _))
                pd.Set(new VipTicksKey(), System.TimeSpan.FromSeconds(defaultVipSeconds).Ticks);
            DomainEvents.RaiseStoreChanged();
        }
    }
}
