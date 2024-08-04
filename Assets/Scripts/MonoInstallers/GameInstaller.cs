using MulticastProject.Core;
using MulticastProject.UI.Screens;
using Zenject;

namespace MulticastProject.Monoinstallers
{
    /// <summary>
    ///DI inject.
    /// </summary>
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallControllers();
            InstallManagers();
            InstallUI();
        }

        private void InstallControllers()
        {
            Container.Bind<GameController>().FromComponentInHierarchy().AsSingle();
        }

        private void InstallManagers()
        {
            Container.Bind<LevelManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<AudioManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ScreenManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ClusterManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<FactoryManager>().FromComponentInHierarchy().AsSingle();
        }

        private void InstallUI()
        {
            Container.BindInterfacesAndSelfTo<GameScreen>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MainMenuScreen>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SettingsScreen>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<VictoryScreen>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}
