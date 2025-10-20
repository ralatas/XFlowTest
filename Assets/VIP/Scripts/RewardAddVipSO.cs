using UnityEngine;
using Core;
using System;

namespace VIP
{
    [CreateAssetMenu(menuName = "VIP/Reward Add VIP Time (seconds)")]
    public class RewardAddVipSO : ScriptableObject, IOperation
    {
        [Min(0)] public double seconds = 30;

        public bool CanExecute(IReadableStore s) => true;

        public void Apply(IWritableStore s)
        {
            s.TryGet(new VipTicksKey(), out long currentTicks);
            long addTicks = TimeSpan.FromSeconds(seconds).Ticks;
            s.Set(new VipTicksKey(), currentTicks + addTicks);
        }
    }
}
