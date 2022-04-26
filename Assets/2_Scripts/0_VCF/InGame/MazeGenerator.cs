using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze
{
    public int X, Y;
    public bool[][] horizontalWalls;
    public bool[][] verticalWalls;
    public int[][] distances;
}

public class MazeGenerator
{
    private int[] dx = new int[4] { 0, 0, 1, 1 };
    private int[] dy = new int[4] { 0, 1, 0, 1 };

    
}
