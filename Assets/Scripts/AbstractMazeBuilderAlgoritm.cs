public abstract class MazeBuilderAlgorithm
{
    protected Cell[,] cells;
    protected int rows, cols;

    //Constructor
    protected MazeBuilderAlgorithm(Cell[,] cells) : base()
    {
        this.cells = cells;
        rows = cells.GetLength(0);
        cols = cells.GetLength(1);
    }

    public abstract void CreateMaze();
}