using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class NodeEditorWindow : EditorWindow
{
    private GUIStyle _wStyle;
    private GUIStyle _guiStyleSubTitle;
    public float SetNodeWeight;
    public int SetNodeID;
    public Node Nodo;
    private bool _showFoldout;
    private Vector2 scrollPos;
    /*[MenuItem("Custom Tools/Node Editor")]
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
    }*/

    public void Initialize(Node nodo) {
        _wStyle = new GUIStyle
        {
            fontStyle = FontStyle.Bold,
            fontSize = 15,
            alignment = TextAnchor.MiddleCenter,
            wordWrap = true
        };
        _guiStyleSubTitle = new GUIStyle()
        {
            fontSize = 15,
            alignment = TextAnchor.MiddleLeft,
            fontStyle = FontStyle.Bold,
            wordWrap = true
        };
        Nodo = nodo;
    }

    private void OnGUI()
    {
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.LabelField(Nodo.gameObject.name + " Data: ", _guiStyleSubTitle);

        EditorGUILayout.BeginHorizontal();
        Nodo = (Node)EditorGUILayout.ObjectField("Nodo: ", Nodo, typeof(Node), true);
        if (GUILayout.Button("Select On Scene", GUILayout.Width(120)))
        {
            EditorGUIUtility.PingObject(Nodo);
            Selection.activeGameObject = Nodo.gameObject;
        }
        EditorGUILayout.EndHorizontal();

        if (Nodo != null) {
            Nodo.NodeID = EditorGUILayout.IntField("ID", Nodo.NodeID);
            Nodo.gameObject.transform.position = EditorGUILayout.Vector3Field("Position", Nodo.gameObject.transform.position);
            Nodo.f = EditorGUILayout.FloatField("Peso", Nodo.f);
            Repaint();
            
            Nodo.IsBlocked = EditorGUILayout.Toggle("Is Blocked: ", Nodo.IsBlocked);
            Nodo.IsPath = EditorGUILayout.Toggle("Is Path: ", Nodo.IsPath);
            if (Nodo.IsPath) {
                EditorGUILayout.BeginHorizontal();
                Nodo.previous = (Node)EditorGUILayout.ObjectField("Nodo Previo: ", Nodo.previous, typeof(Node), true);
                if (GUILayout.Button("Select Previous", GUILayout.Width(120)))
                {
                    EditorGUIUtility.PingObject(Nodo.previous);
                    Selection.activeGameObject = Nodo.previous.gameObject;
                }
                EditorGUILayout.EndHorizontal();
            }

            _showFoldout = EditorGUILayout.Foldout(_showFoldout, "La cantidad de nodos vecinos es "+ Nodo.neighbors.Count);
            if (_showFoldout && Nodo.neighbors.Count != 0)
            {
                scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(550));
                for (int i = 0; i < Nodo.neighbors.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.IntField("ID Vecino " + (i+1), Nodo.neighbors[i].NodeID);
                        Nodo.neighbors[i] = (Node)EditorGUILayout.ObjectField(GUIContent.none, Nodo.neighbors[i], typeof(Node), true);
                        if (GUILayout.Button("Select Neighbour", GUILayout.Width(120)))
                        {
                            EditorGUIUtility.PingObject(Nodo.neighbors[i]);
                            Selection.activeGameObject = Nodo.neighbors[i].gameObject;
                        }
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.EndScrollView();
            }
            
        }
        if (!Application.isPlaying)
        {
            if (EditorGUI.EndChangeCheck())
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());

            }
        }

    }



}
