using Modules.SaveSystem;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public sealed class ZenjectGameContext : IGameContext
    {
        private DiContainer _diContainer;
        
        [Inject]
        public void Construct(DiContainer container)
        {
            _diContainer = container;
        }

        public T GetService<T>() where T : class
        {
            return _diContainer.Resolve<T>();
        }
    }
}