namespace Modules.SaveSystem
{
    public interface IGameRepository
    {
        T GetData<T>();

        bool TryGetData<T>(out T value);

        void SetData<T>(T value);
    }
}