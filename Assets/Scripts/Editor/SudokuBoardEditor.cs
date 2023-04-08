using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SudokuBoard))]
public class SudokuBoardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Draw all properties except for 'useRandomSeed' and 'boardSeed'
        SerializedProperty iterator = serializedObject.GetIterator();
        bool enterChildren = true;
        while (iterator.NextVisible(enterChildren))
        {
            if (iterator.name != "useRandomSeed" && iterator.name != "boardSeed")
            {
                EditorGUILayout.PropertyField(iterator, true);
            }
            enterChildren = false;
        }

        // Draw 'useRandomSeed' property
        SerializedProperty useRandomSeedProperty = serializedObject.FindProperty("useRandomSeed");
        EditorGUILayout.PropertyField(useRandomSeedProperty);

        // Draw 'boardSeed' property if 'useRandomSeed' is false
        if (!useRandomSeedProperty.boolValue)
        {
            SerializedProperty boardSeedProperty = serializedObject.FindProperty("boardSeed");
            EditorGUILayout.PropertyField(boardSeedProperty);
        }

        serializedObject.ApplyModifiedProperties();
    }
}