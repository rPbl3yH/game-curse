using UnityEngine;

namespace Game.Scripts
{
    public class TriggerListener
    {
        private readonly LevelController _levelController;

        public TriggerListener(LevelController levelController)
        {
            _levelController = levelController;
        }

        public void OnTrigger(Collider collider)
        {
            if (collider.GetComponent<Character>())
            {
                _levelController.LoseGame();
            }
        }
    }
}