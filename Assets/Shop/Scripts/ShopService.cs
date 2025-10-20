using System.Collections;
using UnityEngine;
using Core;

namespace Shop
{
    public class ShopService
    {
        private readonly PlayerData _pd;
        public ShopService(PlayerData pd) { _pd = pd; }

        public bool CanBuy(ShopBundleSO b) => CompositeRunner.ValidateAll(b.Costs, _pd);

        public IEnumerator BuyRoutine(ShopBundleSO b, System.Action<bool> onDone)
        {
            if (!CanBuy(b)) { onDone?.Invoke(false); yield break; }

            float t = 0f;
            while (t < 3f) { t += Time.deltaTime; yield return null; }

            bool ok = CompositeRunner.ExecuteTransaction(b.Costs, b.Rewards, _pd);
            if (ok) DomainEvents.RaiseStoreChanged();
            onDone?.Invoke(ok);
        }
    }
}
