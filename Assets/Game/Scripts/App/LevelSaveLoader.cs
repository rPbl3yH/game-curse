using Modules.SaveSystem;

namespace Game.Scripts
{
    public class LevelSaveLoader : SaveLoader<int, LevelController>
    {
        protected override void SetupData(LevelController service, int data)
        {
            service.Level = data;
        }

        protected override int ConvertToData(LevelController service)
        {
            return service.Level;
        }
    }
}