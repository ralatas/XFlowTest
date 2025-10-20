using UnityEngine;
using TMPro;
using Core;
using System;

namespace VIP
{
    public class VipView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI vipText;
        public double plusSeconds = 30;

        void OnEnable() { DomainEvents.StoreChanged += Refresh; }
        void OnDisable() { DomainEvents.StoreChanged -= Refresh; }
        void Start() { Refresh(); }

        public void OnPlusVip() => VipController.AddSeconds(plusSeconds);

        void Refresh()
        {
            var pd = PlayerData.Instance;
            pd.TryGet(new VipTicksKey(), out long ticks);
            var ts = new TimeSpan(ticks);
            vipText.text = ((int)ts.TotalSeconds).ToString();
        }
    }
}
