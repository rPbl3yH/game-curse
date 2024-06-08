using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class LevelAudioController : MonoBehaviour
    {
        [SerializeField] private LevelController _levelController;

        private GameAudioConfig _gameAudioConfig;
        
        [Inject]
        public void Construct(GameAudioConfig gameAudioConfig)
        {
            _gameAudioConfig = gameAudioConfig;
        }
        
        private void OnEnable()
        {
            _levelController.LevelStarted += OnLevelStarted;
            _levelController.LevelCompleted += OnLevelCompleted;
            _levelController.LevelLost += OnLevelLost;
        }

        private void OnDisable()
        {
            _levelController.LevelStarted -= OnLevelStarted;
            _levelController.LevelCompleted -= OnLevelCompleted;
            _levelController.LevelLost -= OnLevelLost;
        }

        private void OnLevelLost()
        {
            AudioManager.Instance.PlaySound(_gameAudioConfig.PlayerDeathLose);
        }

        private void OnLevelCompleted()
        {
            AudioManager.Instance.PlaySound(_gameAudioConfig.LevelWin);
        }

        private void OnLevelStarted()
        {
            AudioManager.Instance.PlaySound(_gameAudioConfig.LevelStarted);
        }
    }
}