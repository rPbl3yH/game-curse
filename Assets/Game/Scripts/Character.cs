using System;
using UnityEngine;

namespace Game.Scripts
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _speed = 3f;

        private Vector3 _direction;
        
        public void Move(Vector3 direction)
        {
            _characterController.Move(direction * _speed * Time.deltaTime);
        }
    }
}
