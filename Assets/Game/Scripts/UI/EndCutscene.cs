using Game.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI
{
    public class EndCutscene : MonoBehaviour
    {
        private LevelController _levelController;

        // Start is called before the first frame update

        [Inject]
        public void Construct(LevelController levelController)
        {
            _levelController = levelController;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AgainGame()
        {
            _levelController.LoadLevel(0);
        }

        public void ExitTheGame() => Application.Quit();
    }
}