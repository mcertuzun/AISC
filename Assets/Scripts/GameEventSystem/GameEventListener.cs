using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameEventSystem
{
    public class GameEventListener : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        public GameEvent gameEvent;
        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent responseEvent;

        private void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            gameEvent.UnRegisterListener(this);
        }

        public void OnEventRaised()
        {
            responseEvent.Invoke();
        }
    }
}