using System;
using DG.Tweening;
using UnityEngine;

namespace Modules.BaseUI
{
    public class ImageProgressBar : BaseProgressBar
    {
        private Tween _tween;

        private float _startAlpha;

        private void Start()
        {
            _startAlpha = _progressImage.color.a;
        }

        public override void SetProgress(float progress)
        {
            _progressImage.fillAmount = progress;
        }

        public override void Show()
        {
            _tween?.Kill();
            var color = _progressImage.color;
            color.a = _startAlpha;
            _progressImage.color = color;
        }

        public override void Hide()
        {
            _tween = _progressImage.DOFade(0f, 1f)
                .SetLink(gameObject);
        }
    }
}