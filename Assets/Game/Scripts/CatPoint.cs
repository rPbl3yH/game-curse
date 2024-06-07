using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts
{
    public class CatPoint : MonoBehaviour
    {
        [SerializeField] private List<CatPoint> _catPoints;
        [SerializeField] private Character _character;
        [SerializeField] private float _radius =2f;
        [SerializeField] private Cat _catPrefab;

        private bool _isActivated;

        private void Update()
        {
            var distance = Vector3.Distance(_character.GetPosition(),transform.position);
            
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
            foreach (var catPoint in _catPoints)
            {
                LaunchCat(catPoint);
            }

            _isActivated = true;
        }

        private Vector3 GetPosition()
        {
            return transform.position;
        }

        private void LaunchCat(CatPoint catPoint)
        {
            var cat = Instantiate(_catPrefab, transform.position, Quaternion.identity);
            cat.MoveToPosition(catPoint.GetPosition());
        }

        private void OnDrawGizmosSelected()
        {
            Handles.DrawWireDisc(transform.position, Vector3.up, _radius);
        }
    }
}
