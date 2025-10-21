using UnityEngine;
using Core;

namespace Health
{
    [CreateAssetMenu(menuName = "Health/Cost Percent HP")]
    public class CostPercentHpSO : ScriptableObject, IOperation
    {
        [Range(0,100)] public int percent;

        public bool CanExecute(IReadableStore s)
        {
            if (!s.TryGet(new HealthKey(), out int hp)) return false;
            int cost = Mathf.RoundToInt(hp * (percent / 100f));
            return hp >= cost;
        }

        public void Apply(IWritableStore s)
        {
            s.TryGet(new HealthKey(), out int hp);
            int cost = Mathf.RoundToInt(hp * (percent / 100f));
            HealthController.Add(-cost);
        }
    }
}
