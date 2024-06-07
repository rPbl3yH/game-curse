using System;
using UnityEngine;

namespace Modules.Input
{
    public interface IInputHandler
    {
        public event Action<Vector2> Up;
        public event Action<Vector2> Down;
        public event Action<Vector2> Drag;

        void Activate();
        void Deactivate();
    }

    public interface IScreenInputHandler : IInputHandler
    {
        public event Action Cancelled;
    }
}