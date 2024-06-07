using System;
using UnityEngine;

namespace Game.Scripts
{
    public class Cat : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;

        private bool _isActivated;
        private Vector3 _targetPosition;
        
        public void MoveToPosition(Vector3 position)
        {
            _targetPosition = position;
            _isActivated = true;
        }

        private void Update()
        {
            if (!_isActivated)
            {
                return;
            }
            
            var distance = Vector3.Distance(transform.position, _targetPosition);
            if (distance < Mathf.Epsilon)
            {
                Destroy(gameObject);
                return;
            }

            var forward = (_targetPosition - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
            transform.position = Vector3.MoveTowards(
                transform.position, _targetPosition, _speed * Time.deltaTime);
        }
    }
}