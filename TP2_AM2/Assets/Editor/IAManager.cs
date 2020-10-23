using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IAManager : EditorWindow
{
    private GUIStyle _guiStyleTitle;
    private GUIStyle _guiStyleSubTitle;
    private Vector3 _positionPath;
    private Vector2 scrollPos;
    private AStar AStar;
    private Node _initialNode;
    private Node _endNode;
    [MenuItem("IA/IA Manager")]
    public static void OpenWindow() {
        var window = GetWindow<IAManager>();
        window.maxSize = new Vector2(750, 900);
        window.Initialize();
        window.Show();
    }

    public void Initialize() {
        AStar = FindObjectOfType<AStar>();
        
        _positionPath = new Vector3(2,5,9);
        _guiStyleTitle = new GUIStyle() {
            fontSize = 20,
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
            wordWrap = true
        };
        _guiStyleSubTitle = new GUIStyle()
        {
            fontSize = 14,
            alignment = TextAnchor.MiddleLeft,
            fontStyle = FontStyle.BoldAndItalic,
            wordWrap = true
        };
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("IA Information", _guiStyleTitle);

        GUILayout.BeginArea(new Rect(40f,40f,450f, 900f));
        _initialNode = (Node)EditorGUILayout.ObjectField("Nodo Inicial: ", _initialNode, typeof(Node), true);
        _endNode = (Node)EditorGUILayout.ObjectField("Nodo Final: ", _endNode, typeof(Node), true);

        if (GUILayout.Button("Iniciar Algoritmo A*"))
        {
            //AStar.GetPath();
        }
        EditorGUILayout.BeginHorizontal();
            //EditorGUILayout.LabelField("Node Path Map", _guiStyleSubTitle);
            EditorGUILayout.LabelField("Waypoint Path Vectors", _guiStyleSubTitle);
        EditorGUILayout.EndHorizontal();
        LoadSpaces(2);

        int sizeList = 25;

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(sizeList * 25));
            for (int i = 0; i < sizeList; i++)
            {
                EditorGUILayout.BeginHorizontal();
                    _positionPath = EditorGUILayout.Vector3Field("Nodo " + i + " del Path ", _positionPath);
                EditorGUILayout.EndHorizontal();
            }

        EditorGUILayout.EndScrollView();

        GUILayout.EndArea();
       


    }

    public void LoadSpaces(int length)
    {
        for (int i = 0; i < length; i++)
        {
            EditorGUILayout.Space();
        }
    }

}
