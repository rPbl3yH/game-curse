using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Elementary.Time
{
    [Serializable]
    public sealed class Timer : ISerializationCallbackReceiver, ITimer
    {
        public event Action OnStarted;
        public event Action OnTimeChanged;
        public event Action OnStopped;
        public event Action OnEnded;
        public event Action OnReset;

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-10)]
        [PropertySpace(8)]
        public bool IsPlaying { get; private set; }

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-9)]
        [ProgressBar(0, 1)]
        public float Progress
        {
            get => 1 - _remainingTime / _duration;
            set => SetProgress(value);
        }

        public float Duration
        {
            get => _duration;
            set => _duration = value;
        }

        [ReadOnly]
        [ShowInInspector]
        [PropertyOrder(-8)]
        public float RemainingTime
        {
            get => _remainingTime;
            set => _remainingTime = Mathf.Clamp(value, 0, _duration);
        }
        
        [Space]
        [SerializeField]
        private float _duration;

        private float _remainingTime;

        public Timer()
        {
        }

        public Timer(float duration)
        {
            _duration = duration;
            _remainingTime = duration;
        }

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
                OnStopped?.Invoke();
            }
        }

        [Button]
        public void ResetTime()
        {
            _remainingTime = _duration;
            OnReset?.Invoke();
        }

        public void OnUpdate(float deltaTime)
        {
            if (!IsPlaying)
            {
                return;
            }

            if (_remainingTime <= 0)
            {
                return;
            }
            
            _remainingTime -= deltaTime;
            OnTimeChanged?.Invoke();

            if (_remainingTime > 0)
            {
                return;
            }
            
            IsPlaying = false;
            OnEnded?.Invoke();
        }

        private void SetProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);
            _remainingTime = _duration * (1 - progress);
            OnTimeChanged?.Invoke();
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            _remainingTime = _duration;
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }
    }
}