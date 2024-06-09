using System;
using Modules.BaseUI;
using Modules.GameManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Scripts
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField]
        private CharacterController _characterController;
        
        private MenuService _menuService;
        private GameManager _gameManager;

        public event Action LevelLost;
        public event Action LevelStarted;
        public event Action LevelCompleted;
        
        [Inject]
        public void Construct(GameManager gameManager, MenuService menuService)
        {
            _gameManager = gameManager;
            _menuService = menuService;
        }

        private void Start()
        {
            PrepareGame();
        }

        private void PrepareGame()
        {
            _gameManager.InitGame();
            _menuService.ShowMenu(MenuType.Main);
            _characterController.Setup();
        }

        public void StartGame()
        {
            Debug.Log("Start Game");
            _menuService.HideMenu();
            _gameManager.StartGame();
            
            LevelStarted?.Invoke();
        }

        public void WinGame()
        {
            LevelCompleted?.Invoke();
            _gameManager.FinishGame();
        }
        
        public void LoseGame()
        {
            LevelLost?.Invoke();
            Debug.Log("Lose game");
            _gameManager.FinishGame();
        }
        
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}