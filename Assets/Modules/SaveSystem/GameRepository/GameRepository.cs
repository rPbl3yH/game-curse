using System.Collections.Generic;
using Newtonsoft.Json;

namespace Modules.SaveSystem
{
    public sealed class GameRepository : IGameRepository
    {
        private Dictionary<string, string> _gameState = new();
        private readonly IGameStateSaver _gameStateSaver;

        public GameRepository(IGameStateSaver gameStateSaver)
        {
            _gameStateSaver = gameStateSaver;
        }

        public void LoadState()
        {
            _gameState = _gameStateSaver.Load();
        }

        public void SaveState()
        {
            _gameStateSaver.Save(_gameState);
        }

        public T GetData<T>()
        {
            var serializedData = _gameState[typeof(T).Name];
            return JsonConvert.DeserializeObject<T>(serializedData);
        }

        public bool TryGetData<T>(out T value)
        {
            if (_gameState.TryGetValue(typeof(T).Name, out var serializedData))
            {
                value = JsonConvert.DeserializeObject<T>(serializedData);
                return true;
            }

            value = default;
            return false;
        }

        public void SetData<T>(T value)
        {
            var serializedData = JsonConvert.SerializeObject(value);
            _gameState[typeof(T).Name] = serializedData;
        }
    }
}