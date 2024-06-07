using Modules.GameManagement;
using Zenject;

namespace Game.Scripts
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameManager>().FromComponentInHierarchy().AsCached().NonLazy();
            Container.Bind<GameController>().FromComponentInHierarchy().AsCached().NonLazy();
            Container.Bind<TriggerListener>().AsCached().NonLazy();
        }
    }
}