using System;
using Modules.BaseUI;
using Modules.GameManagement;
using Modules.SaveSystem;
using Sirenix.OdinInspector;
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
        private SaveLoadManager _saveLoadManager;
        private SceneLoader _sceneLoader = new();

        [ShowInInspector]
        public int Level { get; set; }
        public event Action LevelLost;
        public event Action LevelStarted;
        public event Action LevelCompleted;
        
        [Inject]
        public void Construct(
            GameManager gameManager, 
            MenuService menuService,
            SaveLoadManager saveLoadManager)
        {
            _gameManager = gameManager;
            _menuService = menuService;
            _saveLoadManager = saveLoadManager;
        }

        private void Start()
        {
            LoadGame();
            LoadScene();
            PrepareGame();
        }

        private void LoadGame()
        {
            _saveLoadManager.Load();
        }

        private void SaveGame()
        {
            _saveLoadManager.Save();
        }

        private void LoadScene()
        {
            _sceneLoader.LoadScene(Level);
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
            SaveGame();
        }

        public void WinGame()
        {
            LevelCompleted?.Invoke();
            _gameManager.FinishGame();
            SaveGame();
        }
        
        public void LoseGame()
        {
            LevelLost?.Invoke();
            Debug.Log("Lose game");
            _gameManager.FinishGame();
        }
        
        public void Restart()
        {
            _sceneLoader.ReloadScene();
        }

        public void LaunchNextLevel()
        {
            Level++;
            LoadScene();
        }
    }
}