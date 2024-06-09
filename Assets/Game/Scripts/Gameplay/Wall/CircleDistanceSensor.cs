using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts
{
    public class CircleDistanceSensor : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;
        [SerializeField]
        private float _radius = 5f; 
        [ShowInInspector, ReadOnly]
        private bool _isInsideRadius = false; 
        
        public event Action TargetEntered; 
        public event Action TargetExited; 
    
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
                TargetEntered?.Invoke();
                _isInsideRadius = true;
            }
            else if (_isInsideRadius && distanceToTarget > _radius)
            {
                TargetExited?.Invoke();
                _isInsideRadius = false;
            }
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = Color.black;
            Handles.DrawWireDisc(transform.position, Vector3.up, _radius);
        }
#endif
    }
}

