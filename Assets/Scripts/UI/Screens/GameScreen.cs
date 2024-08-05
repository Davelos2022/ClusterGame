using UnityEngine;
using MulticastProject.Base;
using MulticastProject.UI.GameElements;
using MulticastProject.Data;
using Zenject;

namespace MulticastProject.UI.Screens
{
    public class GameScreen : ScreenBase
    {
        [SerializeField] private ButtonAnimBase _backBTN;
        [SerializeField] private ButtonAnimBase _checkWords;


        private Notification _notification;

        [Inject]
        public void Construct(Notification notification) => _notification = notification;

        private void OnEnable()
        {
            _backBTN.Init(ClickBack);
            _checkWords.Init(ClickCheckWords);
        }

        private void OnDisable()
        {
            _backBTN.DeInit();
            _checkWords.DeInit();
        }

        private void ClickBack()
        {
            _screenManager.ShowScreen<MainMenuScreen>();
        }

        private void ClickCheckWords()
        {
            if (_gameController.CheckGameResult()) _gameController.CompletedGame();
            else _notification.SetContent(NameDataMassages.NEGATIVE_MESSAGE, Notification.TypeNotification.Negative);
        }
    }
}