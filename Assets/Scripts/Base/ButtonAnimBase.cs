using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using MulticastProject.Interfaces;
using MulticastProject.Data;
using MulticastProject.Core;
using Cysharp.Threading.Tasks;
using System;
using Zenject;

namespace MulticastProject.Base
{
    /// <summary>
    /// Abstract base class for buttons with animations in Unity.
    /// Provides methods for initialization, cleanup, and handling button click events with optional animations.
    /// Implements interface methods for pointer enter and exit events.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public abstract class ButtonAnimBase : MonoBehaviour, IAnimate, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected bool _isAnim;
        [SerializeField] protected Ease _typeAnimInput;
        [SerializeField] protected UnityEvent _onClickEvent;

        protected AudioManager _audioManager;
        protected bool _pressed = false;

        public Button Button { get; private set; }
        //public Button Button => _button;

        [Inject]
        public void Construct(AudioManager audioManager) => _audioManager = audioManager;

        /// <summary>
        /// Initializes the button with a callback for the click event.
        /// </summary>
        /// <param name="onClickCallBack">Action to be called when the button is clicked.</param>
        public virtual void Init(Action onClickCallBack)
        {
            Button = GetComponent<Button>();
            Button.onClick.AddListener(OnClickButton);
            _onClickEvent.AddListener(() => onClickCallBack());
        }

        /// <summary>
        /// Cleans up listeners from the button's onClick event.
        /// </summary>
        public virtual void DeInit()
        {
            Button.onClick.RemoveAllListeners();
            _onClickEvent?.RemoveAllListeners();
        }

        /// <summary>
        /// Handles the button click event, executing the animation if required and invoking the click event.
        /// </summary>
        public virtual async void OnClickButton()
        {
            _audioManager.PlayOneShotSound(NameDataSounds.CLICK_SOUND);

            if (_pressed || Button != null && !Button.interactable) return;

            _pressed = true;
            if (_isAnim) await Animate();

            _onClickEvent?.Invoke();
            _pressed = false;
        }


        /// <summary>
        /// Abstract method to handle pointer enter events.
        /// Must be implemented by derived classes.
        /// </summary>
        /// <param name="eventData">Pointer event data.</param>
        public abstract void OnPointerEnter(PointerEventData eventData);

        /// <summary>
        /// Abstract method to handle pointer exit events.
        /// Must be implemented by derived classes.
        /// </summary>
        /// <param name="eventData">Pointer event data.</param>
        public abstract void OnPointerExit(PointerEventData eventData);

        /// <summary>
        /// Abstract method to perform the animation.
        /// Must be implemented by derived classes.
        /// </summary>
        public abstract UniTask Animate();
    }
}