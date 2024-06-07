using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.GameManagement
{
    public class GameManager : MonoBehaviour
    {
        [ShowInInspector]
        private List<IGameListener> _gameListeners = new();

        private List<IUpdateGameListener> _updateGameListeners = new();
        private List<IFixedUpdateGameListener> _fixedUpdateGameListeners = new();
        private List<ILateUpdateGameListener> _lateUpdateGameListeners = new();
        
        public void StartGame()
        {
            foreach (var listener in _gameListeners)
            {
                if (listener is IGameStartListener startListener)
                {
                    startListener.StartGame();
                }
            } 
        }
        
        public void FinishGame()
        {
            foreach (var listener in _gameListeners)
            {
                if (listener is IGameFinishListener finishListener)
                {
                    finishListener.FinishGame();
                }
            } 
        }
        
        public void PauseGame()
        {
            foreach (var listener in _gameListeners)
            {
                if (listener is IGamePauseListener pauseListener)
                {
                    pauseListener.PauseGame();
                }
            }    
        }

        public void ResumeGame()
        {
            foreach (var listener in _gameListeners)
            {
                if (listener is IGameResumeListener resumeListener)
                {
                    resumeListener.ResumeGame();
                }
            } 
        }

        public void OnUpdate(float deltaTime)
        {
            for (int i = 0; i < _fixedUpdateGameListeners.Count; i++)
            {
                _updateGameListeners[i].OnUpdate(deltaTime);
            }
        }

        public void OnFixedUpdate(float fixedDeltaTime)
        {
            for (int i = 0; i < _fixedUpdateGameListeners.Count; i++)
            {
                _fixedUpdateGameListeners[i].OnFixedUpdate(fixedDeltaTime);
            }
        }
    
        public void OnLateUpdate(float deltaTime)
        {
            for (int i = 0; i < _fixedUpdateGameListeners.Count; i++)
            {
                _lateUpdateGameListeners[i].OnLateUpdate(deltaTime);
            }
        }

        public void AddListener(IGameListener gameListener)
        {
            _gameListeners.Add(gameListener);

            if (gameListener is IUpdateGameListener updateGameListener)
            {
                _updateGameListeners.Add(updateGameListener);
            }
        
            if (gameListener is IFixedUpdateGameListener fixedUpdateGameListener)
            {
                _fixedUpdateGameListeners.Add(fixedUpdateGameListener);
            }
        
            if (gameListener is ILateUpdateGameListener lateUpdateGameListener)
            {
                _lateUpdateGameListeners.Add(lateUpdateGameListener);
            }
        }

        public void RemoveListener(IGameListener gameListener)
        {
            _gameListeners.Remove(gameListener);
        
            if (gameListener is IUpdateGameListener updateGameListener)
            {
                _updateGameListeners.Remove(updateGameListener);
            }
        
            if (gameListener is IFixedUpdateGameListener fixedUpdateGameListener)
            {
                _fixedUpdateGameListeners.Remove(fixedUpdateGameListener);
            }
        
            if (gameListener is ILateUpdateGameListener lateUpdateGameListener)
            {
                _lateUpdateGameListeners.Remove(lateUpdateGameListener);
            }
        }
    }
}