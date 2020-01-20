using System;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectEvents
{
    public abstract class GameEventListener<T, GE, UER> : MonoBehaviour
        where GE : GameEvent<T>
        where UER : UnityEvent<T>
    {
        public GE GameEvent;
        public UER UnityEventResponse;

        private void OnEnable()
        {
            if (GameEvent is null) return;
            GameEvent.EventListeners += TriggerResponses;
        }

        private void OnDisable()
        {
            if (GameEvent is null) return;
            GameEvent.EventListeners -= TriggerResponses;
        }

        public void TriggerResponses(T val)
        {
            UnityEventResponse.Invoke(val);
        }
    }

    public abstract class GameEventListener<GE, UER> : MonoBehaviour
    where GE : GameEvent
    where UER : UnityEvent
    {
        public GE GameEvent;
        public UER UnityEventResponse;

        private void OnEnable()
        {
            if (GameEvent is null) return;
            GameEvent.EventListeners += TriggerResponses;
        }

        private void OnDisable()
        {
            if (GameEvent is null) return;
            GameEvent.EventListeners -= TriggerResponses;
        }

        public void TriggerResponses()
        {
            UnityEventResponse.Invoke();
        }
    }
}
