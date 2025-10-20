using UnityEngine;
using Core;

namespace Gold
{
    [CreateAssetMenu(menuName = "Gold/Reward Fixed Gold")]
    public class RewardGoldSO : ScriptableObject, IOperation
    {
        [Min(0)] public int amount;

        public bool CanExecute(IReadableStore s) => true;

        public void Apply(IWritableStore s)
        {
            s.TryGet(new GoldKey(), out int g);
            s.Set(new GoldKey(), g + amount);
        }
    }
}
