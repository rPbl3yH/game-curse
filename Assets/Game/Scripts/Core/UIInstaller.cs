using Game.Scripts.UI;
using Modules.BaseUI;
using Zenject;

namespace Game.Scripts
{
    public class UIInstaller : Installer<UIInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<MenuService>().FromComponentInHierarchy().AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<MusicSettingPresenter>()
                .FromComponentInHierarchy().AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<SoundSettingPresenter>()
                .FromComponentInHierarchy().AsCached().NonLazy();
        }
    }
}