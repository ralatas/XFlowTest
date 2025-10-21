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

        void OnEnable()  { VipController.OnVipChanged += OnChanged; }
        void OnDisable() { VipController.OnVipChanged -= OnChanged; }

        void Start()
        {
            PlayerData.Instance.TryGet(new VipTicksKey(), out long ticks);
            vipText.text = ((int)new TimeSpan(ticks).TotalSeconds).ToString();
        }

        public void OnPlusVip() => VipController.AddSeconds(plusSeconds);

        void OnChanged(long ticks)
        {
            vipText.text = ((int)new TimeSpan(ticks).TotalSeconds).ToString();
        }
    }
}
