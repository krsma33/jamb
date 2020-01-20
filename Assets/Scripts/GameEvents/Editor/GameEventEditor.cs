﻿using UnityEditor;
using UnityEngine;

namespace ScriptableObjectEvents
{
    [CustomEditor(typeof(GameEvent), true)]
    public class GameEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            GameEvent e = target as GameEvent;

            if (GUILayout.Button("Raise"))
            {
                e.Raise();
            }
        }
    }
}

