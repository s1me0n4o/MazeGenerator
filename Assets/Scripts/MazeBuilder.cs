using UnityEngine;
using System.Collections.Generic;
using System;

public class MazeBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject wallPrefab;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private float size = 6f;

    private int rows, cols;
    private Cell[,] cells;

    public static List<GameObject> innerCells;


    public void BuildBasicMaze(int choise, int inputRows, int inputCols)
    {
        rows = inputRows;
        cols = inputCols;

        if (choise == 1)
        {
            //initializing the maze with all of the cells
            Initialize();

            HuntAndKillAlg();
        }
        else if (choise == 2)
        {
            prefab.GetComponent<GrowingTree>().StartGame(rows, cols);
            //GrowTreeAlgorithm gta = new GrowTreeAlgorithm(cells);
            //gta.CreateMaze();
        }
    }

    private void HuntAndKillAlg()
    {
        //Unfortunately i am not able to start a coroutine when implementing an abstract class
        //startin the hunt and kill algorithm
        MazeBuilderAlgorithm mazeBuilderAlg = new HuntKillAlgorithm(cells);
        mazeBuilderAlg.CreateMaze();
    }

    private void Initialize()
    {

        innerCells = new List<GameObject>();

        cells = new Cell[rows, cols];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {

                cells[r, c] = new Cell();

                // in order to avoid overlaping walls we are creating only South and East walls as inner walls

                // create the floor 
                CreateFloor(r, c);

                //create the West walls only if we are at column 0
                if (c == 0)
                {
                    CreateWestWall(r, c);
                }

                //create East walls
                CreateEastWall(r, c);

                //if we are at the row 0 create a Noth wall
                if (r == 0)
                {
                    CreateNorthWall(r, c);
                }

                //create South walls
                CreateSouthWall(r, c);
                
            }
        }

    }

#region CreateWalls
    private void CreateSouthWall(int r, int c)
    {
        cells[r, c].sWall = Instantiate(wallPrefab,
                        new Vector3((r * size) + (size / 2f), 0, c * size),
                        Quaternion.identity) as GameObject;
        cells[r, c].sWall.name = "South " + r + " , " + c;
        cells[r, c].sWall.transform.Rotate(Vector3.up * 90f);
        innerCells.Add(cells[r, c].sWall);
    }

    private void CreateNorthWall(int r, int c)
    {
        cells[r, c].nWall = Instantiate(wallPrefab,
                                        new Vector3((r * size) - (size / 2f), 0, c * size),
                                        Quaternion.identity) as GameObject;
        cells[r, c].nWall.name = "North " + r + " , " + c;
        cells[r, c].nWall.transform.Rotate(Vector3.up, 90f);
    }

    private void CreateEastWall(int r, int c)
    {
        cells[r, c].eWall = Instantiate(wallPrefab,
                                        new Vector3(r * size, 0, (c * size) + (size / 2f)),
                                        Quaternion.identity) as GameObject;
        cells[r, c].eWall.name = "East " + r + " , " + c;
        innerCells.Add(cells[r, c].eWall);
    }

    private void CreateWestWall(int r, int c)
    {
        cells[r, c].wWall = Instantiate(wallPrefab,
                                        new Vector3(r * size, 0, (c * size) - (size / 2f)),
                                        Quaternion.identity) as GameObject;
        cells[r, c].wWall.name = "West " + r + " , " + c;
    }

    private void CreateFloor(int r, int c)
    {
        cells[r, c].floor = Instantiate(wallPrefab,
                                        new Vector3(r * size, -(size / 2f), c * size),
                                        Quaternion.identity) as GameObject;
        cells[r, c].floor.name = "F " + r + " , " + c;
        cells[r, c].floor.transform.Rotate(Vector3.right, 90f);
    }
 #endregion
}
