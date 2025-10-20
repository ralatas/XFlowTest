using UnityEngine;
using Core;

namespace Health
{
    // Attach to scene to set default HP if missing
    public class HealthBootstrapper : MonoBehaviour
    {
        public int defaultHealth = 50;
        void Awake()
        {
            var pd = PlayerData.Instance;
            if (!pd.TryGet(new HealthKey(), out int _))
                pd.Set(new HealthKey(), defaultHealth);
            DomainEvents.RaiseStoreChanged();
        }
    }
}
