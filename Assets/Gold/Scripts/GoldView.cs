using UnityEngine;
using TMPro;
using Core;

namespace Gold
{
    public class GoldView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI goldText;
        public int plusAmount = 10;

        void OnEnable()  { GoldController.OnGoldChanged += OnGoldChanged; }
        void OnDisable() { GoldController.OnGoldChanged -= OnGoldChanged; }

        void Start()
        {
            var pd = PlayerData.Instance;
            pd.TryGet(new GoldKey(), out int g);
            goldText.text = g.ToString();
        }

        public void OnPlusGold() => GoldController.Add(plusAmount);

        void OnGoldChanged(int gold) => goldText.text = gold.ToString();
    }
}
