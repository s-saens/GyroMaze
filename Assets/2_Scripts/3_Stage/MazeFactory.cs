using UnityEngine;
using Newtonsoft.Json;

public class MazeFactory : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject ceilingPrefab;

    [SerializeField] private float spaceSize = 1; // World 내에서 한 칸의 길이
    [SerializeField] private float wallThickness = 0.1f; // 벽의 두께 = x scale(세로벽 기준)
    [SerializeField] private float wallHeight = 2f; // 벽의 높이 = y scale

    [SerializeField] private float panelThickness = 0.1f; // floor, ceiling의 두께 = y scale

    private Maze maze;

    public void MakeMaze(Maze m)
    {
        maze = m;
        Make();
    }
    public void MakeMaze(string m)
    {
        maze = JsonConvert.DeserializeObject<Maze>(m, JsonSettings.Settings);
        Make();
    }

    private void Make()
    {
        MakeFloor();
        MakeCeiling();

        MakeHorizontalWalls();
        MakeVerticalWalls();

        MakeStartPoint();
        MakeEndPoint();
    }

    private void MakeFloor()
    {
        GameObject floor = Instantiate(floorPrefab, this.transform);
        float sizeX = spaceSize * maze.sizeX + wallThickness;
        float sizeZ = spaceSize * maze.sizeY + wallThickness;
        floor.transform.localScale = new Vector3(sizeX, panelThickness, sizeZ);
        float posX = spaceSize * maze.sizeX * 0.5f;
        float posZ = spaceSize * maze.sizeY * 0.5f;
        floor.transform.localPosition = new Vector3(posX, -(wallHeight+panelThickness) * 0.5f, posZ);
    }
    private void MakeCeiling()
    {
        GameObject ceiling = Instantiate(ceilingPrefab, this.transform);
        float sizeX = spaceSize * maze.sizeX + wallThickness;
        float sizeZ = spaceSize * maze.sizeY + wallThickness;
        ceiling.transform.localScale = new Vector3(sizeX, panelThickness, sizeZ);
        float posX = spaceSize * maze.sizeX * 0.5f;
        float posZ = spaceSize * maze.sizeY * 0.5f;
        ceiling.transform.localPosition = new Vector3(posX, (wallHeight+ panelThickness) * 0.5f, posZ);
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
                        wall.transform.localScale = new Vector3(wallLength, wallHeight, wallThickness);

                        // Set Position
                        float posX = spaceSize * x - (spaceSize * seq * 0.5f);
                        float posZ = y * spaceSize;
                        wall.transform.localPosition = new Vector3(posX, 0, posZ);
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
                        wall.transform.localScale = new Vector3(wallThickness, wallHeight, wallLength);

                        // Set Position
                        float posX = x * spaceSize;
                        float posZ = spaceSize * y - (spaceSize * seq * 0.5f);
                        wall.transform.localPosition = new Vector3(posX, 0, posZ);
                    }

                    seq = 0;
                }
            }
        }
    }

    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject endPoint;

    private void MakeStartPoint()
    {
        ball.SetActive(true);
        Vector3 ballPosition = new Vector3(maze.startX + (spaceSize * 0.5f), 0, maze.startY + (spaceSize * 0.5f));
        if(PlayerPrefs.HasKey(KeyData.LAST_POSITION))
        {
            ballPosition = PlayerPrefsExt.GetObject<Vector3>(KeyData.LAST_POSITION, Vector3.one * 0.5f);
            PlayerPrefs.DeleteKey(KeyData.LAST_POSITION);
        }
        ball.transform.position = ballPosition;
    }
    private void MakeEndPoint()
    {
        endPoint.transform.position = new Vector3(maze.endX + (spaceSize * 0.5f), 0, maze.endY + (spaceSize * 0.5f));
    }
}