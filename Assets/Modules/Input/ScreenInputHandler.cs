using System;
using UnityEngine;

namespace Modules.Input
{
    [Serializable]
    public class ScreenInputHandler : MonoBehaviour, IScreenInputHandler
    {
        public event Action<Vector2> Up;
        public event Action<Vector2> Down;
        public event Action<Vector2> Drag;
        public event Action Cancelled;
        
        [SerializeField] private RectTransform _inputField;

        private Vector3 _lastMousePosition;
        private bool _isActive = true;

        public void Activate()
        {
            _isActive = true;
        }

        public void Deactivate()
        {
            _isActive = false;
        }

        private void Update()
        {
            if (!_isActive)
            {
                return;
            }
            
            GetInput();
        }

        private void GetInput()
        {
            var mousePosition = UnityEngine.Input.mousePosition;

            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                OnDown(mousePosition);
            }
            
            if (UnityEngine.Input.GetMouseButton(0))
            {
                if (_lastMousePosition != mousePosition)
                {
                    OnDrag(mousePosition);
                }
            }

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                OnUp(mousePosition);
            }
        
            _lastMousePosition = mousePosition;
        }

        private void OnDown(Vector3 position)
        {
            if (IsInsideField(position))
            {
                Down?.Invoke(position);
            }
        }

        private void OnDrag(Vector3 mousePosition)
        {
            if (IsInsideField(mousePosition))
            {
                Drag?.Invoke(mousePosition);
            }
            else
            {
                Cancelled?.Invoke();
            }
        }

        private void OnUp(Vector3 mousePosition)
        {
            if (IsInsideField(mousePosition))
            {
                Up?.Invoke(mousePosition);
            }
        }

        private bool IsInsideField(Vector3 position)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _inputField, position, null, out var localPoint);

            return _inputField.rect.Contains(localPoint);
        }
    }
}