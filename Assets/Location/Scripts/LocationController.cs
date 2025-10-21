using System;
using Core;

namespace Location
{
    public static class LocationController
    {
        public static event Action<string> OnLocationChanged;

        public static void ResetToDefault(string def = "Town")
        {
            PlayerData.Instance.Set(new LocationKey(), def);

            OnLocationChanged?.Invoke(def);
            DomainEvents.RaiseStoreChanged();
        }

        public static void Set(string value)
        {
            PlayerData.Instance.Set(new LocationKey(), value);

            OnLocationChanged?.Invoke(value);
            DomainEvents.RaiseStoreChanged();
        }
    }
}
