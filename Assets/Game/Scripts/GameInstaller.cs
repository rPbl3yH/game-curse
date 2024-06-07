using Modules.BaseUI;
using Modules.GameManagement;
using Zenject;

namespace Game.Scripts
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            UIInstaller.Install(Container);

            Container.Bind<GameManager>().FromComponentInHierarchy().AsCached().NonLazy();
            Container.Bind<GameController>().FromComponentInHierarchy().AsCached().NonLazy();
            Container.Bind<TriggerListener>().AsCached().NonLazy();
        }
    }

    public class UIInstaller : Installer<UIInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<MenuService>().FromComponentInHierarchy().AsCached().NonLazy();
        }
    }
}