using UnityEngine;
using Core;

namespace VIP
{
    [CreateAssetMenu(menuName = "VIP/Reward Add VIP Time (seconds)")]
    public class RewardAddVipSO : ScriptableObject, IOperation
    {
        [Min(0)] public double seconds = 30;

        public bool CanExecute(IReadableStore s) => true;

        public void Apply(IWritableStore s)
        {
            VipController.AddSeconds(seconds);
        }
    }
}
