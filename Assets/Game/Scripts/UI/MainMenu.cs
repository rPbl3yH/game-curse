using System;
using Modules.BaseUI;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI
{
    public class MainMenu : MenuView
    {
        [SerializeField] private BaseUIButton _startGameButton;
        [SerializeField] private BaseUIButton _settingsButton;
        [SerializeField] private BaseUIButton _aboutUsButton;

        private LevelController _levelController;
        private MenuService _menuService;
        
        [Inject]
        public void Construct(LevelController levelController, MenuService menuService)
        {
            _levelController = levelController;
            _menuService = menuService;
        }
        
        private void OnEnable()
        {
            _startGameButton.Clicked += OnClicked;
            _settingsButton.Clicked += SettingsButtonOnClicked;
            _aboutUsButton.Clicked += ShowAboutAsPanel;
        }

        private void OnDisable()
        {
            _startGameButton.Clicked -= OnClicked;
            _settingsButton.Clicked -= SettingsButtonOnClicked;
        }

        private void SettingsButtonOnClicked() => _menuService.ShowMenu(MenuType.Settings);

        private void OnClicked() => _levelController.StartGame();

        private void ShowAboutAsPanel() => _menuService.ShowMenu(MenuType.AboutUs);

    }
}