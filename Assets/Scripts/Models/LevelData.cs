using System;

namespace MulticastProject.Models
{
    /// <summary>
    /// Represents the data for a game level.
    /// </summary>
    [Serializable]
    public class LevelData
    {
        public int Level;
        public WordData[] WordDatas;
    }

    [Serializable]
    public class WordData
    {
        public string WordHint;
        public string Word;
    }
}
