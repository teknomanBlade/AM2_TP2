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
            for (int i = 0; i < _path.Count; i++)
            {
                _actual = new Vector3(50 + _path[i].transform.position.z * 20, 50 + _path[i].transform.position.x * 10);
                if (i == 0)
                {
                    Debug.Log("START");
                    Handles.color = Color.green;
                    Handles.Label(_actual, "Start", _importantStyle);
                    Handles.DrawSolidDisc(_actual, Vector3.forward, _radius/2);
                }
                else if (i == _path.Count - 1)
                {
                    Debug.Log("END");
                    Handles.color = Color.red;
                    Handles.Label(_actual, "End", _importantStyle);
                    Handles.DrawSolidDisc(_actual, Vector3.forward, _radius);
                }
                else
                {
                    Handles.color = Color.blue;
                    Handles.DrawSolidDisc(_actual, Vector3.forward, _radius/2);
                    Handles.Label(_actual, "Node " + i, _secStyle);
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
