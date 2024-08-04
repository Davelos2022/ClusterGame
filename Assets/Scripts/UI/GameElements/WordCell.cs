using Cysharp.Threading.Tasks;
using DG.Tweening;
using MulticastProject.Core;
using MulticastProject.Interfaces;
using MulticastProject.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace MulticastProject.UI.GameElements
{
    public class WordCell : MonoBehaviour, IDropHandler, IAnimate
    {
        [SerializeField] private Ease _typeAnim;
        [SerializeField] private Vector3 _startScaleElement;
        [SerializeField] private Vector3 _endScaleElement; 
        [SerializeField] private float _duration = .5f;

        private AudioManager _audioManager;
        private LetterCluster _occupyingCluster;
        private string _currentText;

        [Inject]
        public void Construct(AudioManager audioManager) => _audioManager = audioManager; 
        public void SetContent(string text) => _currentText = text;

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null && eventData.pointerDrag.TryGetComponent(out LetterCluster cluster))
            {
                _audioManager.PlaySound(NameDataSounds.POSITIVE_REACTION_SOUND);

                if (_occupyingCluster != null && transform.childCount > 0)
                    _occupyingCluster.ResetPosition();

                _occupyingCluster = cluster;
                _occupyingCluster.transform.SetParent(transform, true);
                _occupyingCluster.transform.localPosition = Vector3.zero;

                Animate().Forget();
            }
        }

        public async UniTask Animate()
        {
            if (_occupyingCluster != null)
            {
                _occupyingCluster.transform.localScale = _startScaleElement;
                await _occupyingCluster.transform.DOScale(_endScaleElement, _duration).AsyncWaitForCompletion();
            }
        }

        public bool InCorrentCluster()
        {
            if (_occupyingCluster == null) return false;
            return _currentText.ToLower() == _occupyingCluster.TextContent.ToLower();
        }
    }
}
