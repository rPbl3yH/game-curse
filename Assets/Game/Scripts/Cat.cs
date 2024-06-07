using System;
using UnityEngine;

namespace Game.Scripts
{
    public class Cat : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;

        private bool _isActivated;
        private Vector3 _targetPosition;
        private Action _action;
        
        public void MoveToPosition(Vector3 position, Action onComplete)
        {
            _targetPosition = position;
            _isActivated = true;
            _action = onComplete;
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
                _action?.Invoke();
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