using System;
using DG.Tweening;
using Modules.BaseUI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.UI
{
    public class AboutUsMenu : MenuView
    {
        [SerializeField] private Animation _showAnimation;

        [SerializeField] private Image _panel;
        [SerializeField] private BaseUIButton _closeMenuButton;

        private MenuService _menuService;

        private void OnEnable() => _closeMenuButton.Clicked += CloseMenu;
        private void OnDisable() => _closeMenuButton.Clicked -= CloseMenu;

        [Inject]
        public void Constract(MenuService menuService)
        {
            _menuService = menuService;
        }

        private void Start()
        {
            //_showAnimation = GetComponent<Animation>();
            Debug.Log("StartInUI");
        }

        public override void Show()
        {
            Debug.Log("Show ");
            base.Show();

            _panel.transform.localPosition = Vector3.zero;


            var color = _panel.color;
            color.a = 0;
            _panel.color = color;

            color.a = 1;
            _panel.DOColor(color, 1).SetLink(this.gameObject);

            _showAnimation.Play();
        }

        private void CloseMenu()
        {
            _menuService.SwitchMenu(MenuType.Main);
        }

        public override void Hide()
        {

            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                transform.GetChild(i).DOMoveY(800, 1f).SetLink(this.gameObject);
            }

            var color = _panel.color;
            color.a = 0;
            _panel.DOColor(color, 0.5f).SetLink(this.gameObject).OnComplete(() => base.Hide());
        }
    }
}
