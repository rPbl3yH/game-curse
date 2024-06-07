using Modules.GameManagement;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class GameController : MonoBehaviour
    {
        private GameManager _gameManager;

        [Inject]
        public void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }
        
        public void LoseGame()
        {
            Debug.Log("Lose game");
            _gameManager.FinishGame();
        }
    }
}