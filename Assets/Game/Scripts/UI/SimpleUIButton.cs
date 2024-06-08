using System;
using DG.Tweening;
using Modules.BaseUI;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class SimpleUIButton : BaseUIButton
    {
        [SerializeField] private RectTransform _buttonTransform;
        [SerializeField] private BaseTweenStat _tweenStat;

        private Tween _tween;
        
        protected override void OnClicked()
        {
            base.OnClicked();
            _tween?.Kill(true);
            _tween = _buttonTransform.DOPunchScale(Vector3.one * _tweenStat.TargetValue,
                    _tweenStat.Duration, 1, 0)
                .SetEase(_tweenStat.Easing)
                .SetLink(gameObject);
        }
    }
}