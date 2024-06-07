using UnityEngine;

namespace Game.Scripts
{
    public class TriggerListener
    {
        private readonly GameController _gameController;

        public TriggerListener(GameController gameController)
        {
            _gameController = gameController;
        }

        public void OnTrigger(Collider collider)
        {
            _gameController.LoseGame();
        }
    }
}