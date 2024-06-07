using System;
using UnityEngine;

namespace Game.Scripts
{
    public class CameraFollower: MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private Transform _target;
        [SerializeField] private float _followRate;

        private void LateUpdate()
        {
            _camera.position = Vector3.Lerp(_camera.position, _target.position, _followRate);
        }
    }
}