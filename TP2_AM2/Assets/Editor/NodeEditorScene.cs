using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (tempNodoJM))]
public class NodeEditorScene : Editor
{
    tempNodoJM _target;

    private void OnEnable()
    {
        _target=(tempNodoJM)target;
    }


    private void OnSceneGUI()
    {


        if (_target.IsActive)
        {
            Handles.color = Color.green;
        }
        else
        {
            Handles.color = Color.red;
        }

        Handles.DrawWireDisc(_target.transform.position, _target.transform.up, _target.Area);
    }
}
