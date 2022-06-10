using UnityEngine;

public class Maze
{
    public int sizeX, sizeY;
    public bool[,] horizontalWalls; // [Y+1][X]
    public bool[,] verticalWalls; // [Y][X+1]
    public int startX, startY;
    public int endX, endY;

    public Maze(int sizeX, int sizeY)
    {
        this.sizeX = sizeX;
        this.sizeY = sizeY;

        horizontalWalls = new bool[sizeY + 1, sizeX];
        verticalWalls = new bool[sizeY, sizeX + 1];

        for (int y = 0; y < sizeY + 1; ++y) for (int x = 0; x < sizeX; ++x) horizontalWalls[y, x] = true;
        for (int y = 0; y < sizeY; ++y) for (int x = 0; x < sizeX + 1; ++x) verticalWalls[y, x] = true;
    }
}
