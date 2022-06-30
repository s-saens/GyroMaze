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
        FindEndPoint(ref maze);

        return maze;
    }

    private void DFS(int x, int y, int dist)
    {
        distances[y, x] = dist;
        // if(dist >= maxDistance)
        // {
        //     maxDistance = dist;
        //     maze.endX = x;
        //     maze.endY = y;
        // }

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
            if (dir == 0) {maze.horizontalWalls[y, x] = false;} // 위
            else if (dir == 1) {maze.horizontalWalls[y + 1, x] = false;} // 아래
            else if (dir == 2) {maze.verticalWalls[y, x] = false;} // 왼쪽
            else if (dir == 3) {maze.verticalWalls[y, x + 1] = false;} // 오른쪽

            DFS(nx, ny, dist+1);
        }
    }

    private void FindEndPoint(ref Maze maze) // BFS
    {
        bool[,] visited = new bool[maze.sizeY, maze.sizeX];
        for (int y = 0; y < maze.sizeY; ++y) for (int x = 0; x < maze.sizeX; ++x) visited[y, x] = false;

        Queue<Vector2Int> q = new Queue<Vector2Int>();
        q.Enqueue(new Vector2Int(maze.startX, maze.startY));

        Vector2Int front = q.Peek();

        while(q.Count > 0)
        {
            front = q.Dequeue();
            visited[front.y, front.x] = true;

            for(int i=0 ; i<4 ; ++i)
            {
                if(i == 0 && maze.horizontalWalls[front.y, front.x]
                || i == 1 && maze.horizontalWalls[front.y + 1, front.x]
                || i == 2 && maze.verticalWalls[front.y, front.x]
                || i == 3 && maze.verticalWalls[front.y, front.x + 1]) continue;

                int nx = front.x + dx[i];
                int ny = front.y + dy[i];

                if (nx < 0 || nx >= maze.sizeX || ny < 0 || ny >= maze.sizeY || visited[ny, nx]) continue;

                q.Enqueue(new Vector2Int(nx, ny));
            }
        }

        maze.endX = front.x;
        maze.endY = front.y;
    }
}