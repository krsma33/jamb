using System;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectEvents
{
    public abstract class GameEventListener<T, GE> : MonoBehaviour
        where GE : GameEvent<T>
    {
        public GE GameEvent;

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

        protected void TriggerResponses(T val)
        {
            HandleEvent(val);
        }

        public abstract void HandleEvent(T val);
    }

    public abstract class GameEventListener<GE> : MonoBehaviour
    where GE : GameEvent
    {
        public GE GameEvent;

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

        protected void TriggerResponses()
        {
            HandleEvent();
        }

        public abstract void HandleEvent();
    }
}
