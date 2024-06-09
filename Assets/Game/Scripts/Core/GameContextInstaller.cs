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
            
            SaveLoadInstaller.Install(Container);
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
}