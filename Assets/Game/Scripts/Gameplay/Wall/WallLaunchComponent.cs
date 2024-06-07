using Game.Scripts.Gameplay.Wall;
using UnityEngine;

namespace Game.Scripts
{
    public class WallLaunchComponent : MonoBehaviour
    {
        [SerializeField] private RectangleDistanceSensor _sensor;
        [SerializeField] private CatSpawnPoint _catSpawnPoint;

        [SerializeField] private Transform _finishPoint;

        private void OnEnable()
        {
            _sensor.TargetEntered += SensorOnTargetEntered;
            _sensor.TargetExited += SensorOnTargetExited;
        }

        private void OnDisable()
        {
            _sensor.TargetEntered -= SensorOnTargetEntered;
            _sensor.TargetExited -= SensorOnTargetExited;
        }

        private void SensorOnTargetExited()
        {
            
        }

        private void SensorOnTargetEntered()
        {
            _catSpawnPoint.TryLaunchCat(_finishPoint.position);
        }
    }
}