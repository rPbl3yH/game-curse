using UnityEngine;

namespace Lessons.Lesson_SectionAndVisuals
{
    public class CharacterAudioController : MonoBehaviour
    {
        [SerializeField] private AnimatorDispatcher _animatorDispatcher;

        private void OnEnable()
        {
            _animatorDispatcher.SubscribeOnEvent("step", OnStep);
        }

        private void OnStep()
        {
            Debug.Log("Step");
        }
    }
}