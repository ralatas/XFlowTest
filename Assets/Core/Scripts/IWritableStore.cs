namespace Core
{
    public interface IWritableStore : IReadableStore
    {
        void Set<T>(IDataKey<T> key, T value);
    }
}
