using Modules.SaveSystem;
using UnityEngine;

namespace Game.Scripts
{
    public class SettingsSaveLoader : SaveLoader<SettingsData, AudioManager>
    {
        protected override void SetupData(AudioManager service, SettingsData data)
        {
            Debug.Log("Load settings data");
            AudioManager.Instance.SetMusicVolume(data.MusicVolume);
            AudioManager.Instance.SetSoundVolume(data.SoundVolume);
        }

        protected override SettingsData ConvertToData(AudioManager service)
        {
            return new SettingsData()
            {
                MusicVolume = AudioManager.Instance.MusicVolume,
                SoundVolume = AudioManager.Instance.SoundVolume,
            };
        }
    }
}