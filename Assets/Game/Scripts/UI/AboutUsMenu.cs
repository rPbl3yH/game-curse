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
        [SerializeField] private Image _panel;
        [SerializeField] private BaseUIButton _closeMenuButton;

        /*private MenuService _menuService;

        [Inject]
        public void Construct(MenuService menuService)
        {
            _menuService = menuService;
        }*/

        private void Awake()
        {
            gameObject.SetActive(false);

            _closeMenuButton.Clicked += Hide;

            _panel.transform.localPosition = Vector3.zero;
        }
        public override void Show()
        {
            gameObject.SetActive(true);

            _panel.transform.localPosition = Vector3.zero;


            var color = _panel.color;
            color.a = 0;
            _panel.color = color;

            color.a = 1;
            _panel.DOColor(color, 1);
        }

        public override void Hide()
        {

            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                transform.GetChild(i).DOMoveY(800, 1f);
            }

            var color = _panel.color;
            color.a = 0;
            _panel.DOColor(color, 1).OnComplete(() => gameObject.SetActive(false));
            //_panel.DOFade(0, 1).OnComplete(() => _menuService.HideMenu());
        }
    }
}
