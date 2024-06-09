using System;
using UnityEngine;

namespace Game.Scripts.Utils
{
    public class TriggerSensor : MonoBehaviour
    {
        public event Action<Collider> ObjectEntered;

        private void OnTriggerEnter(Collider other)
        {
            ObjectEntered?.Invoke(other);
        }
    }
}