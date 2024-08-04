using UnityEngine;
using MulticastProject.Models;

namespace MulticastProject.Scr
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Level/Config")]
    public class LevelConfig : ScriptableObject
    {
        [Header("Level Config")]
        public LevelData[] LevelData;
    }
}