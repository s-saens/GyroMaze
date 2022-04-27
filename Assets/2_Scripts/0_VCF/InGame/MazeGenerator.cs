using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze
{
    public int sizeX, sizeY;
    public bool[,] horizontalWalls; // [Y+1][X]
    public bool[,] verticalWalls; // [Y][X+1]
    public int[,] distances; // [Y][X]
    public int startX, startY;
    public int endX, endY;

    public Maze(int sizeX, int sizeY)
    {
        this.sizeX = sizeX;
        this.sizeY = sizeY;

        horizontalWalls = new bool[sizeY + 1, sizeX];
        verticalWalls = new bool[sizeY, sizeX + 1];
        distances = new int[sizeY, sizeX];

        for(int y=0 ; y<sizeY + 1 ; ++y) for(int x=0 ; x<sizeX ; ++x) horizontalWalls[y, x] = true;
        for(int y=0 ; y<sizeY ; ++y) for(int x=0 ; x<sizeX + 1 ; ++x) verticalWalls[y, x] = true;
        for(int y=0 ; y<sizeY ; ++y) for(int x=0 ; x<sizeX ; ++x) distances[y, x] = 0;
    }
}

public class MazeGenerator
{
    private int[] dx = new int[4] { 0, 0, -1, 1 };
    private int[] dy = new int[4] { -1, 1, 0, 0 };
    private int[] dxw = new int[4] { 0, 0, 1, 0 };
    private int[] dyw = new int[4] { 1, 0, 0, 0 };

    Maze maze;
    int maxDistance = 0;

    public Maze MakeMazeDFS(int sizeX, int sizeY, int startX, int startY)
    {
        maze = new Maze(sizeX, sizeY);

        DFS(startX, startY, 1, -1);
        Debug.Log(maze);

        return maze;
    }

    private void DFS(int x, int y, int dist, int lastDir)
    {
        if (x < 0 || x >= maze.sizeX || y < 0 || y >= maze.sizeY || maze.distances[y, x] > 0) return;

        maze.distances[y, x] = dist;
        maxDistance = Mathf.Max(maxDistance, dist);

        // 벽 파괴
        if (lastDir >= 0)
        {
            if(lastDir == 0) maze.horizontalWalls[y + 1, x] = false;
            else if(lastDir == 1) maze.horizontalWalls[y, x] = false;
            else if(lastDir == 2) maze.verticalWalls[y, x+1] = false;
            else if(lastDir == 3) maze.verticalWalls[y, x] = false;

            Debug.Log("!");
        }

        List<int> directionToGo = new List<int>() {0, 1, 2, 3};

        while(directionToGo.Count > 0)
        {
            int index = Random.Range(0, directionToGo.Count);
            int dir = directionToGo[index];
            directionToGo.RemoveAt(index);
            
            int nx = x + dx[dir];
            int ny = y + dy[dir];

            DFS(nx, ny, dist+1, dir);
        }
    }
}