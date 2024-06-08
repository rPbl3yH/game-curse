using Game;
using Game.Scripts.Gameplay.Character;
using UnityEngine;
using Zenject;

namespace Lessons.Lesson_SectionAndVisuals
{
    public class CharacterAudioController : MonoBehaviour
    {
        [SerializeField] private AnimatorDispatcher _animatorDispatcher;

        private GameAudioConfig _gameAudioConfig;
        
        [Inject]
        public void Construct(GameAudioConfig gameAudioConfig)
        {
            _gameAudioConfig = gameAudioConfig;
        }

        private void OnEnable()
        {
            _animatorDispatcher.SubscribeOnEvent("step", OnStep);
            
        }

        private void OnStep()
        {
            AudioManager.Instance.PlaySound(_gameAudioConfig.Step);
        }
    }
}