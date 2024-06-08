using UnityEngine;

namespace Game.Scripts.Gameplay
{
    public class ThunderboltDeath : BaseCharacterDeath
    {
        [SerializeField] private ParticleSystem _thunderboltSystem;
        [SerializeField] private Character _character;
        
        public override void Show()
        {
            _thunderboltSystem.Play();
            _character.OnFinishedHit();
        }
    }
}