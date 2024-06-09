using Game.Scripts.UI;
using Modules.BaseUI;
using Modules.GameManagement;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class GameContextInstaller : MonoInstaller
    {
        [SerializeField] private GameAudioConfig _gameAudioConfig;
        [SerializeField] private ItemConfig _itemConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<GameAudioConfig>().FromInstance(_gameAudioConfig).AsSingle().NonLazy();
            Container.Bind<ItemConfig>().FromInstance(_itemConfig).AsSingle().NonLazy();
            UIInstaller.Install(Container);

            Container.Bind<CharacterService>().FromComponentInHierarchy().AsCached().NonLazy();

            Container.Bind<GameManager>().FromComponentInHierarchy().AsCached().NonLazy();
            Container.Bind<LevelController>().FromComponentInHierarchy().AsCached().NonLazy();
            Container.Bind<TriggerListener>().AsCached().NonLazy();

            Container.BindInterfacesAndSelfTo<CharacterInputController>()
                .FromComponentInHierarchy().AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<CharacterController>()
                .FromComponentInHierarchy().AsCached().NonLazy();
        }
    }

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