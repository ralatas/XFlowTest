using Core;

namespace Gold
{
    public static class GoldController
    {
        public static void Add(int value)
        {
            var pd = PlayerData.Instance;
            pd.TryGet(new GoldKey(), out int g);
            pd.Set(new GoldKey(), g + value);
            DomainEvents.RaiseStoreChanged();
        }
    }
}
