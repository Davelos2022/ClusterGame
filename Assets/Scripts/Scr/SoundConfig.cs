using UnityEngine;
using System;
using System.Collections.Generic;

namespace MulticastProject.Scr
{
    [CreateAssetMenu(fileName = "SoundConfiguration", menuName = "SoundConfig")]
    public class SoundConfig : ScriptableObject
    {
        public List<SoundEntry> SoundEntries;
    }

    [Serializable]
    public class SoundEntry
    {
        public string Key;
        public AudioClip AudioClip;
    }
}