using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridGenerator : MonoBehaviour
{
	public Node sphere; //este es un nodo temporal
	public int width; //cantidad de nodos por ancho
	public int height; //cantidad de nodos por alto
	public float offset; //la distancia entre los nodos
	public Vector3 InitialPos; //posición inicial de la grilla, Nico fijate de usarla

	private Transform _container; //acá guardo los nodos que genero para que no sea un lío la jerarquia 
	private List<Node> _myNodes = new List<Node>(); //lista de todos los nodos generados

	//Crea un los nodos adentro de un contenedor, según el width, height y offset
	public void GenerateGrid()
	{
		if (_container != null) return;

		_myNodes.Clear();
		_container = new GameObject().transform;
		_container.transform.gameObject.AddComponent<NodesGenerator>();
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
				tempNode.NodeID = counter;
				tempNode.radius = offset;
				_myNodes.Add(tempNode);

				counter++;

			}
		}
		var nodesGenerator = _container.gameObject.GetComponent<NodesGenerator>();
		nodesGenerator.grid = _myNodes;

		Debug.Log("I created the grid");

		StartCoroutine(CheckNode());
	}

	//Necesito la corrutina para que no se llamen las funciones en el mismo frame
	IEnumerator CheckNode()
	{
		List<Node> nodeToRemove = new List<Node>();
		yield return new WaitForSeconds(0.01f);
		for (int i = 0; i < _myNodes.Count; i++)
		{
			var tempNode = _myNodes[i].NodesToDeactivate();
			if (tempNode != null)
			{
				nodeToRemove.Add(tempNode);
				DestroyImmediate(tempNode.gameObject);
			}
		}

		for (int i = 0; i < nodeToRemove.Count; i++)
		{
			if (_myNodes.Contains(nodeToRemove[i]))
			{
				_myNodes.Remove(nodeToRemove[i]);
			}
		}
	}
}
