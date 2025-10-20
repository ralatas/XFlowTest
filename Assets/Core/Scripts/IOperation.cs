namespace Core
{
    // Base "brick" interface used by domain-specific ScriptableObjects
    public interface IOperation : IOperationCheck, IOperationApply { }
}
