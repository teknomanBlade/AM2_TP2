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

    public void ClearNodesMaterial() {
        foreach (var item in grid)
        {
            item.transform.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("MaterialNodeUnblocked");
        }
    }

    public void SetRandomBlockedNodes() {
        ClearNodesMaterial();

        for (int i = 0; i < grid.Count; i++)
        {
            var value = Random.Range(0f, 2f);
            if (value > 1.6f)
            {
                if (i == 0 || i == grid.Count - 1)
                {

                    grid[i].isBlocked = false;
                }
                else
                {
                    grid[i].transform.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("MaterialNodeBlocked");
                    grid[i].isBlocked = true;
                }

            }
            else
            {
                grid[i].isBlocked = false;
            }
        }
        /*foreach (var item in grid)
        {
            var value = Random.Range(0f, 5f);
            if (value > 2.5f)
            {
                if (item.name.Equals("Node") || item.name.Equals("Node (109)"))
                {
                    
                    item.isBlocked = false;
                }
                else
                {
                    item.transform.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("MaterialNodeBlocked");
                    item.isBlocked = true;
                }

            }
            else
            {
                item.isBlocked = false;
            }
        }*/
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
