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
    public int maxDistance = 0;

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

    private Maze maze;

    public Maze MakeMazeDFS(int sizeX, int sizeY, int startX, int startY)
    {
        maze = new Maze(sizeX, sizeY);

        DFS(startX, startY, 1);

        return maze;
    }

    private void DFS(int x, int y, int dist)
    {
        maze.distances[y, x] = dist;
        if(dist >= maze.maxDistance)
        {
            maze.maxDistance = dist;
            maze.endX = x;
            maze.endY = y;
        }

        List<int> directionToGo = new List<int>() {0, 1, 2, 3};
        int cnt = 4;
        while(cnt > 0)
        {
            int index = Random.Range(0, cnt);
            int dir = directionToGo[index];
            directionToGo.RemoveAt(index);
            cnt--;

            int nx = x + dx[dir];
            int ny = y + dy[dir];

            if (nx < 0 || nx >= maze.sizeX || ny < 0 || ny >= maze.sizeY || maze.distances[ny, nx] > 0) continue;

            // 벽 파괴
            if (dir == 0) {maze.horizontalWalls[y, x] = false;}
            else if (dir == 1) {maze.horizontalWalls[y + 1, x] = false;}
            else if (dir == 2) {maze.verticalWalls[y, x] = false;}
            else if (dir == 3) {maze.verticalWalls[y, x + 1] = false;}

            DFS(nx, ny, dist+1);
        }
    }
}