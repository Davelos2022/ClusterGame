using UnityEngine;
using MulticastProject.Base;
using MulticastProject.Data;

namespace MulticastProject.UI.Screens
{
    public class MainMenuScreen : ScreenBase
    {
        [SerializeField] private ButtonAnimBase _playGame;
        [SerializeField] private ButtonAnimBase _settingsGame;
        [SerializeField] private ButtonAnimBase _exitGame;


        private void OnEnable()
        {
            _playGame.Init(ClickPlayGame);
            _settingsGame.Init(ClickSettingsGame);
            _exitGame.Init(ClickExitGame);
        }

        private void OnDisable()
        {
            _playGame.DeInit();
            _settingsGame.DeInit();
            _exitGame.DeInit();
        }

        public override void Show()
        {
            base.Show();
            _audioManager.PlaySound(NameDataSounds.OST_SOUND, true);
        }

        private void ClickPlayGame()
        {
            _audioManager.StopSound();
            _gameController.StartGame();
        }

        private void ClickSettingsGame()
        {
            SettingsScreen settingsScreen = (SettingsScreen)_screenManager.GetScreen<SettingsScreen>();
            settingsScreen.Show();
        }

        private async void ClickExitGame()
        {
            await _gameController.SaveProgress();
            Application.Quit();
        }
    }
}
