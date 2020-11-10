using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IALauncher : MonoBehaviour
{
    public Node _initialNode;
    public Node _endNode;
    private AStar _aStar;
    public List<Node> path;
    // Start is called before the first frame update
    void Start()
    {
        _aStar = FindObjectOfType<AStar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            path = _aStar.GetPath(_initialNode, _endNode);
            foreach (var item in path)
            {
                item.IsPath = true;
            }
        }
    }
}
