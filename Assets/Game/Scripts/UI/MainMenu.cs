using Modules.BaseUI;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI
{
    public class MainMenu : MenuView
    {
        [SerializeField] private BaseUIButton _button;

        private GameController _gameController;
        
        [Inject]
        public void Construct(GameController gameController)
        {
            _gameController = gameController;
        }
        
        private void OnEnable()
        {
            _button.Clicked += OnClicked;
        }

        private void OnDisable()
        {
            _button.Clicked -= OnClicked;
        }

        private void OnClicked()
        {
            _gameController.StartGame();
        }
    }
}