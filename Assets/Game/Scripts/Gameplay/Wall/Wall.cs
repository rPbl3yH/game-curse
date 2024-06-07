using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class Wall : MonoBehaviour
    {
        
        private TriggerListener _triggerListener;
        
        
        [Inject]
        public void Construct(TriggerListener triggerListener)
        {
            _triggerListener = triggerListener;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            _triggerListener.OnTrigger(other);
        }
    }
}
