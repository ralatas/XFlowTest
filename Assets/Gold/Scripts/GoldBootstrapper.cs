using UnityEngine;
using Core;

namespace Gold
{
    public class GoldBootstrapper : MonoBehaviour
    {
        public int defaultGold = 100;
        void Awake()
        {
            var pd = PlayerData.Instance;
            if (!pd.TryGet(new GoldKey(), out int _))
                pd.Set(new GoldKey(), defaultGold);
            DomainEvents.RaiseStoreChanged();
        }
    }
}
