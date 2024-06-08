using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class ThunderboltDeath : BaseCharacterDeath
    {
        [SerializeField] private ParticleSystem _thunderboltSystem;
        [SerializeField] private Character _character;
        
        private GameAudioConfig _gameAudioConfig;
        
        [Inject]
        public void Construct(GameAudioConfig gameAudioConfig)
        {
            _gameAudioConfig = gameAudioConfig;
        }
        
        public override void Show()
        {
            AudioManager.Instance.PlaySound(_gameAudioConfig.ThunderboltClip);
            _thunderboltSystem.Play();
            _character.OnFinishedHit();
        }
    }
}