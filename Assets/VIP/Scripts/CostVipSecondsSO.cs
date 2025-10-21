using UnityEngine;
using Core;

namespace VIP
{
    [CreateAssetMenu(menuName = "VIP/Cost VIP Time (seconds)")]
    public class CostVipSecondsSO : ScriptableObject, IOperation
    {
        [Min(0)] public double seconds = 30;

        public bool CanExecute(IReadableStore s)
        {
            s.TryGet(new VipTicksKey(), out long t);
            long need = System.TimeSpan.FromSeconds(seconds).Ticks;
            return t >= need;
        }

        public void Apply(IWritableStore s)
        {
            VipController.AddSeconds(-seconds);
        }
    }
}
