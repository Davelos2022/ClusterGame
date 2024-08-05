using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;
using MulticastProject.UI.Screens;
using MulticastProject.Models;

namespace MulticastProject.Core
{
    /// <summary>
    /// Main class for game constroller.
    /// </summary>
    public class GameController : MonoBehaviour
    {
        private LevelManager _levelManager;
        private ClusterManager _clusterManager;
        private ScreenManager _screenManager;
        private PlayerManager _playerManager;

        private int _currentLevel;

        [Inject]
        public void Construct(LevelManager levelManager, ClusterManager clusterManager, ScreenManager screenManager)
        {
            _levelManager = levelManager;
            _clusterManager = clusterManager;
            _screenManager = screenManager;
        }

        private async void Start()
        {
            await InitializeGameAsync();
        }

        private async UniTask InitializeGameAsync()
        {
            _playerManager = new PlayerManager();
            await _levelManager.LoadLevelAsync();
            await _playerManager.LoadProgressAsync();

            _currentLevel = _playerManager.PlayerData.LevelGame;
            _screenManager.ShowScreen<MainMenuScreen>();
        }

        /// <summary>
        /// Method to start game.
        /// </summary>
        public void StartGame()
        {
            _levelManager.GenerateClusters(_currentLevel);
            _screenManager.ShowScreen<GameScreen>();
        }

        /// <summary>
        /// Check result game.
        /// </summary>
        public bool CheckGameResult()
        {
            return _clusterManager.ValidateClusters();
        }

        /// <summary>
        /// Completed game.
        /// </summary>
        public void CompletedGame()
        {
            _currentLevel++;
            VictoryScreen victoryScreen = (VictoryScreen)_screenManager.GetScreen<VictoryScreen>();
            victoryScreen?.ShowWords(_levelManager.CurrentLevel.WordDatas);
        }

        /// <summary>
        /// SaveGame progress.
        /// </summary>
        public async UniTask SaveProgress()
        {
            await _playerManager.SaveProgressAsync(new PlayerData { LevelGame = _currentLevel});
        }
    }
}
