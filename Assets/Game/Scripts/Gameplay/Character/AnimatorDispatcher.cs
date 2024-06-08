using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    public class AnimatorDispatcher : MonoBehaviour
    {
        private readonly Dictionary<string, List<Action>> _dictionary = new();
        
        public void ReceiveEvent(string key)
        {
            if (_dictionary.TryGetValue(key, out var actionsList))
            {
                foreach (var action in actionsList)
                {
                    action.Invoke();
                }
            }
        }

        public void SubscribeOnEvent(string key, Action action)
        {
            if (!_dictionary.ContainsKey(key))
            {
                _dictionary.Add(key, new List<Action>());
            }
            
            _dictionary[key].Add(action);
        }

        public void UnsubscribeOnEvent(string key, Action action)
        {
            if (_dictionary.TryGetValue(key, out var actionsList))
            {
                actionsList.Remove(action);
            }
        }
    }
}
