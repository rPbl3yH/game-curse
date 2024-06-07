using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts
{
    public class CatPoint : MonoBehaviour
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _finishPoint;
        
        [SerializeField] private Character _character;
        [SerializeField] private float _radius =2f;
        [SerializeField] private Cat _catPrefab;

        private bool _isActivated;

        private void Update()
        {
            var distance = Vector3.Distance(_character.GetPosition(),_startPoint.position);
            
            if (distance <= _radius && !_isActivated)
            {
                Activate();
                Debug.Log("Activate");
            }

            if (distance > _radius && _isActivated)
            {
                _isActivated = false;
                Debug.Log("Deactivate");
            }
        }

        public void Activate()
        {
            LaunchCat(_finishPoint.position);
            _isActivated = true;
        }

        private Vector3 GetPosition()
        {
            return transform.position;
        }

        private void LaunchCat(Vector3 position)
        {
            var cat = Instantiate(_catPrefab, _startPoint.position, Quaternion.identity);
            cat.MoveToPosition(position);
        }

        private void OnDrawGizmosSelected()
        {
            Handles.DrawWireDisc(_startPoint.position, Vector3.up, _radius);
        }
    }
}
