using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NodesGenerator : MonoBehaviour
{
    public static bool IsFirst = true;
    public static int Counter = 0;
    public List<Node> grid = new List<Node>();

    // Start is called before the first frame update
    void Start()
    {
        SetRandomBlockedNodes();
        StartCoroutine(DrawNeighbours());
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void SetRandomBlockedNodes() {
        foreach (var item in grid)
        {
            var value = Random.Range(0, 7);
            if (value > 3)
            {
                if (item.name.Equals("Node") || item.name.Equals("Node (109)"))
                {
                    item.isBlocked = false;
                }
                else
                {
                    item.isBlocked = true;
                }

            }
            else
            {
                item.isBlocked = false;
            }
        }
    }

    public void DrawNeighboursForEditor() {
        Debug.Log("My Grid Length " + grid.Count);
        foreach (var e in grid)
        {
            e.CheckNeighbors();
        }
    }

    IEnumerator DrawNeighbours()
    {
        yield return new WaitForSeconds(0.2f);
        Debug.Log("My Grid Length " + grid.Count);
        foreach (var e in grid)
        {
            e.CheckNeighbors();
        }
    }
}
