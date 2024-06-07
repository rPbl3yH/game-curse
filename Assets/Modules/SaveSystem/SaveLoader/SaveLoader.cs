namespace Modules.SaveSystem
{
    public abstract class SaveLoader<TData, TService> : ISaveLoader where TService : class
    {
        private ISaveLoader _saveLoaderImplementation;

        void ISaveLoader.LoadGame(IGameRepository repository, IGameContext gameContext)
        {
            var service = gameContext.GetService<TService>();
            if (repository.TryGetData(out TData data))
            {
                SetupData(service, data);
            }
            else
            {
                SetupByDefault(service);
            }
        }

        void ISaveLoader.SaveGame(IGameRepository repository, IGameContext gameContext)
        {
            var service = gameContext.GetService<TService>();
            var data = ConvertToData(service);
            repository.SetData(data);
        }

        protected abstract void SetupData(TService service, TData level);

        protected abstract TData ConvertToData(TService service);

        protected virtual void SetupByDefault(TService service)
        {
        }
    }
}