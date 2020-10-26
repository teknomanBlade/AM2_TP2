using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Node))]
public class NodeEditor : Editor
{
    private Node _target;
    private GUIStyle _guiStyleBtnText;
    private GUIStyle _guiStyleSubTitle;
    public bool _showData;
    private void OnEnable()
    {
        _target = (Node)target;
        _guiStyleSubTitle = new GUIStyle()
        {
            fontSize = 15,
            alignment = TextAnchor.MiddleLeft,
            fontStyle = FontStyle.Bold,
            wordWrap = true
        };
        /*_guiStyleBtnText = new GUIStyle()
        {
            fontSize = 12,
            wordWrap = true
        };*/
    }

    private void OnSceneGUI()
    {
        Handles.BeginGUI();
            var p = Camera.current.WorldToScreenPoint(_target.transform.position);
            //calculamos posición...
            var r = new Rect(p.x - 50, Screen.height - p.y - 50, 80, 30);
            if (GUI.Button(r, "Select Node")) {
                _showData = !_showData;
            }

            if(_showData)
                DrawInspectorInScene();

        Handles.EndGUI();
    }

    private void DrawInspectorInScene()
    {
        GUILayout.BeginArea(new Rect(20, 20, 250, 250));
        var rec = EditorGUILayout.BeginVertical();
        //me crea un fondo de color que ocupa todo el rect creado por el Begin/EndVertical
        GUI.Box(rec, GUIContent.none);
        EditorGUILayout.LabelField(_target.gameObject.name + " Position: ", _guiStyleSubTitle);
        _target.gameObject.transform.position = EditorGUILayout.Vector3Field(GUIContent.none, _target.gameObject.transform.position);
        _target.isBlocked = EditorGUILayout.Toggle("Is Blocked: ", _target.isBlocked);
        _target.isPath = EditorGUILayout.Toggle("Is Path: " , _target.isPath);
        EditorGUILayout.EndVertical();
        GUILayout.EndArea();
    }
}
