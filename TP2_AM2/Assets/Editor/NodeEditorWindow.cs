using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class NodeEditorWindow : EditorWindow
{
    private GUIStyle _wStyle;
    public float SetNodeWeight;
    public int SetNodeID;
    public Node Nodo;
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

        Nodo = (Node)EditorGUILayout.ObjectField("Nodo: ", Nodo, typeof(Node), true);


        Nodo.NodeID = EditorGUILayout.IntField("ID", Nodo.NodeID);
        Nodo.f = EditorGUILayout.FloatField("Peso", Nodo.f);
        Repaint();

        _showFoldout = EditorGUILayout.Foldout(_showFoldout, "La cantidad de nodos vecinos es "+ Nodo.neighbors.Count);
        if (_showFoldout && Nodo.neighbors.Count!=0)
        {
            EditorGUILayout.BeginVertical();
            for (int i = 0; i < Nodo.neighbors.Count; i++)
            {
                EditorGUILayout.IntField("ID Vecino " + (i+1), Nodo.neighbors[i].NodeID);
               
            }

        }


        if (EditorGUI.EndChangeCheck())
        {
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());

        }

    }



}
