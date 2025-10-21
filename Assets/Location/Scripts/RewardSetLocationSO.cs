using UnityEngine;
using Core;

namespace Location
{
    [CreateAssetMenu(menuName = "Location/Reward Set Location")]
    public class RewardSetLocationSO : ScriptableObject, IOperation
    {
        public string value;
        public bool CanExecute(IReadableStore s) => true;
        public void Apply(IWritableStore s)
        {
            LocationController.Set(value);
        }
    }
}
