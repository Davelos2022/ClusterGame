using UnityEngine;
using Zenject;
using MulticastProject.UI.GameElements;
using MulticastProject.Data;
using MulticastProject.Models;

namespace MulticastProject.Core
{
    /// <summary>
    /// Manages letter clusters on the game screen.
    /// </summary>
    public class ClusterManager : MonoBehaviour
    {
        [SerializeField] private Transform _wordsContainer;
        [SerializeField] private Transform _clusterContainer;

        private LevelManager _levelManager;
        private FactoryManager _factoryManager;

        private WordContainer[] _words;

        [Inject]
        public void Construct(LevelManager levelManager, FactoryManager factoryManager)
        {
            _levelManager = levelManager;
            _factoryManager = factoryManager;
        }
        
        private void OnEnable()
        {
            _levelManager.GenerationLevel += CreateClusters;
            _levelManager.GenerationLevel += CreateWord;
        }

        private void OnDisable()
        {
            _levelManager.GenerationLevel -= CreateClusters;
            _levelManager.GenerationLevel -= CreateWord;
        }

        /// <summary>
        /// Create clusters from level.
        /// </summary>
        private void CreateClusters()
        {
            ClearContent(_clusterContainer);

            ClusterData[] clusters = _levelManager.Clusters.ToArray();
            ShuffleArray(clusters);

            for (int x = 0; x < clusters.Length; x++)
            {
                var cluster = clusters[x];
                LetterCluster clusterObject =  _factoryManager.
                    CreateObject<LetterCluster>(NameDataObjects.CLUSTER_PREFAB, _clusterContainer);

                clusterObject.transform.localPosition = Vector2.zero;
                clusterObject.Setup(cluster);
            }
        }

        /// <summary>
        /// Create word cell from level.
        /// </summary>
        private void CreateWord()
        {
            ClearContent(_wordsContainer);

            _words = new WordContainer[_levelManager.CurrentLevel.WordDatas.Length];

            for (int x = 0; x < _words.Length; x++)
            {
                var wordData = _levelManager.CurrentLevel.WordDatas[x];

                string[] words = _levelManager.ClustersWord[wordData.Word];
                WordContainer wordContainer = _factoryManager.CreateObject<WordContainer>(NameDataObjects.WORD_PREFAB, _wordsContainer);
                wordContainer.SetupContainer(wordData.WordHint, words);
                _words[x] = wordContainer;
            }
        }

        /// <summary>
        /// Shuffles the elements of an array using the Fisher-Yates algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <param name="array">The array to shuffle.</param>
        private void ShuffleArray<T>(T[] array)
        {
            System.Random rng = new System.Random();

            int n = array.Length;

            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
        }

        /// <summary>
        /// Checks if all clusters are placed correctly.
        /// </summary>
        public bool ValidateClusters()
        {
            int correctWords = 0;

            foreach (var word in _words)
                if (word.InCorrectWord()) correctWords++;

            if (correctWords >= _words.Length) return true;
            else return false;
        }

        /// <summary>
        /// Clear content.
        /// </summary>
        private void ClearContent(Transform transform)
        {
            if (transform.childCount <= 0) return;

            for (int x = 0; x < transform.childCount; x++)
                Destroy(transform.GetChild(x).gameObject);
        }
    }
}