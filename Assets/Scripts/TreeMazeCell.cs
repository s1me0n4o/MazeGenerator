using UnityEngine;


public class TreeMazeCell : MonoBehaviour
{
    public IntVector2 coordinates;

    private TreeCellEdges[] edges = new TreeCellEdges[TreeDirections.Count];

    private int initEdgeCount;

    //check if a cell is fully initialized, based on track how often an edge has been set
    public bool IsFullyInit
    {
        get
        {
            return initEdgeCount == TreeDirections.Count;
        }
    }

    //getting edge based on the direction
    public TreeCellEdges GetEdge (TreeDir dir)
    {
        return edges[(int)dir];
    }

    //seting edge
    public void SetEdge (TreeDir dir, TreeCellEdges edge)
    {
        edges[(int)dir] = edge;
        initEdgeCount += 1;
    }

    //randomly deside how manyunitialized dirs we should skip, then we are looping through the array and whenever
    //we find a hole we check whether we are out of skips. if so this is our direction, otherwise decrease skips
    public TreeDir RandomNoDir
    {
        get
        {
            int skip = Random.Range(0, TreeDirections.Count - initEdgeCount);
            for (int i = 0; i < TreeDirections.Count; i++)
            {
                if (edges[i] == null)
                {
                    if (skip == 0)
                    {
                        return (TreeDir)i;
                    }
                    skip -= 1;      
                }
            }
            throw new System.InvalidOperationException("The Cell has no more dirs left");
        }
    }
}
