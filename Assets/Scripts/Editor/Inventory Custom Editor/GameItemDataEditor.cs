using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SpaceHorror.InventorySystem.Editors
{
    [CustomEditor(typeof(GameItemData), true)]
    public class GameItemDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GameItemData data = target as GameItemData;

            if(data.Stackable)
            {
                GUILayout.BeginHorizontal();
                data.SetStackable(EditorGUILayout.Toggle("Stackable", data.Stackable));
                data.SetStackSize(EditorGUILayout.IntField("Stack Size", data.StackSize));
                GUILayout.EndHorizontal();          
            }
            else
            {
                data.SetStackable(EditorGUILayout.Toggle("Stackable", data.Stackable));
            }

            EditorUtility.SetDirty(data);
        }
    }
}
