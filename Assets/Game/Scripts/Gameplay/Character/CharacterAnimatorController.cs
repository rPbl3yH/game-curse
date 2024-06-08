using System;
using Animancer;
using Modules.BaseUI;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class CharacterAnimatorController : MonoBehaviour
    {
        [Inject] private MenuService _menuService;
        [SerializeField] private Character _character;
        [SerializeField] private AnimancerComponent _animancerComponent;
        
        private static readonly string StandingDeath = "StandingDeath";
        private static readonly string ShockedDeath = "ShockedDeath";
        private static readonly string FlyingDeath = "FlyingDeath";
        private static readonly string HappyIdle = "HappyIdle";
        private static readonly string Walking = "Walking";

        private readonly string[] _deathKeys = {
            StandingDeath,
            ShockedDeath,
            FlyingDeath,
        };
        
        private AnimancerState _state;

        private void OnEnable()
        {
            _character.VelocityChanged += OnVelocityChanged;
            _character.DeathEvent += OnDeathEvent;
            _character.DeathRequest += OnDeathRequest;
        }

        private void OnDisable()
        {
            _character.VelocityChanged -= OnVelocityChanged;
            _character.DeathEvent -= OnDeathEvent;
            _character.DeathRequest -= OnDeathRequest;
        }

        private void OnDeathRequest()
        {
            _animancerComponent.Play(HappyIdle, 0.2f);
        }

        private void OnDeathEvent(int index)
        {
            var key = _deathKeys[index];
            
            _state = _animancerComponent.Play(key);
            _state.Events.endEvent.callback += Callback;
        }

        private void Callback()
        {
            _state.Events.endEvent.callback -= Callback;
            _menuService.ShowMenu(MenuType.Lose);
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