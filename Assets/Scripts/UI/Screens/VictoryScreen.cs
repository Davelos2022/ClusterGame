using TMPro;
using MulticastProject.Base;
using UnityEngine;
using MulticastProject.Models;
using MulticastProject.Data;
using MulticastProject.UI.GameElements;

namespace MulticastProject.UI.Screens
{
    public class VictoryScreen : ScreenBase
    {
        [SerializeField] private RectRebuilder _rectRebuilder;
        [SerializeField] private TextMeshProUGUI _wordsText;
        [SerializeField] private ButtonAnimBase _nextLevel;
        [SerializeField] private ButtonAnimBase _exitToMenu;

        private void OnEnable()
        {
            _nextLevel.Init(ClickNextLevel);
            _exitToMenu.Init(ClickExitToMenu);
        }

        private void OnDisable()
        {
            _nextLevel.DeInit();
            _exitToMenu.DeInit();
        }

        public override void Show()
        {
            base.Show();
            _audioManager.PlayOneShotSound(NameDataSounds.WIN_LEVEL_SOUND);
        }

        public void ShowWords(WordData[] words)
        {
            _wordsText.text = null;

            foreach (WordData word in words)
                _wordsText.text += word.Word + "\n";

            _rectRebuilder.Rebuild();
            Show();
        }

        private void ClickNextLevel()
        {
            _gameController.StartGame();
        }

        private void ClickExitToMenu()
        {       
            _screenManager.ShowScreen<MainMenuScreen>();
        }
    }
}