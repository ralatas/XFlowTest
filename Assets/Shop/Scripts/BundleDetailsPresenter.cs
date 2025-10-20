using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class BundleDetailsPresenter : MonoBehaviour
    {
        [SerializeField] Transform root;
        [SerializeField] GameObject cardPrefab;
        [SerializeField] ShopBundleSO[] bundlesCatalog;
        [SerializeField] Button backButton;

        void Start()
        {
            var id = Core.SceneContext.SelectedBundleGuid;
            var bundle = bundlesCatalog.FirstOrDefault(b => b && b.bundleId == id) ?? bundlesCatalog.FirstOrDefault();
            var go = Instantiate(cardPrefab, root);
            var card = go.GetComponent<ShopCardPresenter>();
            card.bundle = bundle;

            if (backButton) backButton.onClick.AddListener(() =>
                UnityEngine.SceneManagement.SceneManager.LoadScene("ShopListScene"));
        }
    }
}
