using Game.Scripts.Gameplay;
using UnityEngine;

namespace Game.Scripts
{
    public class CharacterDeathComponent : MonoBehaviour
    {
        [SerializeField] private BaseCharacterDeath[] _characterDeaths;

        [SerializeField] private int _debugAnimationIndex = -1;

        public void StartDeath(out int deathIndex)
        {
            var index = Random.Range(0, _characterDeaths.Length);
            
            if (_debugAnimationIndex != -1)
            {
                index = _debugAnimationIndex;
            }

            deathIndex = index;
            _characterDeaths[index].Show();
        }
    }
}