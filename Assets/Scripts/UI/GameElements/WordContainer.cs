using UnityEngine;
using TMPro;
using Zenject;
using MulticastProject.Core;
using MulticastProject.Data;
using DG.Tweening;

namespace MulticastProject.UI.GameElements
{
    /// <summary>
    /// WordContainer class for wordCell class.
    /// </summary>
    public class WordContainer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _hintWordText;
        [SerializeField] private Transform _wordCellsContainer;
        [SerializeField] private GameObject _wordCellPrefab;
        [Space]
        [SerializeField] private CanvasGroup _notCorrectWord;

        private FactoryManager _factoryManager;
        private WordCell[] _wordCells;

        private const float DURATION = 1.5f;

        [Inject]
        public void Construct(FactoryManager factoryManager) => _factoryManager = factoryManager;

        public void SetupContainer(string hintText, string[] clusterWord)
        {
            if (string.IsNullOrEmpty(hintText) || clusterWord == null) return;

            _hintWordText.text = hintText;
            _wordCells = new WordCell[clusterWord.Length];

            for (int x = 0; x < clusterWord.Length; x++)
            {
                var clusster = clusterWord[x];
                WordCell wordCell = _factoryManager.CreateObject<WordCell>(NameDataObjects.CELL_PREFAB, _wordCellsContainer);
                wordCell.SetContent(clusster);
                _wordCells[x] = wordCell;
            }
        }

        public bool InCorrectWord()
        {
            foreach (var cell in _wordCells)
            {
                if (!cell.InCorrentCluster())
                {
                    ShowNotCorrection();
                    return false;
                }
            }

            return true;
        }

        private async void ShowNotCorrection()
        {
            await _notCorrectWord.DOFade(1, DURATION).AsyncWaitForCompletion();
            await _notCorrectWord.DOFade(0, DURATION).AsyncWaitForCompletion();
        }
    }
}