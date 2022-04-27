using UnityEngine;
using Newtonsoft.Json;

public class MazeFactory : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject ceilingPrefab;

    [SerializeField] private float spaceSize = 1; // World 내에서 한 칸의 길이
    [SerializeField] private float wallThickness = 0.1f; // 벽의 두께 = x scale(세로벽 기준)
    [SerializeField] private float wallHeight = 1f; // 벽의 높이 = y scale

    [SerializeField] private float panelThickness = 0.1f; // floor, ceiling의 두께 = y scale

    private Maze maze;

    private void Start() // TEST CODE
    {
        MazeGenerator gen = new MazeGenerator();
        Maze m = gen.MakeMazeDFS(100, 100, 1, 1);

        MakeMaze(m);
    }

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
    private void MakeStartPoint()
    {

    }
    private void MakeEndPoint()
    {

    }
}