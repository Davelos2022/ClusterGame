using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;
using System;
using MulticastProject.Base;

namespace MulticastProject.UI.ButtonsAnim
{
    public class ButtonScaleAnim : ButtonAnimBase
    {
        [SerializeField][Range(0.1f, 1.5f)] private float _scaleInputTarget = .9f;
        [SerializeField][Range(0.1f, 3f)] private float _durationInputAnim = .2f;
        [Space]
        [SerializeField][Range(0.1f, 3f)] private float _scalePointerTarget = 1.2f;
        [SerializeField][Range(0.1f, 3f)] private float _durationScaleAnim = .4f;

        public override void Init(Action onClickCallBack)
        {
            base.Init(onClickCallBack);
        }

        public override void DeInit()
        {
            base.DeInit();
        }

        public override void OnClickButton()
        {
            base.OnClickButton();
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (_pressed || Button != null && !Button.interactable) return;

            if (_isAnim)
            {
                DOTween.Kill(transform);
                Button.transform.DOScale(_scalePointerTarget, _durationScaleAnim);
            }
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            if (_pressed || Button != null && !Button.interactable) return;

            if (_isAnim)
            {
                DOTween.Kill(transform);
                Button.transform.DOScale(Vector3.one, _durationScaleAnim);
            }
        }

        public override async UniTask Animate()
        {
            DOTween.Kill(transform);
            await Button.transform.DOScale(_scaleInputTarget, _durationInputAnim).AsyncWaitForCompletion();
            await Button.transform.DOScale(Vector3.one, _durationInputAnim).AsyncWaitForCompletion();
        }
    }
}