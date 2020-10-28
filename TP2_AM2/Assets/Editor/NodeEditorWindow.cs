using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class NodeEditorWindow : EditorWindow
{
    private GUIStyle _wStyle;
    public int SetNodeWeight;
    public int SetNodeID;
    public tempNodoJM Nodo;
    private bool _showFoldout;

    [MenuItem("Custom Tools/Node Editor")]
    public static void OpenWindow()
    {
        var w = GetWindow<NodeEditorWindow>();
        w._wStyle = new GUIStyle
        {
            fontStyle = FontStyle.Bold,
            fontSize = 15,
            alignment = TextAnchor.MiddleCenter,
            wordWrap = true
        };

        w.Show();
    }

    private void OnGUI()
    {
        EditorGUI.BeginChangeCheck();

        Nodo = (tempNodoJM)EditorGUILayout.ObjectField("Nodo: ", Nodo, typeof(tempNodoJM), true);


        Nodo.NodeID = EditorGUILayout.IntField("ID", Nodo.NodeID);
        Nodo.NodeWeight = EditorGUILayout.IntField("Peso", Nodo.NodeWeight);
        Repaint();

        _showFoldout = EditorGUILayout.Foldout(_showFoldout, "La cantidad de nodos vecinos es "+ Nodo.NeighboursID.Count);
        if (_showFoldout && Nodo.NeighboursID.Count!=0)
        {
            EditorGUILayout.BeginVertical();
            for (int i = 0; i < Nodo.NeighboursID.Count; i++)
            {
                EditorGUILayout.IntField("ID Vecino " + (i+1), Nodo.NeighboursID[i]);
            }

        }


        if (EditorGUI.EndChangeCheck())
        {
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());

        }

    }



}
