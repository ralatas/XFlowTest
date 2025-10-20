using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public static class CompositeRunner
    {
        public static bool ValidateAll(IEnumerable<IOperation> ops, IReadableStore s)
            => ops.All(op => op.CanExecute(s));

        public static bool ExecuteTransaction(
            IEnumerable<IOperation> costs,
            IEnumerable<IOperation> rewards,
            PlayerData pd)
        {
            var snap = pd.CreateSnapshot();
            if (!ValidateAll(costs, pd)) return false;

            foreach (var c in costs) c.Apply(pd);

            if (!ValidateAll(rewards, pd)) { pd.Restore(snap); return false; }

            foreach (var r in rewards) r.Apply(pd);
            return true;
        }
    }
}
