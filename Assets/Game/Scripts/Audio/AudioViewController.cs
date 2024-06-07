using System;
using Modules.BaseUI;
using UnityEngine;

namespace Game
{
    public class AudioViewController : MonoBehaviour
    {
        [SerializeField] private BaseUIButton _mouseButton;
        [SerializeField] private BaseUIButton _soundButton;
        
        private float _startSfxVolume;
        private float _startMusicVolume;

        private void Start()
        {
            _startSfxVolume = AudioManager.Instance.SoundVolume;
            _startMusicVolume = AudioManager.Instance.MusicVolume;
        }

        private void OnEnable()
        {
            _mouseButton.Clicked += OnMusicMouseButtonClick;
            _soundButton.Clicked += OnSoundButtonClick;
        }

        private void OnDisable()
        {
            _mouseButton.Clicked -= OnMusicMouseButtonClick;
            _soundButton.Clicked -= OnSoundButtonClick;
        }

        private void OnMusicMouseButtonClick()
        {
            if (AudioManager.Instance.MusicVolume == 0f)
            {
                EnableMusicVolume();
            }
            else
            {
                DisableMusicVolume();
            }
        }

        private void OnSoundButtonClick()
        {
            if (AudioManager.Instance.SoundVolume == 0f)
            {
                EnableSoundVolume();
            }
            else
            {
                DisableSoundVolume();
            }
        }

        private void EnableMusicVolume()
        {
            AudioManager.Instance.SetMusicVolume(_startMusicVolume);
        }

        private void DisableMusicVolume()
        {
            AudioManager.Instance.SetMusicVolume(0f);
        }

        private void EnableSoundVolume()
        {
            AudioManager.Instance.SetSoundVolume(_startSfxVolume);
        }

        private void DisableSoundVolume()
        {
            AudioManager.Instance.SetSoundVolume(0f);
        }
    }
}