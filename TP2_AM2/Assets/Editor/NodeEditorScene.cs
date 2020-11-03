using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (Node))]
public class NodeEditorScene : Editor
{
    Node _target;

    private void OnEnable()
    {
        _target=(Node)target;
    }


    private void OnSceneGUI()
    {


        if (_target.isPath)
        {
            Handles.color = Color.green;
        }
        if(_target.isBlocked)
        {
            Handles.color = Color.red;
        }

        Handles.DrawWireDisc(_target.transform.position, _target.transform.up, _target.radius);
    }
}
