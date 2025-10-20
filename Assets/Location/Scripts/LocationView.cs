using UnityEngine;
using TMPro;
using Core;

namespace Location
{
    public class LocationView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI locationText;
        public string defaultLocation = "Town";

        void OnEnable() { DomainEvents.StoreChanged += Refresh; }
        void OnDisable() { DomainEvents.StoreChanged -= Refresh; }
        void Start() { Refresh(); }

        public void OnResetLocation() => LocationController.ResetToDefault(defaultLocation);

        void Refresh()
        {
            var pd = PlayerData.Instance;
            pd.TryGet(new LocationKey(), out string loc);
            locationText.text = loc ?? "-";
        }
    }
}
