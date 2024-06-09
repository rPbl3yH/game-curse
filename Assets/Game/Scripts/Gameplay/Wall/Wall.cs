using System;
using Game.Scripts.Gameplay.Wall;
using Modules.BaseUI;
using UnityEngine;

namespace Game.Scripts
{
    public class Wall : MonoBehaviour
    {
        [SerializeField] private RectangleDistanceSensor _sensor;
        [SerializeField] private ItemPoint _startItemPoint;
        [SerializeField] private ItemPoint _endItemPoint;

        [SerializeField] private Cat _catPrefab;
        [SerializeField] private ImageProgressBar _imageProgressBar;

        Cat _cat;
        private bool _isCatActivated;

        private void OnEnable()
        {
            _sensor.TargetEntered += SensorOnTargetEntered;
        }

        private void OnDisable()
        {
            _sensor.TargetEntered -= SensorOnTargetEntered;
        }
        
        private void SensorOnTargetEntered()
        {
            TryLaunchCat(_endItemPoint.GetPosition());
        }

        private void TryLaunchCat(Vector3 position)
        {
            if (_isCatActivated)
                return;

            _cat = Instantiate(_catPrefab, _startItemPoint.GetPosition(), Quaternion.identity);
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