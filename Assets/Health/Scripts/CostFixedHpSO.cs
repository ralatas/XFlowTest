using UnityEngine;
using Core;

namespace Health
{
    [CreateAssetMenu(menuName = "Health/Cost Fixed HP")]
    public class CostFixedHpSO : ScriptableObject, IOperation
    {
        [Min(0)] public int amount;

        public bool CanExecute(IReadableStore s) => s.TryGet(new HealthKey(), out int hp) && hp >= amount;

        public void Apply(IWritableStore s)
        {
            HealthController.Add(-amount);
        }
    }
}
