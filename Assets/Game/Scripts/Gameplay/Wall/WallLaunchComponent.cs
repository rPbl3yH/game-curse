using DG.Tweening;
using Game.Scripts.Gameplay.Wall;
using UnityEngine;

namespace Game.Scripts
{
    public class WallLaunchComponent : MonoBehaviour
    {
        [SerializeField] private RectangleDistanceSensor _sensor;
        [SerializeField] private CatSpawnPoint _catSpawnPoint;

        [SerializeField] private Transform _finishPoint;

        [SerializeField] private GameObject[] boxVariant;

        private void Start()
        {
            var box = Instantiate(
                boxVariant[Random.Range(0, boxVariant.Length)],
                _catSpawnPoint.transform.position,
                Quaternion.Euler(Vector3.zero)
            );

            box.transform.DOScale(Vector3.one, 1);
            box.transform.Rotate(0, Random.Range(0, 360), 0);
            box.transform.SetParent(_catSpawnPoint.transform);

            box = Instantiate(
                boxVariant[Random.Range(0, boxVariant.Length)],
                _finishPoint.position,
                Quaternion.Euler(Vector3.zero)
            );

            box.transform.DOScale(Vector3.one, 1);
            box.transform.Rotate(0, Random.Range(0, 360), 0);
            box.transform.SetParent(_finishPoint);
        }

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