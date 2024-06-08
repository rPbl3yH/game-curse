using Modules.BaseUI;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI
{
    public class MainMenu : MenuView
    {
        [SerializeField] private BaseUIButton _button;

        private LevelController _levelController;
        
        [Inject]
        public void Construct(LevelController levelController)
        {
            _levelController = levelController;
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
            _levelController.StartGame();
        }
    }
}