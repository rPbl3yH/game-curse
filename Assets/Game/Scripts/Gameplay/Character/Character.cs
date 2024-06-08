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

        public event Action<Vector3> VelocityChanged;
        public event Action DeathRequest;
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

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public Vector3 GetVelocity()
        {
            return _characterController.velocity;
        }

        public void Death()
        {
            _deathComponent.StartDeath();
            _isDead = true;
            DeathRequest?.Invoke();
        }

        public void OnBrickHit()
        {
            DeathEvent?.Invoke(0);
        }
    }
}
