using MulticastProject.Base;
using MulticastProject.Data;
using UnityEngine.UI;
using UnityEngine;

namespace MulticastProject.UI.Screens
{
    public class SettingsScreen : ScreenBase
    {
        [SerializeField] private ButtonAnimBase _close;
        [SerializeField] private Toggle _soundToggle;

        private void OnEnable()
        {
            _close.Init(ClickClose);
            _soundToggle.onValueChanged.AddListener(OnSoundToggleChanged);
        }

        private void OnDisable()
        {
            _close.DeInit();
            _soundToggle.onValueChanged.RemoveListener(OnSoundToggleChanged);
        }

        private void OnSoundToggleChanged(bool isOn)
        {
            if (!isOn) _audioManager.PlaySound(NameDataSounds.CLICK_SOUND);
            _audioManager.SetMute(isOn);
        }

        public void ClickClose()
        {
            Hide();
        }
    }
}