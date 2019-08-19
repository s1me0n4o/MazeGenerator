using UnityEngine;

//in order to stor the directions i use enums.
public enum TreeDir
{
    North,
    East,
    South,
    West
}

public static class TreeDirections
{
    public const int Count = 4;

    //we will get a random direction
    public static TreeDir RandomVal
    {
        get
        {
            return (TreeDir)Random.Range(0, Count);
        }
    }

    //get random direction based on current coordinates
    private static IntVector2[] defineVecotrs = {
        new IntVector2(0, 1),
        new IntVector2(1, 0),
        new IntVector2(0, -1),
        new IntVector2(-1, 0)
    };

    public static IntVector2 ToIntVector2(this TreeDir dir)
    {
        return defineVecotrs[(int)dir];
    }

    //opposites directions
    private static TreeDir[] opposites = {
        TreeDir.South,
        TreeDir.West,
        TreeDir.North,
        TreeDir.East
    };

    public static TreeDir GetOppositeDir( this TreeDir dir)
    {
        return opposites[(int)dir];
    }

    //rotations
    private static Quaternion[] rotations = {
        Quaternion.identity,
        Quaternion.Euler(0f, 90f, 0f),
        Quaternion.Euler(0f, 180f, 0f),
        Quaternion.Euler(0f, 270f, 0f),
    };

    public static Quaternion ToRotation( this TreeDir dir)
    {
        return rotations[(int)dir];
    }
}