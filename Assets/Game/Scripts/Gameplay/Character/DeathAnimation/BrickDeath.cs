using System;
using DG.Tweening;
using Game.Scripts.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class BrickDeath : BaseCharacterDeath
    {
        [SerializeField] private Character _character;
        [SerializeField] private Transform _finishPoint;
        [SerializeField] private BaseTweenStat _tweenStat;
        [SerializeField] private FallingPhysicsComponent _fallingPhysicsComponent;

        private GameAudioConfig _gameAudioConfig;
        
        [Inject]
        public void Construct(GameAudioConfig gameAudioConfig)
        {
            _gameAudioConfig = gameAudioConfig;
        }
        
        private void Start()
        {
            _fallingPhysicsComponent.Deactivate();
            Hide();
        }

        [Button]
        public override void Show()
        {
            gameObject.SetActive(true);
            AudioManager.Instance.PlaySound(_gameAudioConfig.BrickFallClip);
            
            transform.DOMove(_finishPoint.position, _tweenStat.Duration)
                .SetLink(gameObject)
                .From(transform.position)
                .OnComplete(OnFinished);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnFinished()
        {
            _fallingPhysicsComponent.Activate();
            _fallingPhysicsComponent.Fall();
            _character.OnFinishedHit();
        }
    }
}
