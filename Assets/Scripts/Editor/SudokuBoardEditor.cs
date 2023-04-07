using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SudokuBoard))]
public class SudokuGridEditpr : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SudokuBoard sudokuBoard = (SudokuBoard)target;

        if (GUILayout.Button("Build Board")) {
            sudokuBoard.StartBuildBoard();
        }
        if (GUILayout.Button("Clear Board")) {
            sudokuBoard.ClearBoard();
        }
    }
}