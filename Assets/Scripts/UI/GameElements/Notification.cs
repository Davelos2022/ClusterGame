using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MulticastProject.UI.GameElements
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Notification : MonoBehaviour
    {
        public enum TypeNotification
        {
            Negative,
            Positive
        }

        [SerializeField] private Image _iamgePanel;
        [SerializeField] private TextMeshProUGUI _contentText;
        [Space]
        [SerializeField] private Sprite _positiveIcon;
        [SerializeField] private Sprite _negativeIcon;

        private CanvasGroup _canvasGroup;
        private const float DURATION = 1.5f;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetContent(string text, TypeNotification typeNotification)
        {
            _contentText.text = text;
            _iamgePanel.sprite = typeNotification == TypeNotification.Positive ? _positiveIcon : _negativeIcon;
            ShowPanel();
        }

        private async void ShowPanel()
        {
            await _canvasGroup.DOFade(1, DURATION).AsyncWaitForCompletion();
            await _canvasGroup.DOFade(0, DURATION).AsyncWaitForCompletion();
        }

    }
}