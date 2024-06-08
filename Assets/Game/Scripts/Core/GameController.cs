using System;
using Modules.BaseUI;
using Modules.GameManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Scripts
{
    public class GameController : MonoBehaviour
    {
        private CharacterController _characterController;
        
        private MenuService _menuService;
        private GameManager _gameManager;
        
        [Inject]
        public void Construct(GameManager gameManager, MenuService menuService)
        {
            _gameManager = gameManager;
            _menuService = menuService;
        }

        private void Start()
        {
            _characterController = FindObjectOfType<CharacterController>();

            PrepareGame();
        }

        private void PrepareGame()
        {
            _menuService.ShowMenu(MenuType.Main);
            _characterController.Setup();
        }

        public void StartGame()
        {
            Debug.Log("Start Game");
            _menuService.HideMenu();
            _gameManager.StartGame();
        }
        
        public void LoseGame()
        {
            Debug.Log("Lose game");
            _gameManager.FinishGame();
            _menuService.ShowMenu(MenuType.Lose);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}