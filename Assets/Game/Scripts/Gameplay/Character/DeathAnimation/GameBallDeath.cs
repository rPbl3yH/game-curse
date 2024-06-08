using System;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Gameplay
{
    public class GameBallDeath : BaseCharacterDeath
    {
        [SerializeField] private Transform _ball;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private BaseTweenStat _tweenStat;
        [SerializeField] private Character _character;
        [SerializeField] private Transform _visualRoot;
        [SerializeField] private FallingPhysicsComponent _fallingPhysicsComponent;

        private void Start()
        {
            _fallingPhysicsComponent.Deactivate();
            Hide();
        }

        public override void Show()
        {
            _ball.gameObject.SetActive(true);
            _visualRoot.transform.rotation = quaternion.LookRotation(Vector3.back, Vector3.up);

            var randomOffset = new Vector3(Random.Range(-5f, 5f), 0f, 0f);
            var startPosition = _ball.position + randomOffset;
            
            _ball.DOMove(_endPoint.position, _tweenStat.Duration)
                .From(startPosition)
                .SetLink(gameObject)
                .OnComplete(OnCompleted);
        }
        
        private void Hide()
        {
            _ball.gameObject.SetActive(false);
        }

        private void OnCompleted()
        {
            _fallingPhysicsComponent.Activate();
            _fallingPhysicsComponent.Fall();
            _character.OnFinishedHit();
        }
    }
}