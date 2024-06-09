using Modules.SaveSystem;
using Zenject;

namespace Game.Scripts
{
    public class SaveLoadInstaller : Installer<SaveLoadInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SettingsSaveLoader>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LevelSaveLoader>().AsSingle().NonLazy();
            
            Container.Bind<SaveLoadManager>().AsSingle().NonLazy();
        }
    }
}