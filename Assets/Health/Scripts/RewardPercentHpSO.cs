using UnityEngine;
using Core;

namespace Health
{
    [CreateAssetMenu(menuName = "Health/Reward Percent HP")]
    public class RewardPercentHpSO : ScriptableObject, IOperation
    {
        [Range(-100, 100)] public int percent;

        public bool CanExecute(IReadableStore s)
            => s.TryGet(new HealthKey(), out int _);

        public void Apply(IWritableStore s)
        {
            s.TryGet(new HealthKey(), out int hp);
            var delta = (int)System.Math.Round(hp * (percent / 100.0));
            HealthController.Add(delta);
        }
    }
}
