using UnityEngine;
using Cysharp.Threading.Tasks;
using MulticastProject.Models;
using MulticastProject.Utils;
using MulticastProject.Data;
using System;
using System.IO;
using System.Collections.Generic;

namespace MulticastProject.Core
{
    public class LevelManager : MonoBehaviour
    {
        public List<ClusterData> Clusters { get; private set; }
        public LevelData CurrentLevel { get; private set; }
        public Dictionary<string, string[]> ClustersWord { get; private set; }

        private LevelData[] LevelData;

        private const int CLUSTER_SIZE_MIN = 2;
        private const int CLUSTER_SIZE_MAX = 4;

        public Action GenerationLevel;

        /// <summary>
        /// Loads the level from a JSON file.
        /// </summary>
        public async UniTask LoadLevelAsync()
        {
            string pathToFile = Path.Combine(NameDataSystem.DIRECTORY_LEVEL, NameDataSystem.NAME_LEVEL_JSON_FILE);
            LevelData = await JSONService.LoadJSONAsync<LevelData[]>(pathToFile, TypeTargetJson.Resources);
        }

        /// <summary>
        /// Generates clusters from the words in the level.
        /// </summary>
        public void GenerateClusters(int level)
        {
            if (level >= LevelData.Length) level = UnityEngine.Random.Range(0, LevelData.Length -1);

            CurrentLevel = LevelData[level];
            Clusters = new List<ClusterData>();
            ClustersWord = new Dictionary<string, string[]>();

            foreach (var wordData in CurrentLevel.WordDatas)
            {
                string[] clusters = CreateClustersFromWord(wordData.Word);
                ClustersWord[wordData.Word] = clusters;

                for (int x = 0; x < clusters.Length; x++)
                    Clusters.Add(new ClusterData(clusters[x]));
            }

            GenerationLevel?.Invoke();
        }

        /// <summary>
        /// Creates and shuffles clusters from a given word.
        /// </summary>
        /// <param name="word">The word to split into clusters.</param>
        /// <returns>A shuffled array of word clusters.</returns>
        public string[] CreateClustersFromWord(string word)
        {
            if (string.IsNullOrEmpty(word)) return Array.Empty<string>();

            List<string> clusters = new List<string>();

            int i = 0;
            while (i < word.Length)
            {
                int remainingLength = word.Length - i;
                int clusterSize = UnityEngine.Random.Range(CLUSTER_SIZE_MIN, CLUSTER_SIZE_MAX + 1);

                if (remainingLength <= CLUSTER_SIZE_MAX && remainingLength >= CLUSTER_SIZE_MIN)
                {
                    clusterSize = remainingLength;
                }
                else if (remainingLength < CLUSTER_SIZE_MIN)
                {
                    if (clusters.Count > 0)
                    {
                        clusters[clusters.Count - 1] += word.Substring(i);
                    }
                    break;
                }
                else if (remainingLength < clusterSize)
                {
                    clusterSize = remainingLength;
                }

                clusters.Add(word.Substring(i, clusterSize));
                i += clusterSize;
            }

            string[] clustersArray = clusters.ToArray();
            return clustersArray;
        }
    }
}
