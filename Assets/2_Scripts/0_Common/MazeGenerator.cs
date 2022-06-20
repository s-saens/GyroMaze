using UnityEngine;
using System.Collections.Generic;

public class MazeGenerator
{
    private int[] dx = new int[4] { 0, 0, -1, 1 };
    private int[] dy = new int[4] { -1, 1, 0, 0 };

    private Maze maze;
    private int[,] distances; // [Y][X]
    private int maxDistance;

    private void InitDistances(int sizeY, int sizeX)
    {
        maxDistance = 0;
        distances = new int[sizeY, sizeX];
        for (int y = 0; y < sizeY; ++y) for (int x = 0; x < sizeX; ++x) distances[y, x] = 0;
    }

    public Maze MakeMazeDFS(int sizeX, int sizeY, int startX, int startY)
    {
        maze = new Maze(sizeX, sizeY);

        InitDistances(sizeX, sizeY);

        DFS(startX, startY, 1);

        return maze;
    }

    private void DFS(int x, int y, int dist)
    {
        distances[y, x] = dist;
        if(dist >= maxDistance)
        {
            maxDistance = dist;
            maze.endX = x;
            maze.endY = y;
        }

        List<int> directionToGo = new List<int>() {0, 1, 2, 3};
        for(int i=4 ; i>0 ; --i)
        {
            int index = Random.Range(0, i);
            int dir = directionToGo[index];
            directionToGo.RemoveAt(index);

            int nx = x + dx[dir];
            int ny = y + dy[dir];

            if (nx < 0 || nx >= maze.sizeX || ny < 0 || ny >= maze.sizeY) continue;
            if (distances[ny,nx] > 0) continue;

            // 벽 파괴
            if (dir == 0) {maze.horizontalWalls[y, x] = false;}
            else if (dir == 1) {maze.horizontalWalls[y + 1, x] = false;}
            else if (dir == 2) {maze.verticalWalls[y, x] = false;}
            else if (dir == 3) {maze.verticalWalls[y, x + 1] = false;}

            DFS(nx, ny, dist+1);
        }
    }
}