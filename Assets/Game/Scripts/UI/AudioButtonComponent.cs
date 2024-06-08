using System;
using Modules.BaseUI;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class AudioButtonComponent : MonoBehaviour
    {
        [SerializeField] private BaseUIButton _baseUIButton;
        [SerializeField] private GameAudioConfig _gameAudioConfig;
        
        private void OnEnable()
        {
            _baseUIButton.Clicked += OnClicked;
        }

        private void OnDisable()
        {
            _baseUIButton.Clicked -= OnClicked;
        }

        private void OnClicked()
        {
            AudioManager.Instance.PlaySound(_gameAudioConfig.ButtonClicked);
        }
    }
}