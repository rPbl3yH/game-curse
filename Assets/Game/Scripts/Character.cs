using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts
{
    public class Character : MonoBehaviour
    {
        [ShowInInspector] public Vector3 Velocity => _characterController.velocity;
        
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _speed = 3f;

        public RotationComponent RotationComponent;

        private Vector3 _direction;
        
        public void Move(Vector3 direction)
        {
            _characterController.Move(direction * _speed * Time.deltaTime);
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

        public void Stop()
        {
            _characterController.Move(Vector3.zero);
        }
    }
}
