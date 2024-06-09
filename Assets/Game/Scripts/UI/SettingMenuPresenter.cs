using Modules.BaseUI;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI
{
    public class SettingMenuPresenter : MonoBehaviour
    {
        [SerializeField] private BaseUIButton _closeSettingButton;

        private MenuService _menuService;

        [Inject] 
        public void Construct(MenuService menuService)
        {
            _menuService = menuService;
        }

        private void OnEnable()
        {
            _closeSettingButton.Clicked += OnCloseClicked;
        }

        private void OnDisable()
        {
            _closeSettingButton.Clicked -= OnCloseClicked;
        }

        private void OnCloseClicked()
        {
            _menuService.HideMenu();
        }
    }
}