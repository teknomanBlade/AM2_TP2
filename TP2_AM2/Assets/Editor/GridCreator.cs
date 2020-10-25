using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridCreator : EditorWindow
{
    private GUIStyle _wStyle;
    //nodos de ancho de la grilla
    public int NodesWidth;
    //nodos de alto de la grilla
    public int NodesHeight;
    //posición inicial de la grilla
    public Vector3 InitialPos;
    //floats de la posición inicial de la grilla
    private float _posX;
    private float _posY;
    private float _posZ;
    //valores minimos de alto y ancho
    private int _minWidth = 2;
    private int _minHeight = 2;
    //ver si agregamos valores maximos ???


    [MenuItem("Custom Tools/Grid Creator")]
    public static void OpenWindow()
    {
        var w = GetWindow<GridCreator>();
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
        EditorGUILayout.BeginVertical(GUILayout.Height(110));
        GUILayout.Label("Seleccione ancho y alto de la grilla. El mínimo es 2x2");
        EditorGUILayout.BeginHorizontal(GUILayout.Width(10));
        NodesWidth= EditorGUILayout.IntField("Ancho", NodesWidth);
        NodesHeight = EditorGUILayout.IntField("Alto", NodesHeight);
        EditorGUILayout.EndHorizontal();
        GUILayout.Label("Seleccione posición inicial de la grila en X, Y y Z");
        EditorGUILayout.BeginHorizontal(GUILayout.Width(5));
        _posX= EditorGUILayout.FloatField("X", _posX);
        _posY= EditorGUILayout.FloatField("Y", _posY);
        _posZ = EditorGUILayout.FloatField("Z", _posZ);
        InitialPos = new Vector3(_posX, _posY, _posZ);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
               
        if(GUILayout.Button("Crear Grilla"))
        {
            CheckValues();

            //pasar parámetros de ancho alto y posición a Nico
        }


        if (GUILayout.Button("Cerrar ventana"))
        {
            Close();
        }

    }

    private void CheckValues()
    {
        if (NodesWidth < 2)
        {
           NodesWidth=_minWidth;
            Repaint();
        }

        if (NodesHeight < 2)
        {
            NodesHeight = _minHeight;
            Repaint();
        }

        return;

    }





}
