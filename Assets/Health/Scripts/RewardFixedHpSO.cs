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
            HealthController.Add(amount);
        }
    }
}
