using System;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts
{
    public class Character : MonoBehaviour
    {
        [ShowInInspector] public Vector3 Velocity => _characterController.velocity;
        
        [SerializeField] private UnityEngine.CharacterController _characterController;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private CharacterDeathComponent _deathComponent;
        
        public RotationComponent RotationComponent;

        private Vector3 _direction;
        
        [ShowInInspector, ReadOnly]
        private bool _isDead;

        private int _deathIndex;

        public event Action<Vector3> VelocityChanged;
        public event Action StopRequest;
        public event Action<int> DeathEvent;
        
        public void Move(Vector3 direction)
        {
            if (_isDead)
            {
                return;
            }
            
            _characterController.Move(direction * _speed * Time.deltaTime);
            VelocityChanged?.Invoke(_characterController.velocity);
            
            RotationComponent.Rotate(direction);
        }

        [Button]
        public void Death()
        {
            _isDead = true;
            StopRequest?.Invoke();
            _deathComponent.StartDeath(out _deathIndex);
            print($"Death index = {_deathIndex}");
        }

        public void Stop()
        {
            StopRequest?.Invoke();
        }

        public void OnFinishedHit()
        {
            DeathEvent?.Invoke(_deathIndex);
        }
    }
}
