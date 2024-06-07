namespace Modules.SaveSystem
{
    public interface ISaveLoader
    {
        void LoadGame(IGameRepository repository, IGameContext gameContext);

        void SaveGame(IGameRepository repository, IGameContext gameContext);
    }
}
