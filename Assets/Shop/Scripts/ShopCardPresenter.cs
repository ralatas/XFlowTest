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
        bool _processing;

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
            Refresh();
        }

        void Refresh()
        {
            if (_processing)
            {
                if (buyButton) buyButton.interactable = FalseIfPresent(buyButton);
                if (buyLabel) buyLabel.text = "Processing…";
                return;
            }
            bool canBuy = bundle && _service.CanBuy(bundle);
            if (buyButton) buyButton.interactable = canBuy;
            if (buyLabel) buyLabel.text = canBuy ? "Buy" : "Unavailable";
            Debug.Log("Scene name: " + SceneManager.GetActiveScene().name);
            infoButton.gameObject.SetActive(SceneManager.GetActiveScene().name != "BundleDetailsScene");
        }

        bool FalseIfPresent(Button b) { return false; }

        void OnBuy()
        {
            if (_processing || bundle == null) return;
            _processing = true;
            Refresh();
            StartCoroutine(_service.BuyRoutine(bundle, ok =>
            {
                _processing = false;
                Refresh();
            }));
        }

        void OnInfo()
        {
            if (bundle == null) return;
            Core.SceneContext.SelectedBundleGuid = bundle.bundleId;
            SceneManager.LoadScene("BundleDetailsScene");
        }
    }
}
