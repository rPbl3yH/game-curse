using Modules.Input;
using UnityEngine;

namespace Game.Scripts
{
    public class CharacterInputController : MonoBehaviour
    {
        [SerializeField] private JoystickInput _joystickInput;
        [SerializeField] private Character _character;
        
        private void OnEnable()
        {
            _joystickInput.DirectionMoved += JoystickInputOnDirectionMoved;
            _joystickInput.PositionEnded += JoystickInputOnCanceled;
        }

        private void OnDisable()
        {
            _joystickInput.DirectionMoved -= JoystickInputOnDirectionMoved;
            _joystickInput.PositionEnded -= JoystickInputOnCanceled;
        }

        private void JoystickInputOnCanceled(Vector2 direction)
        {
            _character.Move(Vector3.zero);
        }

        private void JoystickInputOnDirectionMoved(Vector2 direction)
        {
            var characterDirection = new Vector3(direction.x, 0f, direction.y);
            _character.Move(characterDirection);
        }
    }
}