namespace Core
{
    public interface IReadableStore
    {
        bool TryGet<T>(IDataKey<T> key, out T value);
    }
}
