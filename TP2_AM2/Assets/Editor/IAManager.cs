using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IAManager : EditorWindow
{
    private GUIStyle _guiStyleTitle;
    private GUIStyle _guiStyleSubTitle;
    private GUIStyle _guiStyleNodeTitle;
    private GUIStyle _guiStyleNodeInitialTitle;
    private GUIStyle _guiStyleNodeFinalTitle;
    private Vector2 scrollPos;
    private AStar AStar;
    private NodesGenerator _nodesGenerator;
    private Node _initialNode;
    private Node _endNode;
    private Node _selectedNode;
    private PreviewWindow _previewWindow;
    private List<Node> _pathIA;
    public List<Node> PathIA
    {
        get {
            return _pathIA;
        }
        set {
            _pathIA = value;
        }
    }
    public bool IsNullNodeInitialAndFinal { get; set; }
    [MenuItem("IA/IA Manager")]
    public static void OpenWindow() {
        var window = GetWindow<IAManager>();
        window.maxSize = new Vector2(750, 1024);
        window.Initialize();
        window.Show();
    }

    public void Initialize() {
        AStar = FindObjectOfType<AStar>();
        _nodesGenerator = FindObjectOfType<NodesGenerator>();
        PathIA = new List<Node>();
        _guiStyleNodeInitialTitle = new GUIStyle()
        {
            fontSize = 12,
            alignment = TextAnchor.MiddleLeft,
            fontStyle = FontStyle.Bold,
            wordWrap = true

        };
        _guiStyleNodeInitialTitle.normal.textColor = Color.green;
        _guiStyleNodeFinalTitle = new GUIStyle()
        {
            fontSize = 12,
            alignment = TextAnchor.MiddleLeft,
            fontStyle = FontStyle.Bold,
            wordWrap = true
        };
        _guiStyleNodeFinalTitle.normal.textColor = Color.green;
        _guiStyleNodeTitle = new GUIStyle()
        {
            fontSize = 12,
            alignment = TextAnchor.MiddleLeft,
            fontStyle = FontStyle.Bold,
            wordWrap = true
        };
        _guiStyleTitle = new GUIStyle() {
            fontSize = 20,
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
            wordWrap = true
        };
        _guiStyleSubTitle = new GUIStyle()
        {
            fontSize = 15,
            alignment = TextAnchor.MiddleLeft,
            fontStyle = FontStyle.BoldAndItalic,
            wordWrap = true
        };
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("IA Information", _guiStyleTitle);

        GUILayout.BeginArea(new Rect(40f,40f,450f, 1024f));
        _initialNode = (Node)EditorGUILayout.ObjectField("Nodo Inicial: ", _initialNode, typeof(Node), true);
        _endNode = (Node)EditorGUILayout.ObjectField("Nodo Final: ", _endNode, typeof(Node), true);

        if (GUILayout.Button("Iniciar Algoritmo A*"))
        {
            
            if (_initialNode != null && _endNode != null)
            {

                PathIA = AStar.GetPath(_initialNode, _endNode);
                IsNullNodeInitialAndFinal = false;
            }
            else {
                IsNullNodeInitialAndFinal = true;
            }
        }
        if (IsNullNodeInitialAndFinal) {
            EditorGUILayout.HelpBox("Por favor asigne los Nodos Inicial y Final antes de ejecutar el algoritmo", MessageType.Error);
        }

        if (GUILayout.Button("Randomize Blocked Nodes")) {
            _nodesGenerator.SetRandomBlockedNodes();
        }

        if (GUILayout.Button("Clear Path Nodes"))
        {
            ClearPathNodes();
        }

        if (GUILayout.Button("Preview Path IA"))
        {
            _previewWindow = GetWindow<PreviewWindow>();
            _previewWindow.start = new Vector2(position.xMin, position.yMin);
            _previewWindow.offset = new Vector2(position.width, position.height);
            _previewWindow.Initialize(PathIA);
        }

        EditorGUILayout.BeginHorizontal();
            //EditorGUILayout.LabelField("Node Path Map", _guiStyleSubTitle);
            EditorGUILayout.LabelField("Waypoint Path Vectors", _guiStyleSubTitle);
        EditorGUILayout.EndHorizontal();

        LoadSpaces(2);
        if (PathIA.Count > 0) {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(550));
            for (int i = 0; i < PathIA.Count; i++)
            {
                EditorGUILayout.BeginVertical();
                //EditorGUILayout.SelectableLabel(_pathIA[i].gameObject.name, _guiStyleNodeTitle);
                HighlightInitialAndFinalNodes(PathIA, i);
                PathIA[i].isPath = true;
                    EditorGUILayout.BeginHorizontal();
                PathIA[i].gameObject.transform.position = EditorGUILayout.Vector3Field(GUIContent.none, PathIA[i].gameObject.transform.position);
                    if(GUILayout.Button("Select")){
                        _selectedNode = PathIA[i];
                        EditorGUIUtility.PingObject(_selectedNode);
                        Selection.activeGameObject = _selectedNode.gameObject;
                    }
                    EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndScrollView();
        }

        GUILayout.EndArea();
    }

    public void ClearPathNodes() {
        foreach (var item in _nodesGenerator.grid)
        {
            item.ClearNode();
            if(item.isPath)
                item.isPath = false;
        }

        PathIA.Clear();
    }

    public void HighlightInitialAndFinalNodes(List<Node> pathIA, int index) {
        if (pathIA[index].Equals(_initialNode))
        {
            EditorGUILayout.SelectableLabel(pathIA[index].gameObject.name, _guiStyleNodeInitialTitle);
        }
        else if (pathIA[index].Equals(_endNode))
        {
            EditorGUILayout.SelectableLabel(pathIA[index].gameObject.name, _guiStyleNodeFinalTitle);
        }
        else
        {
            EditorGUILayout.SelectableLabel(pathIA[index].gameObject.name, _guiStyleNodeTitle);
        }
    }

    public void LoadSpaces(int length)
    {
        for (int i = 0; i < length; i++)
        {
            EditorGUILayout.Space();
        }
    }

}
