using System;
using UnityEngine;

namespace Game.Scripts
{
    public class CatPoint : MonoBehaviour
    {
        [SerializeField] private CatPoint _catPoint;
        [SerializeField] private Transform _rootPosition;
        
        [SerializeField] private Cat _catPrefab;
        [SerializeField] private DistanceSensor _distanceSensor;

        private bool _isCatActivated;

        private void OnEnable()
        {
            _distanceSensor.ObjectEntered += OnObjectEntered;
        }

        private void OnDisable()
        {
            _distanceSensor.ObjectExited -= OnObjectEntered;
        }

        private void OnObjectEntered()
        {
            if (_isCatActivated)
            {
                return;
            }
            
            LaunchCat(_catPoint.GetPosition());
        }
        
        private Vector3 GetPosition()
        {
            return transform.position;
        }

        private void LaunchCat(Vector3 position)
        {
            var cat = Instantiate(_catPrefab, _rootPosition.position, Quaternion.identity);
            cat.MoveToPosition(position, OnCatFinished);
            _isCatActivated = true;
        }

        private void OnCatFinished()
        {
            _isCatActivated = false;
        }
    }
}
