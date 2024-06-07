using System.Collections.Generic;

namespace Modules.SaveSystem
{
    public interface IGameContext
    {
        public T GetService<T>() where T : class;
    }
}