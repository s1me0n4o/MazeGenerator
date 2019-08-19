/*The point here is each time we add a cell, we are adding it to list and the new cell will be 
 created as random direction from the current cell*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingTree : MonoBehaviour
{
    [SerializeField]
    private GrowingTree mazePrefab;
    [SerializeField]
    private TreeMazeCell cellPrefab;
    [SerializeField]
    private TreeMazeWall wallPrefab;
    [SerializeField]
    private TreeMazePassage passagePrefab;

    private TreeMazeCell[,] cells;
    private float wallSize = 6f;
    private float delay = 0.01f;

    public GrowingTree mazeInstance;
    public IntVector2 size;

    public void StartGame(int inputRows, int inputCols)
    {
        size.r = inputRows;
        size.c = inputCols;

        mazeInstance = Instantiate(mazePrefab) as GrowingTree;
        StartCoroutine(mazeInstance.InstatiateMaze());
    }

    //getting random coordinates for the first cell
    private IntVector2 randomCoor
    {
        get
        {
            return new IntVector2(UnityEngine.Random.Range(0, size.r), UnityEngine.Random.Range(0, size.c));
        }
    }

    //check if the coordinates are in borders
    private bool ContainsCoor(IntVector2 coor)
    {
        return coor.r >= 0 && coor.r < size.r && coor.c >= 0 && coor.c < size.c;
    }

    public TreeMazeCell GetNewCell(IntVector2 coordinates)
    {
        return cells[coordinates.r, coordinates.c];
    }


    //creating the maze
    public IEnumerator InstatiateMaze()
    {
        WaitForSeconds creationDelay = new WaitForSeconds(delay);
        cells = new TreeMazeCell[size.r, size.c];
        //setting a list for backtracking mechanizm(in other words to keep track of the active cells)
        List<TreeMazeCell> activeCells = new List<TreeMazeCell>();
        DoFirstStep(activeCells);

        while (activeCells.Count > 0)
        {
            yield return creationDelay;
            DoNextStep(activeCells);
        }
    }

    //adding cell with random coordinates
    private void DoFirstStep(List<TreeMazeCell> activeCells)
    {
        activeCells.Add(CreateCell(randomCoor));
    }

    //getting the current cell, checks if its all edges are initialized and if so removing it from the list
    //a cell will be fully initialized only when all its neighbors have been visited.
    //in order to prevent incorrect walls, we should pick a random direction that is not yet initialized for current cell
    private void DoNextStep(List<TreeMazeCell> activeCells)
    {
        int curIndex = activeCells.Count - 1;

        TreeMazeCell curCell = activeCells[curIndex];
        if (curCell.IsFullyInit)
        {
            activeCells.RemoveAt(curIndex);
            return;
        }

        TreeDir direction = curCell.RandomNoDir;

        IntVector2 coor = curCell.coordinates + direction.ToIntVector2();

        if (ContainsCoor(coor))
        {
            // checking if the neighbor doesnt exists, if so we are creating it and adding a passage between the
            //neighbor and the current cell. If the neighbor already exists, separate them with wall.
            TreeMazeCell neighbor = GetNewCell(coor);
            if (neighbor == null)
            {
                neighbor = CreateCell(coor);
                CreatePassage(curCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else
            {
                CreateWall(curCell, neighbor, direction);
            }
        }
        else
        {
            CreateWall(curCell, null, direction);
        }
    }

    //creating a wall
    private void CreateWall(TreeMazeCell curCell, TreeMazeCell neighbor, TreeDir dir)
    {
        TreeMazeWall wall = Instantiate(wallPrefab) as TreeMazeWall;
        wall.Init(curCell, neighbor, dir);
        if (neighbor != null)
        {
            wall = Instantiate(wallPrefab) as TreeMazeWall;
            wall.Init(neighbor, curCell, dir.GetOppositeDir());
        }
    }

    //create a passage
    private void CreatePassage(TreeMazeCell curCell, TreeMazeCell neighbor, TreeDir dir)
    {
        TreeMazePassage passage = Instantiate(passagePrefab) as TreeMazePassage;
        passage.Init(curCell, neighbor, dir);
        passage = Instantiate(passagePrefab) as TreeMazePassage;
        passage.Init(neighbor, curCell, dir.GetOppositeDir());
    }

    //create a cell
    public TreeMazeCell CreateCell(IntVector2 coordinates)
    {
        TreeMazeCell newCell = Instantiate(cellPrefab) as TreeMazeCell;
        cells[coordinates.r, coordinates.c] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Tree Maze Cell " + coordinates.r + ", " + coordinates.c;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coordinates.r * wallSize, -(wallSize / 2f), coordinates.c * wallSize);
        //newCell.transform.Rotate(Vector3.right, 90f);
        return newCell;
    }
}
