using System;
using UnityEngine;

namespace Game.Scripts
{
    [Serializable]
    public class RotationComponent 
    {
        [SerializeField] private Transform _rotationRoot;
        [SerializeField] private Vector3 _rotateDirection;
        [SerializeField] private float _rotateRate;
        [SerializeField] private bool _canRotate;
        
        public void Rotate(Vector3 forwardDirection)
        {
            _rotateDirection = forwardDirection;

            if (!_canRotate)
            {
                return;
            }

            if (forwardDirection == Vector3.zero)
            {
                return;
            }

            var targetRotation = Quaternion.LookRotation(_rotateDirection, Vector3.up);
            _rotationRoot.rotation = Quaternion.Lerp(_rotationRoot.rotation, targetRotation, _rotateRate);
        }
    }
}