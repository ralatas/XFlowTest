using UnityEngine;
using TMPro;
using Core;

namespace Health
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI healthText;
        public int plusAmount = 5;

        void OnEnable() { DomainEvents.StoreChanged += Refresh; }
        void OnDisable() { DomainEvents.StoreChanged -= Refresh; }
        void Start() { Refresh(); }

        public void OnPlusHealth() => HealthController.Add(plusAmount);

        void Refresh()
        {
            var pd = PlayerData.Instance;
            pd.TryGet(new HealthKey(), out int hp);
            healthText.text = hp.ToString();
        }
    }
}
