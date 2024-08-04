using MulticastProject.Scr;
using System.Collections.Generic;
using UnityEngine;

namespace MulticastProject.Core
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private SoundConfig _soundConfiguration;

        private Dictionary<string, AudioClip> _soundRegistry;

        private void Awake()
        {
            InitializeSoundRegistry();
        }

        private void InitializeSoundRegistry()
        {
            _soundRegistry = new Dictionary<string, AudioClip>();

            foreach (var entry in _soundConfiguration.SoundEntries)
            {
                if (!_soundRegistry.ContainsKey(entry.Key))
                {
                    _soundRegistry[entry.Key] = entry.AudioClip;
                }
            }
        }

        public void SetMute(bool isMuted)
        {
            _audioSource.mute = isMuted;
        }

        public void PlaySound(string key, bool loop = false)
        {
            if (_soundRegistry.ContainsKey(key))
            {
                _audioSource.clip = _soundRegistry[key];
                _audioSource.loop = loop;
                _audioSource.Play();  
            }
            else
            {
                Debug.LogWarning($"Audio clip for key '{key}' not found.");
            }
        }

        public void PlayOneShotSound(string key)
        {
            if (_soundRegistry.ContainsKey(key)) _audioSource.PlayOneShot(_soundRegistry[key]);
            else Debug.LogWarning($"Audio clip for key '{key}' not found.");
        }

        public void StopSound()
        {
            _audioSource.Stop();
        }
    }
}
