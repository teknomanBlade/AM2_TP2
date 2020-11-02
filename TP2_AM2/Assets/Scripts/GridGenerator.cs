using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridGenerator : MonoBehaviour
{
	public OtherNodeNicolas sphere; //este es un nodo temporal
	public int width; //cantidad de nodos por ancho
	public int height; //cantidad de nodos por alto
	public float offset; //la distancia entre los nodos
    public Vector3 InitialPos; //posición inicial de la grilla, Nico fijate de usarla, saludos JM

	private Transform _container; //acá guardo los nodos que genero para que no sea un lío la jerarquia 
	private List<OtherNodeNicolas> _myNodes = new List<OtherNodeNicolas>(); //lista de todos los nodos generados

	// Start is called before the first frame update
	void Start()
	{
		GenerateGrid();
	}

	//Crea un los nodos adentro de un contenedor, según el width, height y offset
	void GenerateGrid()
	{
		if (_container != null) return;

		_container = new GameObject().transform;
		_container.position = Vector3.zero;
		_container.transform.name = "AllNodes";

		int counter = 0;
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				var tempNode = Instantiate(sphere, new Vector3(j * offset, 0, i * offset), Quaternion.identity);
				tempNode.transform.SetParent(_container);
				tempNode.transform.name = "Node " + counter;

				tempNode.radius = offset;
				_myNodes.Add(tempNode);

				counter++;

			}
		}

		Debug.Log("I created the grid");

		StartCoroutine(CheckNode());
	}

	//Necesito la corrutina para que no se llamen las funciones en el mismo frame
	IEnumerator CheckNode()
	{
		yield return new WaitForSeconds(0.01f);
		CheckAndDeactivate();
	}

	//Desactiva los nodos que considera que estan fuera del nivel
	void CheckAndDeactivate()
	{
		for (int i = 0; i < _myNodes.Count; i++)
		{
			var tempNode = _myNodes[i].NodesToDeactivate();
			if (tempNode != null)
				tempNode.gameObject.SetActive(false);
			_myNodes[i].CheckNeighbors();
		}
	}
}
