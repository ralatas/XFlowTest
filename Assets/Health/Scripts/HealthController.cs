using Core;

namespace Health
{
    public static class HealthController
    {
        public static void Add(int value)
        {
            var pd = PlayerData.Instance;
            pd.TryGet(new HealthKey(), out int hp);
            pd.Set(new HealthKey(), System.Math.Max(0, hp + value));
            DomainEvents.RaiseStoreChanged();
        }
    }
}
