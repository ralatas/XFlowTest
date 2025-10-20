using UnityEngine;
using Core;

namespace Health
{
    [CreateAssetMenu(menuName = "Health/Reward Fixed HP")]
    public class RewardFixedHpSO : ScriptableObject, IOperation
    {
        [Min(0)] public int amount;

        public bool CanExecute(IReadableStore s)
            => s.TryGet(new HealthKey(), out int _);

        public void Apply(IWritableStore s)
        {
            s.TryGet(new HealthKey(), out int hp);
            s.Set(new HealthKey(), System.Math.Max(0, hp + amount));
        }
    }
}
