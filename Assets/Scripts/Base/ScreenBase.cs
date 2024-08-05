using UnityEngine;
using DG.Tweening;
using MulticastProject.Interfases;
using Cysharp.Threading.Tasks;
using MulticastProject.Interfaces;
using MulticastProject.Core;
using Zenject;

namespace MulticastProject.Base
{
    /// <summary>
    /// Base class for all screens.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class ScreenBase : MonoBehaviour, IScreen, IAnimate
    {
        public enum TypeAnim
        {
            None,
            Scale,
            Move,
            Fade
        }

        [SerializeField] private bool _isAnim = false;
        [SerializeField] private TypeAnim _typeAnim;
        [SerializeField] private float _duration;
        [SerializeField] private Vector3 _startVector;
        [SerializeField] private Vector3 _targetVector;

        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;

        protected ScreenManager _screenManager;
        protected GameController _gameController;
        protected AudioManager _audioManager;

        [Inject]
        public void Construct(ScreenManager screenManager, GameController gameController, AudioManager audioManager)
        {
            _screenManager = screenManager;
            _gameController = gameController;
            _audioManager = audioManager;
        }

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public async virtual void Show()
        {
            gameObject.SetActive(true);
            if (_isAnim) await Animate();
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual async UniTask Animate()
        {
            DOTween.KillAll(this);

            switch (_typeAnim)
            {
                case TypeAnim.Scale:
                    _rectTransform.localScale = _startVector;
                    await _rectTransform.DOScale(_targetVector, _duration).AsyncWaitForCompletion();
                    break;
                case TypeAnim.Move:
                    _rectTransform.anchoredPosition = _startVector;
                    await _rectTransform.DOLocalMove(_targetVector, _duration).AsyncWaitForCompletion();
                    break;
                case TypeAnim.Fade:
                    _canvasGroup.alpha = 0;
                    await _canvasGroup.DOFade(1, _duration).AsyncWaitForCompletion();
                    break;
            }
        }
    }
}
