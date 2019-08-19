using UnityEngine;

//In order to keep track of the cell connections, adding the follwing method. Each cell has 4 edges, each of them
//connects  to a neighbor cell, unless its a border. I will use the following abstract class for walls and passages.
public abstract class TreeCellEdges : MonoBehaviour
{
    public TreeMazeCell cell, otherCell;
    public TreeDir dir;

    public void Init (TreeMazeCell cell, TreeMazeCell otherCell, TreeDir dir)
    {
        this.cell = cell;
        this.otherCell = otherCell;
        this.dir = dir;
        cell.SetEdge(dir, this);
        transform.parent = cell.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = dir.ToRotation();
    }
}
