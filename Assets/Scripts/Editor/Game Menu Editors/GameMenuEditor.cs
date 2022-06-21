using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SpaceHorror.UI.Editors
{


    [CustomEditor(typeof(GameMenu))]
    public class GameMenuEditor : Editor
    {
        private string _toOpen;

        public override void OnInspectorGUI()
        {
            Debug.Log("I Exist");
            DrawDefaultInspector();

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Open Menu"))
            {
                OpenMenu(_toOpen);
            }

            string toOpen = EditorGUILayout.TextField(_toOpen);

            _toOpen = toOpen;

            if (GUILayout.Button("Reset Menu"))
            {
                GameMenu menu = target as GameMenu;
                menu.ResetMenus();
            }

            GUILayout.EndHorizontal();
        }

        private void OpenMenu(string path)
        {
            if (string.IsNullOrEmpty(path)) return;

            GameMenu menu = target as GameMenu;

            menu.OpenMenu(path);
        }
    }
}