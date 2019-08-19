/*
 This algorithm is based on two phases Kill and Hunt. 
 It starts from the Kill phase. The algorithm starts from coordinates 0:0 and moves in random directions untill it hits a deadend. Once the cell is visited a flag is changed.
 Then comes the Hunt phase. The hunt phase search for unvisited cells and once it found unvisited cell start the kill phase again. When it hit the borders it start the hunt again.
 Looping this way unitll all cells are visited.
 */

using UnityEngine;

//HuntKillAlgorithm inheritance MazeBuilderAlgorithm
public class HuntKillAlgorithm : MazeBuilderAlgorithm
{
    private int currentRow = 0;
    private int currentCol = 0;
    private bool isComplete = false;

    public HuntKillAlgorithm(Cell[,] cells) : base(cells)
    {
    }

    public override void CreateMaze()
    {
        HuntKill();
    }

    private void HuntKill()
    {
        //cetting the current cell as visited
        cells[currentRow, currentCol].isVisited = true;

        //checking if the route is complete
        while (!isComplete)
        {
            KillWalls();
            HuntWalls();
        }
    }

#region KillWalls
    private void KillWalls()
    {
        //check around for not visited cells and if we are in boundaries
        while (RouteIsAvl(currentRow, currentCol))
        {
            //random from 1 to 4
            int dir = UnityEngine.Random.Range(1, 5);

            //CellAvl checks if its a valid cell and if we are in bounds
            //if we go north and its a valid cell(we never destroy north and west walls(bounds))
            if (dir == 1 && CellAvl(currentRow - 1, currentCol))
            {
                //destroy the South wall
                DestroyWall(cells[currentRow - 1, currentCol].sWall);
                currentRow--;
            }
            else if (dir == 2 && CellAvl(currentRow + 1, currentCol))
            {
                //go south and destroy the South wall
                DestroyWall(cells[currentRow, currentCol].sWall);
                currentRow++;
            }
            else if (dir == 3 && CellAvl(currentRow, currentCol + 1))
            {
                //go east and destroy
                DestroyWall(cells[currentRow, currentCol].eWall);
                currentCol++;
            }
            else if (dir == 4 && CellAvl(currentRow, currentCol - 1))
            {
                //go west and destroy east wall, because west is a bound
                DestroyWall(cells[currentRow, currentCol - 1].eWall);
                currentCol--;
            }

            //set the current cell to visited\
            cells[currentRow, currentCol].isVisited = true;
        }
    }

    private bool RouteIsAvl(int r, int c)
    {
        int avlRoutes = 0;

        if (r > 0 && !cells[r - 1, c].isVisited)
        {
            avlRoutes++;
        }
        
        if (r < rows - 1 && !cells[r + 1, c].isVisited)
        {
            avlRoutes++;
        }
        
        if (c > 0 && !cells[r, c - 1].isVisited)
        {
            avlRoutes++;
        }

        if (c < cols - 1 && !cells[r, c + 1].isVisited)
        {
            avlRoutes++;
        }

        return avlRoutes > 0;
    }

    private bool CellAvl(int r, int c)
    {
        if (r >= 0 && r < rows && c >= 0 && c < cols && !cells[r, c].isVisited)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DestroyWall(GameObject wall)
    {
        if (wall != null)
        {
            GameObject.Destroy(wall);
        }
    }
#endregion

 #region Hunt
    private void HuntWalls()
    {
        // if the round is complete
        isComplete = true;

        //go trough each row and col check if the cell is not visited and if the neighbor cells are visited 
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (!cells[r, c].isVisited && NeighborIsVisited(r, c))
                {
                    isComplete = false;
                    currentRow = r;
                    currentCol = c;
                    //get a random wall and destroy it
                    DestroyNeighborWall(currentRow, currentCol);
                    cells[currentRow, currentCol].isVisited = true;
                    return;
                }
            }
        }
    }

    private bool NeighborIsVisited(int r, int c)
    {
        int cellsVisited = 0;

        if (r > 0 && cells[r - 1, c].isVisited)
        {
            cellsVisited++;
        }

        if (r < (rows - 2) && cells[r + 1, c].isVisited)
        {
            cellsVisited++;
        }

        if (c > 0 && cells[r, c - 1].isVisited)
        {
            cellsVisited++;
        }

        if (c < (cols - 2) && cells[r, c + 1].isVisited)
        {
            cellsVisited++;
        }

        return cellsVisited > 0;
    }

    private void DestroyNeighborWall(int r, int c)
    {
        bool isDestroyedWall = false;

        while (!isDestroyedWall)
        {
            int dir = UnityEngine.Random.Range(1, 5);

            if (dir == 1 && r > 0 && cells[r - 1, c].isVisited)
            {
                DestroyWall(cells[r, c].nWall);
                DestroyWall(cells[r - 1, c].sWall);
                isDestroyedWall = true;
            }
            else if (dir == 2 && r < (rows - 2) && cells[r + 1, c].isVisited)
            {
                DestroyWall(cells[r, c].sWall);
                DestroyWall(cells[r + 1, c].nWall);
                isDestroyedWall = true;
            }
            else if (dir == 3 && c > 0 && cells[r, c - 1].isVisited)
            {
                DestroyWall(cells[r, c].wWall);
                DestroyWall(cells[r, c - 1].eWall);
                isDestroyedWall = true;
            }
            else if (dir == 4 && c < (cols - 2) && cells[r, c + 1].isVisited)
            {
                DestroyWall(cells[r, c].eWall);
                DestroyWall(cells[r, c + 1].wWall);
                isDestroyedWall = true;
            }
        }
    }
#endregion
}
