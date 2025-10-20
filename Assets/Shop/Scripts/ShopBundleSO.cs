using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Core;

namespace Shop
{
    [CreateAssetMenu(menuName = "Shop/Bundle")]
    public class ShopBundleSO : ScriptableObject
    {
        public string bundleId;
        public string displayName;
        public List<ScriptableObject> costOps;
        public List<ScriptableObject> rewardOps;

        public IEnumerable<IOperation> Costs  => costOps?.OfType<IOperation>() ?? Enumerable.Empty<IOperation>();
        public IEnumerable<IOperation> Rewards=> rewardOps?.OfType<IOperation>() ?? Enumerable.Empty<IOperation>();
    }
}
