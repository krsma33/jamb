using System;
using UnityEngine;

namespace ScriptableObjectEvents
{
    public abstract class GameEvent<T> : ScriptableObject
    {
        public event Action<T> EventListeners;

        public void Raise(T item)
        {
            EventListeners(item);
        }
    }

    public abstract class GameEvent : ScriptableObject
    {
        public event Action EventListeners;

        public void Raise()
        {
            EventListeners();
        }
    }
}

