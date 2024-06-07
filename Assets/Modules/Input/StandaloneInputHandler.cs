using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Modules.Input
{
    [Serializable]
    public class StandaloneInputHandler : MonoBehaviour, IInputHandler
    {
        public event Action<Vector2> Up;
        public event Action<Vector2> Down;
        
        public event Action<Vector2> Drag;
        public event Action<Vector3> DeltaDrag; 
        
        private Vector3 _lastMousePosition = Vector3.zero;
        private EventSystem _eventSystem;
        private bool _isEnabled = true;

        private void Awake()
        {
            _eventSystem = EventSystem.current;
        }

        public void Activate()
        {
            _isEnabled = true;
        }

        public void Deactivate()
        {
            _isEnabled = false;
        }

        void Update()
        {
            if (!_isEnabled)
            {
                return;
            }

            if (_eventSystem.IsPointerOverGameObject())
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
                Down?.Invoke(mousePosition);
            }
            
            if (UnityEngine.Input.GetMouseButton(0))
            {
                if (_lastMousePosition != mousePosition)
                {
                    Drag?.Invoke(mousePosition);

                    var direction = mousePosition - _lastMousePosition;
                    DeltaDrag?.Invoke(direction);
                }
            }

            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                Up?.Invoke(mousePosition);
            }
        
            _lastMousePosition = mousePosition;
        }
    }
}