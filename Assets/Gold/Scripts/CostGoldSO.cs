using UnityEngine;
using Core;

namespace Gold
{
    [CreateAssetMenu(menuName = "Gold/Cost Fixed Gold")]
    public class CostGoldSO : ScriptableObject, IOperation
    {
        [Min(0)] public int amount;

        public bool CanExecute(IReadableStore s) => s.TryGet(new GoldKey(), out int g) && g >= amount;

        public void Apply(IWritableStore s)
        {
            GoldController.Add(-amount);
        }
    }
}
