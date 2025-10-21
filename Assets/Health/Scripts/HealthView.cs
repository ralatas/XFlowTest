using UnityEngine;
using TMPro;
using Core;

namespace Health
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI healthText;
        public int plusAmount = 5;

        void OnEnable()  { HealthController.OnHealthChanged += OnChanged; }
        void OnDisable() { HealthController.OnHealthChanged -= OnChanged; }

        void Start()
        {
            var pd = PlayerData.Instance;
            pd.TryGet(new HealthKey(), out int hp);
            healthText.text = hp.ToString();
        }

        public void OnPlusHealth() => HealthController.Add(plusAmount);

        void OnChanged(int hp) => healthText.text = hp.ToString();
    }
}
