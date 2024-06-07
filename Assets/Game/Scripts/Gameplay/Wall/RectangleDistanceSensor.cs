using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Gameplay.Wall
{
    public class RectangleDistanceSensor : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform; 
        [SerializeField] private Vector2 _rectangleSize; 

        [ShowInInspector, ReadOnly]
        private bool _isInRectangle = false; 

        public event Action TargetEntered; 
        public event Action TargetExited; 

        void Update()
        {
            CheckPositionInRectangle();
        }

        void CheckPositionInRectangle()
        {
            float xCheck = Mathf.Abs(_targetTransform.position.x - transform.position.x) / _rectangleSize.x;
            float yCheck = Mathf.Abs(_targetTransform.position.y - transform.position.y) / _rectangleSize.y;

            if ((xCheck > 1 || yCheck > 1) && _isInRectangle)
            {
                TargetExited?.Invoke();
                _isInRectangle = false;
            }
            else if (xCheck <= 1 && yCheck <= 1 && _isInRectangle == false)
            {
                TargetEntered?.Invoke();
                _isInRectangle = true;
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            Handles.color = Color.black;
            var size = new Vector3(_rectangleSize.x, 0.01f, _rectangleSize.y);
            Handles.DrawWireCube(transform.position, size);
        }
    }
}