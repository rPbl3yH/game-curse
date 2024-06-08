using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Create GameAudioConfig", fileName = "GameAudioConfig", order = 0)]
    public class GameAudioConfig : ScriptableObject
    {
        public AudioClip Step;
        public AudioClip CatVoice;

        [Header("Level")]
        public AudioClip LevelStarted;
        public AudioClip LevelWin;
        public AudioClip PlayerDeathLose;
        public AudioClip LevelLose;

        [Header("DeathEffect")]
        public AudioClip BallFallClip;
        public AudioClip ThunderboltClip;
        public AudioClip BrickFallClip;
    }
}