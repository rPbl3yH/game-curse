using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Gameplay.Wall
{
    public class RectangleDistanceSensor : MonoBehaviour
    {
        [SerializeField] private Transform _centerPoint;
        [SerializeField] private Transform _targetTransform; 
        [SerializeField] private Vector2 _rectangleSize; 

        [ShowInInspector, ReadOnly]
        private bool _isInRectangle; 

        public event Action TargetEntered; 
        public event Action TargetExited; 

        void Update()
        {
            CheckPositionInRectangle();
        }

        void CheckPositionInRectangle()
        {
            float xCheck = Mathf.Abs(_targetTransform.position.x - _centerPoint.position.x) / _rectangleSize.x;
            float zCheck = Mathf.Abs(_targetTransform.position.z - _centerPoint.position.z) / _rectangleSize.y;

            if ((xCheck > 1 || zCheck > 1) && _isInRectangle)
            {
                TargetExited?.Invoke();
                _isInRectangle = false;
            }
            else if (xCheck <= 1 && zCheck <= 1 && !_isInRectangle)
            {
                TargetEntered?.Invoke();
                _isInRectangle = true;
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            Handles.color = Color.black;
            var size = new Vector3(_rectangleSize.x, 0.01f, _rectangleSize.y);
            Handles.DrawWireCube(_centerPoint.position, size);
        }
    }
}