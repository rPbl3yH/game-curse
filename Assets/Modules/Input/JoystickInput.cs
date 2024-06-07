using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Modules.Input
{
    public sealed class JoystickInput : MonoBehaviour
    {
        private const int MOUSE_BUTTON = 0;
        private const float MIN_MAGNITUDE = 0.05f;

        public event Action<Vector2> PositionStarted;
        
        public event Action<Vector2> PositionMoved;

        public event Action<Vector2> DirectionMoved;

        public event Action<Vector2> PositionEnded;
        
        public event Action Canceled;
        
        private bool _isHoldStarted;
        private bool _isMoveStarted;

        private Vector2 _centerScreenPosition;

        private EventSystem _eventSystem;

        private void Awake()
        {
            _eventSystem = EventSystem.current;
        }

        private void Update()
        {
#if UNITY_EDITOR
            UpdateMouse();
#else
            UpdateTouch();
#endif
        }


#if UNITY_EDITOR
        private void UpdateMouse()
        {
            if (UnityEngine.Input.GetMouseButtonDown(MOUSE_BUTTON) && !IsPointerOverGameObject())
            {
                StartInput(UnityEngine.Input.mousePosition);
            }
            else if (_isHoldStarted && UnityEngine.Input.GetMouseButton(MOUSE_BUTTON))
            {
                ProcessMove(UnityEngine.Input.mousePosition);
            }
            else if (_isHoldStarted && UnityEngine.Input.GetMouseButtonUp(MOUSE_BUTTON))
            {
                EndInput(UnityEngine.Input.mousePosition);
            }
        }
        
        private bool IsPointerOverGameObject()
        {
            if (_eventSystem == null)
            {
                return false;
            }

            return _eventSystem.IsPointerOverGameObject();
        }
#else
        private void UpdateTouch()
        {
            var touchCount = Input.touchCount;
            if (touchCount < 1)
            {
                return;
            }

            var touch = Input.GetTouch(0);
            var touchPhase = touch.phase;
            if (touchPhase == TouchPhase.Began && !IsPointerOverGameObject(touch.fingerId))
            {
                StartInput(touch.position);
            }
            else if (_isHoldStarted)
            {
                ProcessMove(touch.position);
            }
            else if (_isHoldStarted && (touchPhase == TouchPhase.Canceled || touchPhase == TouchPhase.Ended))
            {
                EndInput(touch.position);
            }
        }
        
        private bool IsPointerOverGameObject(int fingerId)
        {
            if (_eventSystem == null)
            {
                return false;
            }

            return _eventSystem.IsPointerOverGameObject(fingerId);
        }
#endif

        private void StartInput(Vector2 inputPosition)
        {
            _isHoldStarted = true;
            _centerScreenPosition = inputPosition;
            PositionStarted?.Invoke(inputPosition);
        }

        private void ProcessMove(Vector2 inputPosition)
        {
            var screenVector = inputPosition - _centerScreenPosition;
            if (_isMoveStarted || screenVector.magnitude > MIN_MAGNITUDE)
            {
                _isMoveStarted = true;
                PositionMoved?.Invoke(inputPosition);
                DirectionMoved?.Invoke(screenVector.normalized);
            }
        }

        private void EndInput(Vector2 inputPosition)
        {
            _isMoveStarted = false;
            _isHoldStarted = false;
            PositionEnded?.Invoke(inputPosition);
        }

        public void CancelInput()
        {
            if (_isHoldStarted)
            {
                _isMoveStarted = false;
                _isHoldStarted = false;
                Canceled?.Invoke();
            }
        }
    }
}