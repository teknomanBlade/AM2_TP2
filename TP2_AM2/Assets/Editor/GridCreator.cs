using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridCreator : EditorWindow
{
    private GUIStyle _wStyle;
    private GridGenerator Grid_Generator;
    //nodos de ancho de la grilla
    public int width;
    //nodos de alto de la grilla
    public int height;
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
        Grid_Generator = FindObjectOfType<GridGenerator>();
        EditorGUILayout.BeginVertical(GUILayout.Height(110));
        GUILayout.Label("Seleccione ancho y alto de la grilla. El mínimo es 2x2");
        EditorGUILayout.BeginHorizontal(GUILayout.Width(10));
            width= EditorGUILayout.IntField("Ancho", width);
            height = EditorGUILayout.IntField("Alto", height);
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
            Grid_Generator.height = height;
            Grid_Generator.width = width;
            Grid_Generator.InitialPos = InitialPos;
            Grid_Generator.GenerateGrid();
        }


        /*if (GUILayout.Button("Cerrar ventana"))
        {
            Close();
        }*/

    }

    private void CheckValues()
    {
        if (width < 2)
        {
           width=_minWidth;
            Repaint();
        }

        if (height < 2)
        {
            height = _minHeight;
            Repaint();
        }

        return;

    }





}
