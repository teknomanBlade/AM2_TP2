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
    private Node _selectedNode;
    /*[MenuItem("Custom/Path Preview")]
    public static void ShowWindow()
    {
        GetWindow<PreviewWindow>().Show();
    }*/
    public void Initialize(List<Node> pathIA) {
        _path = pathIA;
    }

    private void OnGUI()
    {
        
        if (_path.Count == 0)
        {
            //_path = FindObjectOfType<IALauncher>().path;
            //_path = FindObjectOfType<IAManager>().PathIA;
            Debug.Log("agarre lista/////////    " + _path.Count);
        }
        else
        {
            //GUILayout.BeginArea(new Rect(40f, 40f, 450f, 1024f));

            EditorGUILayout.BeginVertical();
            GUIUtility.RotateAroundPivot(45, Vector2.zero);

                for (int i = 0; i < _path.Count; i++)
                {
                    Handles.color = Color.green;
                    _actual = new Vector3(80 + _path[i].transform.position.z * 18, 40 + _path[i].transform.position.x * 10);
                    //Handles.DrawSolidDisc(_actual, Vector3.forward, 10);
                    if (Handles.Button(_actual + Vector3.back * 20, Quaternion.identity, 40f, 35f, Handles.SphereHandleCap))
                    {
                        _selectedNode = _path[i];
                        EditorGUIUtility.PingObject(_selectedNode);
                        Selection.activeGameObject = _selectedNode.gameObject;
                        //Debug.Log("Button Clicked");
                    }
                    if (i > 0)
                    {
                        Handles.color = Color.blue;
                        Handles.DrawLine(_previous, _actual);
                    }
                    _previous = _actual;
                }

            EditorGUILayout.EndVertical();
        }
        

        //if (path.Count != 0)
        //    DrawNode();
        //this.Repaint();
    }

    private void DrawNode()
    {
        //foreach (var node in path)
        //{
        //    Handles.DrawSolidDisc(new Vector3(node.transform.position.x / 2, node.transform.position.y / 2, node.transform.position.z / 2), node.transform.up, 5);
        //}
    }
}
