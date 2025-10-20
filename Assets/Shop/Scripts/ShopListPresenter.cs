using System.Linq;
using UnityEngine;

namespace Shop
{
    public class ShopListPresenter : MonoBehaviour
    {
        [SerializeField] Transform content;
        [SerializeField] GameObject cardPrefab;
        [SerializeField] ShopBundleSO[] bundles;

        void Start()
        {
            foreach (var b in bundles.Where(x => x))
            {
                var go = Instantiate(cardPrefab, content);
                var card = go.GetComponent<ShopCardPresenter>();
                card.bundle = b;
            }
        }
    }
}
