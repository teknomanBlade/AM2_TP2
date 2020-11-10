using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AStar : MonoBehaviour
{
    private List<Node> _visited = new List<Node>();
    private List<Node> _notVisited = new List<Node>();
    private List<Node> _path = new List<Node>();
    private NodesGenerator _nodesGenerator;
    public NodesGenerator NodesGenerator {
        get {
            return _nodesGenerator;
        }
        set {
            _nodesGenerator = value;
        }
    }
    private Node initialNode;
    private Node finalNode;

    private void Start()
    {
        //_nodesGenerator = FindObjectOfType<NodesGenerator>();
    }

    public List<Node> GetPath(Node initial, Node finalNode)
    {
        _visited.Clear();
        foreach (var item in _nodesGenerator.grid)
        {
            item.ClearNode();
        }

        foreach (var item in _visited)
        {
            item.ClearNode();
        }

        foreach (var item in _notVisited)
        {
            item.ClearNode();
        }

        _path.Clear();
        _visited.Clear();
        _notVisited.Clear();
        _notVisited.Add(initial);
        initial.g = 0f;

        while (_notVisited.Count > 0)
        {
            Node current = SearchNextNode();
            _visited.Add(current);
            foreach (var item in current.neighbors)
            {
                if (_visited.Contains(item))
                    continue;

                if (!_notVisited.Contains(item))
                {
                    _notVisited.Add(item);
                    item.h = Mathf.Abs((finalNode.transform.position -
                        item.transform.position).magnitude);
                }
                float distanceToNeighbor =
                    Vector3.Distance(current.transform.position, item.transform.position);
                float newG = distanceToNeighbor + current.g;
                if (newG < item.g)
                {
                    item.g = newG;
                    item.previous = current;
                    item.f = item.g + item.h;
                }

                if (_notVisited.Contains(finalNode))
                {
                    _path.Add(finalNode);
                    Node node = finalNode.previous;
                    while (node)
                    {
                        _path.Insert(0, node);
                        node = node.previous;
                    }
                }
            }
        }
        return _path;
    }

    private Node SearchNextNode()
    {
        Node n = _notVisited[0];
        for (int i = 0; i < _notVisited.Count; i++)
        {
            if (_notVisited[i].f < n.f)
            {
                n = _notVisited[i];
            }
        }
        _notVisited.Remove(n);
        return n;
    }
}
