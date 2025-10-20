namespace Core
{
    public interface IOperationCheck
    {
        bool CanExecute(IReadableStore store);
    }
}
