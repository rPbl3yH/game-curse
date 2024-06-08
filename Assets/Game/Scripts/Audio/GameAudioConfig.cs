using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Create GameAudioConfig", fileName = "GameAudioConfig", order = 0)]
    public class GameAudioConfig : ScriptableObject
    {
        public AudioClip Step;
        public AudioClip Step2;
    }
}