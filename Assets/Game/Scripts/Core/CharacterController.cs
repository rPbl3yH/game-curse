using Modules.GameManagement;
using UnityEngine;

namespace Game.Scripts
{
    public class CharacterController : MonoBehaviour, IGameFinishListener
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Character _character;
        
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