using System;
using DG.Tweening;
using Game.Scripts.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    public class BrickDeath : BaseCharacterDeath
    {
        [SerializeField] private Character _character;
        [SerializeField] private Transform _finishPoint;
        [SerializeField] private BaseTweenStat _tweenStat;
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private float _force = 10f;
        [SerializeField] private float _torgue = 10f;

        private void Start()
        {
            _rigidbody.isKinematic = true;
            Hide();
        }

        [Button]
        public override void Show()
        {
            gameObject.SetActive(true);
            transform.DOMove(_finishPoint.position, _tweenStat.Duration)
                .SetLink(gameObject)
                .From(transform.position)
                .OnComplete(OnFinished);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnFinished()
        {
            _rigidbody.isKinematic = false;
            var randomVector = Vector3Utils.GetRandomVector3(0f, 1f);
            randomVector.y = 1f;
            _rigidbody.AddRelativeForce(randomVector * _force);
            _rigidbody.AddRelativeTorque(randomVector * _torgue);

            _character.OnFinishedHit();
        }
    }
}
