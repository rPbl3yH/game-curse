using System;
using UnityEngine;

namespace Game.Scripts
{
    public class CatSpawnPoint : MonoBehaviour
    {
        [SerializeField] private Transform _catSpawnPoint;
        [SerializeField] private Cat _catPrefab;

        private bool _isCatActivated;
        
        private Vector3 GetPosition()
        {
            return transform.position;
        }

        public void TryLaunchCat(Vector3 position)
        {
            if (_isCatActivated)
            {
                return;
            }
            
            var cat = Instantiate(_catPrefab, _catSpawnPoint.position, Quaternion.identity);
            cat.MoveToPosition(position, OnCatFinished);
            _isCatActivated = true;
        }

        private void OnCatFinished()
        {
            _isCatActivated = false;
        }
    }
}
