using System.Collections.Generic;

namespace Modules.SaveSystem
{
    public interface IGameStateSaver
    {
        void Save(Dictionary<string, string> gameState);
        Dictionary<string, string> Load();
    }
}