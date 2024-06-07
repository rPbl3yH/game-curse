using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Elementary.Time
{
    [AddComponentMenu("Elementary/Time/MonoStopwatch")]
    public sealed class MonoStopwatch : MonoBehaviour, IStopwatch
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

        private Coroutine _coroutine;
        
        [Button]
        public void Play()
        {
            if (IsPlaying)
            {
                return;
            }

            IsPlaying = true;
            OnStarted?.Invoke();
            _coroutine = StartCoroutine(TimerRoutine());
        }

        [Button]
        public void Stop()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

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

        private IEnumerator TimerRoutine()
        {
            while (true)
            {
                yield return null;
                _currentTime += UnityEngine.Time.deltaTime;
                OnTimeChanged?.Invoke();
            }
        }
    }
}