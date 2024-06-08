using Modules.BaseUI;
using UnityEngine;

namespace Game.Scripts
{
    public class CatSpawnPoint : MonoBehaviour
    {
        [SerializeField] private Transform _catSpawnPoint;
        [SerializeField] private Cat _catPrefab;
        [SerializeField] private ImageProgressBar _imageProgressBar;

        Cat _cat;
        private bool _isCatActivated;

        private Vector3 GetPosition()
        {
            return transform.position;
        }

        public void TryLaunchCat(Vector3 position)
        {
            if (_isCatActivated)
                return;

            _cat = Instantiate(_catPrefab, _catSpawnPoint.position, Quaternion.identity);
            _cat.ProgressChanged += OnProgressChanged;
            _cat.MoveToPosition(position, OnCatFinished);
            
            _imageProgressBar.Show();
            _isCatActivated = true;
        }

        private void OnProgressChanged(float progress)
        {
            _imageProgressBar.SetProgress(progress);
        }

        private void OnCatFinished()
        {
            _isCatActivated = false;
            _cat.ProgressChanged -= OnProgressChanged;
            _imageProgressBar.Hide();
        }
    }
}
