using UnityEngine;
using Core;

namespace Location
{
    // Attach to scene to set default Location if missing
    public class LocationBootstrapper : MonoBehaviour
    {
        public string defaultLocation = "Town";
        void Awake()
        {
            var pd = PlayerData.Instance;
            if (!pd.TryGet(new LocationKey(), out string _))
                pd.Set(new LocationKey(), defaultLocation);
            DomainEvents.RaiseStoreChanged();
        }
    }
}
