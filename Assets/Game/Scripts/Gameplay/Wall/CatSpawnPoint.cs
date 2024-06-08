using DG.Tweening;
using System;
using UnityEngine;

namespace Game.Scripts
{
    public class CatSpawnPoint : MonoBehaviour
    {
        [SerializeField] private Transform _catSpawnPoint;
        [SerializeField] private Transform _curseTrail;
        [SerializeField] private Cat _catPrefab;

        Cat cat;
        private bool _isCatActivated;
        
        private Vector3 GetPosition()
        {
            return transform.position;
        }

        public void TryLaunchCat(Vector3 position)
        {
            if (_isCatActivated)
                return;

            cat = Instantiate(_catPrefab, _catSpawnPoint.position, Quaternion.identity);
            cat.MoveToPosition(position, OnCatFinished);
            _isCatActivated = true;

            _curseTrail.DOComplete();
            _curseTrail.position = position;
            //_curseTrail.GetComponent<Renderer>()?.material.DOFade(1, 3);
        }

        private void OnCatFinished()
        {
            _isCatActivated = false;

            _curseTrail.DOScaleY(0, 3);
            _curseTrail.DOLocalMoveZ(5f, 3);
            //_curseTrail.GetComponent<Renderer>()?.material.DOFade(0, 1);
        }

        private void Update()
        {
            if (!_isCatActivated)
                return;

            var dist = Vector3.Distance(_catSpawnPoint.position, cat.transform.position)-1.5f;
            if (dist < 0)
                dist = 0;
            _curseTrail.localScale =
                new Vector3(
                    _curseTrail.localScale.x,
                    dist/2,
                    _curseTrail.localScale.z
                );

            _curseTrail.localPosition =
                new Vector3(
                    _curseTrail.localPosition.x,
                    _curseTrail.localPosition.y,
                    dist/2
                );
        }
    }
}
