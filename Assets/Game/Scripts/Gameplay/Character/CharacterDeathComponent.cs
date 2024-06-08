using Game.Scripts.Gameplay;
using UnityEngine;

namespace Game.Scripts
{
    public class CharacterDeathComponent : MonoBehaviour
    {
        [SerializeField] private BrickDeath _brickDeath;

        public void StartDeath()
        {
            _brickDeath.Show();
        }
    }
}