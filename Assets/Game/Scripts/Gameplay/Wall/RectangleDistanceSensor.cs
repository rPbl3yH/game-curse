using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay.Wall
{
    public class RectangleDistanceSensor : MonoBehaviour
    {
        [SerializeField] private Transform _centerPoint;
        [SerializeField] private Vector2 _rectangleSize; 

        [ShowInInspector, ReadOnly]
        private bool _isInRectangle; 

        private Transform _targetTransform;

        public event Action TargetEntered; 
        public event Action TargetExited;

        [Inject]
        public void Construct(CharacterService characterService)
        {
            _targetTransform = characterService.GetCharacter().transform;
        }

        private void Update()
        {
            CheckPositionInRectangle();
        }

        private void CheckPositionInRectangle()
        {
            var xCheck = 
                Mathf.Abs(_targetTransform.position.x - _centerPoint.position.x) / (_rectangleSize.x / 2f);
            var zCheck = 
                Mathf.Abs(_targetTransform.position.z - _centerPoint.position.z) / (_rectangleSize.y / 2f);

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
        
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Handles.color = Color.black;
            var size = new Vector3(_rectangleSize.x, 0.01f, _rectangleSize.y);
            Handles.DrawWireCube(_centerPoint.position, size);
        }
#endif
    }
}