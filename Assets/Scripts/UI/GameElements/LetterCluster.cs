using UnityEngine;
using UnityEngine.EventSystems;
using MulticastProject.Models;
using TMPro;

namespace MulticastProject.UI.GameElements
{
    /// <summary>
    /// Represents a draggable letter cluster.
    /// </summary>

    [RequireComponent(typeof(CanvasGroup))]
    public class LetterCluster : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private LayerMask _ignoreLayer;
        [SerializeField] private TextMeshProUGUI _text;

        private ClusterData _clusterData;
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;
        private Canvas _canvas;

        private Transform _startParent;
        private Vector3 _startPosition;
        public string TextContent => _text.text;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();

            _startParent = transform.parent;
            _startPosition = transform.position;
        }

        public void Setup(ClusterData clusterData)
        {
            _clusterData = clusterData;
            _text.text = _clusterData.Cluster;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = .5f;
            _canvasGroup.blocksRaycasts = false;
            _rectTransform.SetParent(_canvas.transform);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;

            if ((1 << eventData.pointerEnter.layer & _ignoreLayer) != 0) return;

            if (!eventData.pointerEnter.TryGetComponent<WordCell>(out var wordCell)) ResetPosition();
        }

        public void ResetPosition()
        {
            transform.SetParent(_startParent, false);
            transform.localPosition = _startPosition;
        }
    }
}

