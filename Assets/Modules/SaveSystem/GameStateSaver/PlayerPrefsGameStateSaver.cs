using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Modules.SaveSystem
{
    public sealed class PlayerPrefsGameStateSaver : IGameStateSaver
    {
        private const string GAME_STATE_KEY = "GameState";

        public void Save(Dictionary<string, string> gameState)
        {
            var serializedData = JsonConvert.SerializeObject(gameState);
            PlayerPrefs.SetString(GAME_STATE_KEY, serializedData);
        }

        public Dictionary<string, string> Load()
        {
            if (PlayerPrefs.HasKey(GAME_STATE_KEY))
            {
                var savedJson = PlayerPrefs.GetString(GAME_STATE_KEY);
                var gameState = JsonConvert.DeserializeObject<Dictionary<string, string>>(savedJson);
                return gameState;

            }

            return new Dictionary<string, string>();
        }
    }
}