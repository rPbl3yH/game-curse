using Modules.BaseUI;
using UnityEngine;

namespace Game.Scripts
{
    public class CatSpawnPoint : MonoBehaviour
    {
        [SerializeField] private Transform _catSpawnPoint;
        [SerializeField] private Transform _curseTrail;
        [SerializeField] private Cat _catPrefab;
        [SerializeField] private ImageProgressBar _imageProgressBar;
        [SerializeField] private BaseTweenStat _tweenStat;

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
            cat.ProgressChanged += OnProgressChanged;
            cat.MoveToPosition(position, OnCatFinished);
            _imageProgressBar.Show();
            _isCatActivated = true;

            _curseTrail.position = position;
        }

        private void OnProgressChanged(float progress)
        {
            _imageProgressBar.SetProgress(progress);
        }

        private void OnCatFinished()
        {
            _isCatActivated = false;
            cat.ProgressChanged -= OnProgressChanged;
            _imageProgressBar.Hide();
        }

        private void Update()
        {
            if (!_isCatActivated)
                return;
        }
    }
}
