using System;
using Core;

namespace Shop
{
    public class ShopService
    {
        private readonly PlayerData _pd;
        public ShopService(PlayerData pd) { _pd = pd; }

        public bool CanBuy(ShopBundleSO b) => CompositeRunner.ValidateAll(b.Costs, _pd);

        public void StartPurchase(ShopBundleSO b, Action<bool> onDone)
        {
            if (!CanBuy(b)) { onDone?.Invoke(false); return; }
            ShopPurchaseManager.Instance.StartPurchase(b, 3f, onDone);
        }
    }
}
