using System.Collections.Generic;
using UnityEngine;

namespace GameEventSystem
{
    [CreateAssetMenu]
    public class GameEvent : ScriptableObject
    {
        private readonly List<GameEventListener> _eventListeners = new List<GameEventListener>();
        public int value;
        public void Raise()
        {
            Debug.Log($"Raised {_eventListeners.Count}");
            for (var i = (_eventListeners.Count-1); i >= 0; i--)
                _eventListeners[i].OnEventRaised();
        }

        public void RegisterListener(GameEventListener listener)
        {
            if(!_eventListeners.Contains(listener))
                _eventListeners.Add(listener);
        }
        public void UnRegisterListener(GameEventListener listener)
        {
            if(_eventListeners.Contains(listener))
                _eventListeners.Remove(listener);
        }
    }
}
