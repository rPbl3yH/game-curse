using Modules.GameManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class MusicSettingPresenter : MonoBehaviour, IGameInitListener
    {
        [SerializeField] private Slider _slider;

        void IGameInitListener.InitGame()
        {
            _slider.value = AudioManager.Instance.MusicVolume;
        }

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            AudioManager.Instance.SetMusicVolume(value);
        }
    }
}