using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Scripts
{
    public class Cat : MonoBehaviour
    {
        [SerializeField] private Transform _catModel;
        [SerializeField] private BaseTweenStat _baseTweenStat;

        private bool _isActivated;

        public event Action<float> ProgressChanged;  
        
        public void MoveToPosition(Vector3 position, Action onComplete)
        {
            transform.LookAt(position);

            var tween = _catModel.DOMove(position, _baseTweenStat.Duration)
                .SetLink(_catModel.gameObject)
                .SetEase(_baseTweenStat.Easing)
                .OnComplete(()=>
                {
                    onComplete?.Invoke();
                    OnMoveCompleted();
                });

            tween.OnUpdate(() =>
            {
                ProgressChanged?.Invoke(tween.ElapsedPercentage());
            });
        }

        private void OnMoveCompleted()
        {
            Destroy(gameObject);
        }
    }
}