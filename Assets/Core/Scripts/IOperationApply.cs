namespace Core
{
    public interface IOperationApply
    {
        void Apply(IWritableStore store);
    }
}
