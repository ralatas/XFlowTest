using Core;

namespace Location
{
    public static class LocationController
    {
        public static void ResetToDefault(string def = "Town")
        {
            PlayerData.Instance.Set(new LocationKey(), def);
            DomainEvents.RaiseStoreChanged();
        }
    }
}
