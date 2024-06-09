using DG.Tweening;
using Modules.BaseUI;
using Modules.Input;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class TutorialMenu : MenuView
    {
        [SerializeField] private Transform _content;
        [SerializeField] private TutorialFingerView _fingerView;
        [SerializeField] private JoystickInput _joystickInput;
        [SerializeField] private MenuService _menuService;
        
        [Header("ShowAnimation")]
        [SerializeField] private BaseTweenStat _tweenStat;
        
        private Tween _tween;
        
        public override void Show()
        {
            base.Show();
            _tween?.Kill();
            _tween = _content.DOScale(_tweenStat.TargetValue, _tweenStat.Duration)
                .From(0f)
                .SetEase(_tweenStat.Easing)
                .SetLink(_content.gameObject);
            
            _fingerView.Show();
            _joystickInput.DirectionMoved += OnDirectionMoved;
        }

        private void OnDirectionMoved(Vector2 direction)
        {
            _joystickInput.DirectionMoved -= OnDirectionMoved;
            _menuService.HideMenu();
        }

        public override void Hide()
        {
            _tween?.Kill();
            _tween = _content.DOScale(0f, _tweenStat.Duration)
                .SetEase(_tweenStat.Easing)
                .SetLink(_content.gameObject)
                .OnComplete(() => base.Hide());
        }
    }
}