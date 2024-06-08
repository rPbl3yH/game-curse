using Modules.GameManagement;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class CharacterController : MonoBehaviour, IGameFinishListener
    {
        [SerializeField] private Transform _spawnPoint;
        private Character _character;

        [Inject]
        private void Construct(CharacterService characterService)
        {
            _character = characterService.GetCharacter();
        }
        
        public void Setup()
        {
            _character.transform.position = _spawnPoint.position;
        }
        
        public void FinishGame()
        {
            _character.Death();
        }
    }
}