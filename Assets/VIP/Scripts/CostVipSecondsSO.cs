using UnityEngine;
using Core;
using System;

namespace VIP
{
    [CreateAssetMenu(menuName = "VIP/Cost VIP Time (seconds)")]
    public class CostVipSecondsSO : ScriptableObject, IOperation
    {
        [Min(0)] public double seconds = 30;

        public bool CanExecute(IReadableStore s)
        {
            long currentTicks = 0;
            s.TryGet(new VipTicksKey(), out currentTicks); // if missing => 0
            long needTicks = TimeSpan.FromSeconds(seconds).Ticks;
            return currentTicks >= needTicks;
        }

        public void Apply(IWritableStore s)
        {
            s.TryGet(new VipTicksKey(), out long currentTicks);
            long needTicks = TimeSpan.FromSeconds(seconds).Ticks;
            long next = currentTicks - needTicks;
            if (next < 0) next = 0; // safeguard
            s.Set(new VipTicksKey(), next);
        }
    }
}
