using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts
{
    public class DistanceSensor : MonoBehaviour
    {
        public event Action ObjectEntered; 
        public event Action ObjectExited; 
    
        [SerializeField]
        private Transform _target;
        [SerializeField]
        private float _radius = 5f; 
        [ShowInInspector, ReadOnly]
        private bool _isInsideRadius = false; 
    
        void Update()
        {
            CheckPosition();
        }

        void CheckPosition()
        {
            Vector3 directionToTarget = transform.position - _target.position;
            float distanceToTarget = directionToTarget.magnitude;

            if (!_isInsideRadius && distanceToTarget <= _radius)
            {
                ObjectEntered?.Invoke();
                _isInsideRadius = true;
            }
            else if (_isInsideRadius && distanceToTarget > _radius)
            {
                ObjectExited?.Invoke();
                _isInsideRadius = false;
            }
        }
        
        
        private void OnDrawGizmosSelected()
        {
            Handles.color = Color.black;
            Handles.DrawWireDisc(transform.position, Vector3.up, _radius);
        }
    }
}