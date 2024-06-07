using System;
using UnityEngine;

namespace Game
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("Sound")]
        [SerializeField] private AudioSource _sfxSource;
        [Range(0f, 1f)]
        [SerializeField] private float _soundVolume;
        
        public float SoundVolume => _soundVolume;

        [Header("Music")]
        [SerializeField] private AudioSource _musicSource;
        [Range(0f, 1f)]
        [SerializeField] private float _musicVolume;

        public float MusicVolume => _musicVolume;


        public event Action<float> MusicVolumeChanged;
        public event Action<float> SoundVolumeChanged;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }

            SetSoundVolume(_soundVolume);
            SetMusicVolume(_musicVolume);
        }

        public void PlaySound(AudioClip audioClip)
        {
            _sfxSource.PlayOneShot(audioClip);
        }

        public void PlayMusic(AudioClip audioClip)
        {
            _musicSource.clip = audioClip;
            _sfxSource.Play();
        }

        public void SetSoundVolume(float value)
        {
            _soundVolume = value;
            _sfxSource.volume = _soundVolume;
            SoundVolumeChanged?.Invoke(_soundVolume);
        }

        public void SetMusicVolume(float value)
        {
            _musicVolume = value;
            _musicSource.volume = _musicVolume;
            MusicVolumeChanged?.Invoke(_musicVolume);
        }
    }
}