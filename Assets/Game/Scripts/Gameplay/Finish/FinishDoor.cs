using Game.Scripts.Utils;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class FinishDoor : MonoBehaviour
    {
        [SerializeField] private TriggerSensor _triggerSensor;

        private LevelController _levelController;

        [Inject]
        public void Construct(LevelController levelController)
        {
            _levelController = levelController;
        }
        
        private void OnEnable()
        {
            _triggerSensor.ObjectEntered += OnObjectEntered;
        }

        private void OnObjectEntered(Collider collider)
        {
            if (collider.TryGetComponent(out Character _))
            {
                _levelController.WinGame();
            }
        }
    }
}