using UnityEngine;

namespace Game.Scripts
{
    public class CharacterService : MonoBehaviour
    {
        [SerializeField] private Character _character;
        
        public Character GetCharacter()
        {
            return _character;
        }
    }
}