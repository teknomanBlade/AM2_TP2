using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PreviewWindow : EditorWindow
{
    float gridX = 100;
    float gridY = 100;
    int _nodes;
    bool _button = false;
    Vector3 _previous;
    Vector3 _actual;
    List<Node> _path = new List<Node>();
    GUIStyle _importantStyle = new GUIStyle();
    GUIStyle _secStyle = new GUIStyle();
    float _radius;
    public Vector2 start;
    public Vector2 offset;

    public Node _selectedNode { get; private set; }

    private void Awake()
    {
        _importantStyle.fontSize = 30;
        _importantStyle.fontStyle = FontStyle.Bold;
        _secStyle.fontSize = 15;
        _radius = 10;
        wantsMouseMove = true;
    }
    public void Initialize(List<Node> pathIA) {
        _path = pathIA;
        position = new Rect(new Vector2(start.x+offset.x,start.y), new Vector2(offset.x * 2, offset.y));
        Show();
    }

    private void OnGUI()
    {
        DrawNode();
    }
    private void DrawNode()
    {
        if (_path.Count == 0)
        {
            Handles.color = Color.red;
            Handles.Label(Vector2.zero, "NO AVAILABLE NODES", _importantStyle);
        }
        else
        {
            //GUIUtility.RotateAroundPivot(-90, new Vector2(Screen.width / 2, Screen.height / 2));
            for (int i = 0; i < _path.Count; i++)
            {
                _actual = new Vector3(60 + _path[i].transform.position.z * 20, 80 + _path[i].transform.position.x * 15);
                if (i == 0)
                {
                    Debug.Log("START");
                    Handles.color = Color.green;
                    Handles.Label(_actual, "Start", _importantStyle);
                    if (Handles.Button(_actual + Vector3.back * 20, Quaternion.identity, 40f, 35f, Handles.SphereHandleCap))
                    {
                        _selectedNode = _path[i];
                        EditorGUIUtility.PingObject(_selectedNode);
                        Selection.activeGameObject = _selectedNode.gameObject;
                        //Debug.Log("Button Clicked");
                    }
                    //Handles.DrawSolidDisc(_actual, Vector3.forward, _radius/2);
                }
                else if (i == _path.Count - 1)
                {
                    Debug.Log("END");
                    Handles.color = Color.red;
                    Handles.Label(_actual, "End", _importantStyle);
                    if (Handles.Button(_actual + Vector3.back * 20, Quaternion.identity, 40f, 35f, Handles.SphereHandleCap))
                    {
                        _selectedNode = _path[i];
                        EditorGUIUtility.PingObject(_selectedNode);
                        Selection.activeGameObject = _selectedNode.gameObject;
                        //Debug.Log("Button Clicked");
                    }
                    //Handles.DrawSolidDisc(_actual, Vector3.forward, _radius);
                }
                else
                {
                    Handles.color = Color.blue;
                    if (Handles.Button(_actual + Vector3.back * 20, Quaternion.identity, 40f, 35f, Handles.SphereHandleCap))
                    {
                        _selectedNode = _path[i];
                        EditorGUIUtility.PingObject(_selectedNode);
                        Selection.activeGameObject = _selectedNode.gameObject;
                        //Debug.Log("Button Clicked");
                    }
                    //Handles.DrawSolidDisc(_actual, Vector3.forward, _radius/2);
                    Handles.Label(_actual, _path[i].name/*"Node " + i*/, _secStyle);
                }
                if (i > 0)
                {
                    Handles.color = Color.black;
                    Handles.DrawLine(_previous, _actual);
                }
                _previous = _actual;
            }
        }
       
    }
}
