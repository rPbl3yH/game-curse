using System.Collections.Generic;

namespace Modules.SaveSystem
{
    public sealed class SaveLoadManager
    {
        private readonly GameRepository _gameRepository;
        private readonly List<ISaveLoader> _saveLoaders;
        private readonly IGameContext _gameContext;

        public SaveLoadManager(List<ISaveLoader> saveLoaders, IGameContext gameContext)
        {
            _saveLoaders = saveLoaders;
            _gameContext = gameContext;
            _gameRepository = new GameRepository(new PlayerPrefsGameStateSaver());
        }

        public void Save()
        {
            foreach (var saveLoader in _saveLoaders)
            {
                saveLoader.SaveGame(_gameRepository, _gameContext);    
            }
            
            _gameRepository.SaveState();
        }

        public void Load()
        {
            _gameRepository.LoadState();
            
            foreach (var saveLoader in _saveLoaders)
            {
                saveLoader.LoadGame(_gameRepository, _gameContext);
            }
        }
    }
}