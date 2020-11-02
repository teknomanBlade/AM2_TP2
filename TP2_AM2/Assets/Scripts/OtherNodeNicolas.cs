using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherNodeNicolas : MonoBehaviour
{
	//G es la distancia recorrida desde el nodo inicial hasta este
	public float g;
	//H es una estimacion de la distancia que me falta para llegar al nodo final
	public float h;
	//F = G + H
	public float f;
	//Referencia al nodo anterior, lo usamos para reconstruir el camino
	public OtherNodeNicolas previous;
	//Todos los nodos a los que puedo ir desde este
	public List<OtherNodeNicolas> neighbors = new List<OtherNodeNicolas>();
	//Radio para detectar cuales nodos estan cerca
	public float radius;
	//Layer de los nodos
	public LayerMask nodesLayer;
	//Si el nodo esta bloqueado o si se puede caminar por el
	public bool isBlocked;
	//Una vez que ya tengo un camino paso esto a true solo para los gizmos
	public bool isPath;

	public void CheckNeighbors()
	{
		//Usamos un overlapSphere para obtener los nodos que tenemos en el radio indicado anteriormente 
		//y filtramos por la layer para no agarrar objetos de mas
		var temp = Physics.OverlapSphere(transform.localPosition, radius, nodesLayer, QueryTriggerInteraction.Collide);
		foreach (var item in temp)
		{
			//Nos aseguramos que no este bloqueado y que no sea el mismo en el que ya estamos
			//y los agregamos a la lista de vecinos
			var node = item.GetComponent<OtherNodeNicolas>();
			if (node && !node.isBlocked && node != this)
				neighbors.Add(node);
		}
	}

	public void ClearNode()
	{
		//Cada vez que vamos a calcular un camino nuevo reiniciamos los valores de G
		//y del previous para que no interfieran con los nuevos calculos
		g = Mathf.Infinity;
		previous = null;
	}

	//Esto detecta si el nodo esta adentro del nivel
	bool RaycastHit()
	{
		if (Physics.Raycast(transform.localPosition + Vector3.up, Vector3.down, 1f)) return true;

		return false;
	}

	//Esta función se comunica con el generador para decirle si el nodo esta adentro o no
	public OtherNodeNicolas NodesToDeactivate()
	{
		if (RaycastHit()) return null;
		return this;
	}


	private void OnDrawGizmos()
	{
		if (isBlocked)
		{
			Gizmos.color = Color.red;
			transform.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("MaterialNodeBlocked");
		}
		else
		{
			Gizmos.color = Color.white;
			transform.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("MaterialNodeUnblocked");
			//Gizmos.DrawWireSphere(transform.position, radious);
		}
		if (isPath)
		{
			Gizmos.color = Color.green;
			transform.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("MaterialNodePath");
			if (previous)
				Gizmos.DrawLine(transform.position, previous.transform.position);
		}
		if (RaycastHit()) //agregue esto para mostrar visualmente
		{
			Gizmos.color = Color.blue;
		}

		Gizmos.DrawWireSphere(transform.position, radius);

		/*foreach (var n in neighbors)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, n.transform.position);
        }*/


	}
}
