using Modules.GameManagement;
using Modules.Input;
using UnityEngine;

namespace Game.Scripts
{
    public class CharacterInputController : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        [SerializeField] private JoystickInput _joystickInput;
        [SerializeField] private Character _character;

        void IGameStartListener.StartGame()
        {
            _joystickInput.DirectionMoved += OnDirectionMoved;
            _joystickInput.PositionEnded += OnUp;
        }

        void IGameFinishListener.FinishGame()
        {
            _joystickInput.DirectionMoved -= OnDirectionMoved;
            _joystickInput.PositionEnded -= OnUp;
        }
        
        private void OnUp(Vector2 direction)
        {
            _character.Move(Vector3.zero);
        }

        private void OnDirectionMoved(Vector2 direction)
        {
            var characterDirection = new Vector3(direction.x, 0f, direction.y);
            _character.Move(characterDirection);
        }
    }
}