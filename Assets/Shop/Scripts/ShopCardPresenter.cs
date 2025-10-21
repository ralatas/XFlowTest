using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Core;

namespace Shop
{
    public class ShopCardPresenter : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] Button buyButton;
        [SerializeField] TextMeshProUGUI buyLabel;
        [SerializeField] Button infoButton;

        public ShopBundleSO bundle;
        ShopService _service;
        bool _processingCached;

        void Awake()
        {
            _service = new ShopService(PlayerData.Instance);
            buyButton.onClick.AddListener(OnBuy);
            if (infoButton) infoButton.onClick.AddListener(OnInfo);
            DomainEvents.StoreChanged += Refresh;
        }

        void OnDestroy() { DomainEvents.StoreChanged -= Refresh; }

        void Start()
        {
            if (title) title.text = bundle ? bundle.displayName : "-";
            _processingCached = bundle && ShopPurchaseManager.Instance.IsProcessing(bundle.bundleId);
            Refresh();
        }

        void Update()
        {
            if (bundle == null) return;
            bool now = ShopPurchaseManager.Instance.IsProcessing(bundle.bundleId);
            if (now != _processingCached)
            {
                _processingCached = now;
                Refresh();
            }
        }

        void Refresh()
        {
            if (bundle == null) return;

            bool processing = ShopPurchaseManager.Instance.IsProcessing(bundle.bundleId);
            if (processing)
            {
                if (buyButton) buyButton.interactable = false;
                if (buyLabel)  buyLabel.text = "Processing…";
            } else {
                bool canBuy = _service.CanBuy(bundle);
                if (buyButton) buyButton.interactable = canBuy;
                if (buyLabel) buyLabel.text = canBuy ? "Buy" : "Unavailable";
            }
            if (infoButton) infoButton.gameObject.SetActive(SceneManager.GetActiveScene().name != "BundleDetailsScene");
        }

        void OnBuy()
        {
            if (bundle == null || ShopPurchaseManager.Instance.IsProcessing(bundle.bundleId)) return;
            Refresh();
            _service.StartPurchase(bundle, ok => { Refresh(); });
        }

        void OnInfo()
        {
            if (bundle == null) return;
            Core.SceneContext.SelectedBundleGuid = bundle.bundleId;
            SceneManager.LoadScene("BundleDetailsScene");
        }
    }
}
