using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Modules.GameManagement
{
    public class GameListenerInstaller : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;

        [Inject]
        public void Construct(List<IGameListener> gameListeners)
        {
            Install(gameListeners);
        }

        private void Install(List<IGameListener> gameListeners)
        {
            foreach (var gameListener in gameListeners)
            {
                _gameManager.AddListener(gameListener);
            }
        }
    }
}
