using System;
using DG.Tweening;
using Modules.BaseUI;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class SettingsMenu : MenuView
    {
        [SerializeField] private Transform _content;
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
        }

        public override void Hide()
        {
            base.Hide();
            // _tween?.Kill();
            // _tween = _content.DOScale(0, _tweenStat.Duration)
            //     .SetEase(_tweenStat.Easing)
            //     .SetLink(_content.gameObject)
            //     .OnComplete(base.Hide);
        }
    }
}