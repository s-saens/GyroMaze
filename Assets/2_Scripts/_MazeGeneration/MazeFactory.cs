using UnityEngine;
using Newtonsoft.Json;

public class MazeFactory : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject floorPrefab;

    [SerializeField] private float spaceSize = 1;
    [SerializeField] private float wallThickness = 0.1f;

    private Maze maze;

    public void MakeMaze(Maze m)
    {
        maze = m;
        
        Make();
    }
    public void MakeMaze(string m)
    {
        maze = JsonConvert.DeserializeObject<Maze>(m);
        Make();
    }

    private void Make()
    {
        MakeHorizontalWalls();
        MakeVerticalWalls();

        MakeFloor();
        MakeBallAtStartPoint();
        MakeEndPoint();
    }

    private void MakeHorizontalWalls()
    {
        // 행 고정 후 x좌표 순회
        for(int y = 0 ; y < maze.sizeY + 1 ; ++y)
        {
            int seq = 0;
            for(int x = 0 ; x < maze.sizeX ; ++x)
            {
                bool isWall = maze.horizontalWalls[y,x];

                if(isWall && x < maze.sizeX - 1)
                {
                    seq++;
                }
                else
                {
                    if (seq > 0 || x == maze.sizeX - 1)
                    {
                        GameObject wall = Instantiate(wallPrefab, this.transform);

                        // Set Scale
                        if (x == maze.sizeX - 1 && isWall)
                        {
                            x++;
                            seq++;
                        }

                        float wallLength = spaceSize * seq + wallThickness;
                        wall.transform.localScale = new Vector2(wallLength, wallThickness);

                        // Set Position
                        float posX = spaceSize * x - (spaceSize * seq * 0.5f);
                        float posY = y * spaceSize;
                        wall.transform.localPosition = new Vector2(posX, posY);
                    }

                    seq = 0;
                }
            }
        }
    }
    private void MakeVerticalWalls()
    {
        // 열 고정 후 y좌표 순회 (World에서는 z좌표임)
        for(int x = 0 ; x < maze.sizeX + 1 ; ++x)
        {
            int seq = 0;
            for(int y = 0 ; y < maze.sizeY ; ++y)
            {
                bool isWall = maze.verticalWalls[y,x];

                if (isWall && y < maze.sizeY - 1)
                {
                    seq++;
                }
                else
                {
                    if (seq > 0 || y == maze.sizeY - 1)
                    {
                        GameObject wall = Instantiate(wallPrefab, this.transform);

                        // Set Scale
                        if (y == maze.sizeY - 1 && isWall)
                        {
                            y++;
                            seq++;
                        }

                        float wallLength = spaceSize * seq + wallThickness;
                        wall.transform.localScale = new Vector2(wallThickness, wallLength);

                        // Set Position
                        float posX = x * spaceSize;
                        float posY = spaceSize * y - (spaceSize * seq * 0.5f);
                        wall.transform.localPosition = new Vector2(posX, posY);
                    }

                    seq = 0;
                }
            }
        }
    }

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject endPointPrefab;


    private void MakeFloor()
    {
        GameObject floor = Instantiate(floorPrefab, this.transform);

        float sizeX = spaceSize * maze.sizeX + wallThickness;
        float sizeY = spaceSize * maze.sizeY + wallThickness;
        floor.transform.localScale = new Vector2(sizeX, sizeY);
        float posX = spaceSize * maze.sizeX * 0.5f;
        float posY = spaceSize * maze.sizeY * 0.5f;
        floor.transform.localPosition = new Vector3(posX, posY, 1);
    }

    private void MakeBallAtStartPoint()
    {
        Instantiate(ballPrefab, this.transform);

        Vector3 ballPos = new Vector3(maze.startX + (spaceSize * 0.5f), maze.startY + (spaceSize * 0.5f), -2);

        if(PlayerPrefs.HasKey(KeyData.LAST_POSITION))
        {
            ballPrefab.transform.position = PlayerPrefsExt.GetObject<Vector3>(KeyData.LAST_POSITION, ballPos);
        }
        ballPrefab.transform.position = ballPos;
    }
    private void MakeEndPoint()
    {
        GameObject endPoint = Instantiate(endPointPrefab, this.transform);
        endPoint.transform.position = new Vector3(maze.endX + (spaceSize * 0.5f), maze.endY + (spaceSize * 0.5f), -1);
    }
}