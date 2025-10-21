using UnityEngine;
using TMPro;
using Core;

namespace Location
{
    public class LocationView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI locationText;
        public string defaultLocation = "Town";

        void OnEnable()  { LocationController.OnLocationChanged += OnChanged; }
        void OnDisable() { LocationController.OnLocationChanged -= OnChanged; }

        void Start()
        {
            PlayerData.Instance.TryGet(new LocationKey(), out string loc);
            locationText.text = loc ?? "-";
        }

        public void OnResetLocation() => LocationController.ResetToDefault(defaultLocation);

        void OnChanged(string loc) => locationText.text = loc ?? "-";
    }
}
