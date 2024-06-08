using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    public class GameBallDeath : BaseCharacterDeath
    {
        [SerializeField] private Transform _ball;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private BaseTweenStat _tweenStat;
        [SerializeField] private Character _character;
        
        public override void Show()
        {
            _ball.DOMove(_endPoint.position, _tweenStat.Duration)
                .SetLink(gameObject)
                .OnComplete(()=> _character.OnFinishedHit());
        }
    }
}