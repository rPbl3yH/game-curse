using Modules.BaseUI;
using Modules.GameManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Scripts
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private Character _character;

        private MenuService _menuService;
        private GameManager _gameManager;
        
        [Inject]
        public void Construct(GameManager gameManager, MenuService menuService)
        {
            _gameManager = gameManager;
            _menuService = menuService;
        }
        
        public void LoseGame()
        {
            Debug.Log("Lose game");
            _gameManager.FinishGame();
            _character.Death();
            _menuService.ShowMenu(MenuType.Lose);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}