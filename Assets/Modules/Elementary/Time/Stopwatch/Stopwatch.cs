using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Elementary.Time
{
    [Serializable]
    public sealed class Stopwatch : IStopwatch
    {
        public event Action OnStarted;

        public event Action OnTimeChanged;

        public event Action OnCanceled;

        public event Action OnReset;

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-10)]
        [PropertySpace(8)]
        public bool IsPlaying { get; private set; }

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-8)]
        public float CurrentTime
        {
            get => _currentTime;
            set => _currentTime = Mathf.Max(value, 0);
        }

        private float _currentTime;

        [Button]
        public void Play()
        {
            if (IsPlaying)
            {
                return;
            }

            IsPlaying = true;
            OnStarted?.Invoke();
        }

        [Button]
        public void Stop()
        {
            if (IsPlaying)
            {
                IsPlaying = false;
                OnCanceled?.Invoke();
            }
        }

        [Button]
        public void ResetTime()
        {
            _currentTime = 0;
            OnReset?.Invoke();
        }

        public void OnUpdate(float deltaTime)
        {
            if (!IsPlaying)
            {
                return;
            }
            
            _currentTime += deltaTime;
            OnTimeChanged?.Invoke();
        }
    }
}