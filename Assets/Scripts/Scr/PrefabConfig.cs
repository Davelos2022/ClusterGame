using System;
using UnityEngine;

namespace MulticastProject.Scr
{
    [CreateAssetMenu(fileName = "FactoryConfig", menuName = "Factory/Config")]
    public class PrefabConfig : ScriptableObject
    {
        public PrefabEntry[] PrefabEntries;
    }

    [Serializable]
    public class PrefabEntry
    {
        public string Key;
        public GameObject Prefab;
    }
}