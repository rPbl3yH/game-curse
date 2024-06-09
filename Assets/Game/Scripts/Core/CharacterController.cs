using System;
using Modules.GameManagement;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class CharacterController : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        [SerializeField] private Transform _spawnPoint;
        private Character _character;
        private LevelController _levelController;

        [Inject]
        private void Construct(CharacterService characterService, LevelController levelController)
        {
            _character = characterService.GetCharacter();
            _levelController = levelController;
        }

        void IGameStartListener.StartGame()
        {
            _levelController.LevelLost += OnLevelLost;
            _levelController.LevelCompleted += OnLevelCompleted;
        }

        void IGameFinishListener.FinishGame()
        {
            _levelController.LevelLost -= OnLevelLost;
            _levelController.LevelCompleted -= OnLevelCompleted;
        }

        public void Setup()
        {
            _character.transform.position = _spawnPoint.position;
        }

        private void OnLevelCompleted()
        {
            _character.Stop();
        }

        private void OnLevelLost()
        {
            _character.Death();
        }
    }
}