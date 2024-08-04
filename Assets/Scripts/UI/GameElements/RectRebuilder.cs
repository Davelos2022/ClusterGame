using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MulticastProject.UI.GameElements
{
    public class RectRebuilder : MonoBehaviour
    {
        [SerializeField] private RectTransform _rect;
        [SerializeField] private float _rebuildDelay;

        public void Rebuild()
        {
            RebuildAsync().Forget();
        }

        public async UniTask RebuildAsync()
        {
            if (_rect == null)
            {
                _rect = GetComponent<RectTransform>();
            }

            if (_rect == null)
            {
                return;
            }

            await UniTask.Delay(TimeSpan.FromSeconds(_rebuildDelay));

            LayoutRebuilder.ForceRebuildLayoutImmediate(_rect);
            Canvas.ForceUpdateCanvases();
        }
    }
}