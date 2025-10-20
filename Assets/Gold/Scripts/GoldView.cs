using UnityEngine;
using TMPro;
using Core;

namespace Gold
{
    public class GoldView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI goldText;
        public int plusAmount = 10;

        void OnEnable() { DomainEvents.StoreChanged += Refresh; }
        void OnDisable() { DomainEvents.StoreChanged -= Refresh; }
        void Start() { Refresh(); }

        public void OnPlusGold() => GoldController.Add(plusAmount);

        void Refresh()
        {
            var pd = PlayerData.Instance;
            pd.TryGet(new GoldKey(), out int g);
            goldText.text = g.ToString();
        }
    }
}
