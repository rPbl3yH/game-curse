using System;
using Animancer;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts
{
    public class CharacterAnimatorController : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private AnimancerComponent _animancerComponent;
        
        private static readonly object StandingDeath = "StandingDeath";
        private static readonly object HappyIdle = "HappyIdle";
        private static readonly object Walking = "Walking";

        private void OnEnable()
        {
            _character.VelocityChanged += OnVelocityChanged;
            _character.DeathEvent += CharacterOnDeathEvent;
        }

        private void CharacterOnDeathEvent()
        {
            _animancerComponent.Play(StandingDeath);
        }

        private void OnDisable()
        {
            _character.VelocityChanged -= OnVelocityChanged;
        }

        private void OnVelocityChanged(Vector3 velocity)
        {
            var magnitude = velocity.magnitude;
            if (magnitude < Mathf.Epsilon)
            {
                _animancerComponent.Play(HappyIdle, 0.2f);
            }
            else
            {
                _animancerComponent.Play(Walking, 0.2f);
            }
        }
    }
}