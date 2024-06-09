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
        [Inject] private GameAudioConfig _gameAudioConfig;
        [SerializeField] private Character _character;
        [SerializeField] private AnimancerComponent _animancerComponent;
        
        private static readonly object StandingDeath = "StandingDeath";
        private static readonly object ShockedDeath = "ShockedDeath";
        private static readonly object FlyingDeath = "FlyingDeath";
        private static readonly object HappyIdle = "HappyIdle";
        private static readonly object Walking = "Walking";

        private readonly object[] _deathKeys = {
            StandingDeath,
            ShockedDeath,
            FlyingDeath,
        };
        
        private AnimancerState _state;

        private void OnEnable()
        {
            _character.VelocityChanged += OnVelocityChanged;
            _character.DeathEvent += OnDeathEvent;
            _character.StopRequest += OnStopRequest;
        }

        private void OnDisable()
        {
            _character.VelocityChanged -= OnVelocityChanged;
            _character.DeathEvent -= OnDeathEvent;
            _character.StopRequest -= OnStopRequest;
        }

        private void OnStopRequest()
        {
            _animancerComponent.Play(HappyIdle, 0.2f);
        }

        private void OnDeathEvent(int index)
        {
            print($"Death event index = {index}");
            var key = _deathKeys[index];
            
            _state = _animancerComponent.Play(key);
            _state.Events.endEvent.callback += OnDeathEventEnded;
        }

        private void OnDeathEventEnded()
        {
            _state.Events.endEvent.callback -= OnDeathEventEnded;
            AudioManager.Instance.PlaySound(_gameAudioConfig.LevelLose);
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