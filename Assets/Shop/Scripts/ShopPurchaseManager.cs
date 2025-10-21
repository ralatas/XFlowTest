using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Shop
{
    public sealed class ShopPurchaseManager
    {
        private static readonly Lazy<ShopPurchaseManager> _lazy = new(() => new ShopPurchaseManager());
        public static ShopPurchaseManager Instance => _lazy.Value;

        private readonly Dictionary<string, float> _processingUntil = new();
        private readonly Dictionary<string, List<Action<bool>>> _completionHandlers = new();

        private ShopPurchaseManager() { }

        public bool IsProcessing(string bundleId)
        {
            if (string.IsNullOrEmpty(bundleId)) return false;
            if (_processingUntil.TryGetValue(bundleId, out float until))
            {
                if (Time.realtimeSinceStartup < until) return true;
                _processingUntil.Remove(bundleId);
            }
            return false;
        }

        public void StartPurchase(ShopBundleSO bundle, float delaySeconds, Action<bool> onDone)
        {
            if (bundle == null) { onDone?.Invoke(false); return; }

            if (IsProcessing(bundle.bundleId))
            {
                if (onDone != null)
                {
                    if (!_completionHandlers.TryGetValue(bundle.bundleId, out var list))
                        _completionHandlers[bundle.bundleId] = list = new List<Action<bool>>();
                    list.Add(onDone);
                }
                return;
            }

            Begin(bundle.bundleId, delaySeconds);

            if (onDone != null)
            {
                if (!_completionHandlers.TryGetValue(bundle.bundleId, out var list))
                    _completionHandlers[bundle.bundleId] = list = new List<Action<bool>>();
                list.Add(onDone);
            }

            RunPurchaseAsync(bundle, delaySeconds).Forget();
        }

        private async UniTaskVoid RunPurchaseAsync(ShopBundleSO b, float delaySeconds)
        {
            try
            {
                await UniTask.Delay(
                    TimeSpan.FromSeconds(delaySeconds),
                    delayType: DelayType.UnscaledDeltaTime,
                    cancellationToken: default
                );

                bool ok = CompositeRunner.ExecuteTransaction(b.Costs, b.Rewards, PlayerData.Instance);

                Finish(b.bundleId);
                if (ok) DomainEvents.RaiseStoreChanged();

                if (_completionHandlers.TryGetValue(b.bundleId, out var handlers))
                {
                    _completionHandlers.Remove(b.bundleId);
                    foreach (var h in handlers.ToArray())
                        h?.Invoke(ok);
                }
            }
            catch (Exception e)
            {
                // На всякий случай: снимем processing и сообщим всем ожидателям о fail
                Finish(b.bundleId);
                if (_completionHandlers.TryGetValue(b.bundleId, out var handlers))
                {
                    _completionHandlers.Remove(b.bundleId);
                    foreach (var h in handlers.ToArray()) h?.Invoke(false);
                }
                Debug.LogException(e);
            }
        }

        private void Begin(string bundleId, float seconds)
        {
            if (string.IsNullOrEmpty(bundleId)) return;
            _processingUntil[bundleId] = Time.realtimeSinceStartup + seconds;
            DomainEvents.RaiseStoreChanged();
        }

        private void Finish(string bundleId)
        {
            if (string.IsNullOrEmpty(bundleId)) return;
            if (_processingUntil.Remove(bundleId))
                DomainEvents.RaiseStoreChanged();
        }
    }
}