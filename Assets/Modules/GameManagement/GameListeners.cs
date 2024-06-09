namespace Modules.GameManagement
{
    public interface IGameListener
    {
        
    }

    public interface IGameInitListener : IGameListener
    {
        void InitGame();
    }
    
    public interface IGameStartListener : IGameListener
    {
        void StartGame();
    }

    public interface IGamePauseListener : IGameListener
    {
        void PauseGame();
    }

    public interface IGameResumeListener : IGameListener
    {
        void ResumeGame();
    }

    public interface IGameFinishListener : IGameListener
    {
        void FinishGame();
    }
    
    public interface IUpdateGameListener : IGameListener
    {
        void OnUpdate(float deltaTime);
    }
    
    public interface IFixedUpdateGameListener : IGameListener
    {
        void OnFixedUpdate(float fixedDeltaTime);
    }
    
    public interface ILateUpdateGameListener : IGameListener
    {
        void OnLateUpdate(float lateDeltaTime);
    }
}