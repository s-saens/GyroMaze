using System.Reflection.Emit;
using UnityEngine;
using Newtonsoft.Json;

public class MazeFactory : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject ceilingPrefab;

    [SerializeField] private float spaceSize = 1; // World 내에서 한 칸의 길이
    [SerializeField] private float wallThickness = 0.1f; // 벽의 두께 = x scale(세로벽 기준)
    [SerializeField] private float wallHeight = 0.1f; // 벽의 높이 = y scale

    [SerializeField] private float panelThickness = 0.1f; // floor, ceiling의 두께 = y scale

    private Maze maze;

    private void Start() // TEST CODE
    {
        Maze m = new Maze();

        maze.X = 4;
        maze.Y = 4;
        maze.horizontalWalls = new bool[5][] {  new bool[4] {true, true, true, true },
                                                new bool[4] {false, true, false, false },
                                                new bool[4] {true, false, true, false },
                                                new bool[4] {false, true, false, false },
                                                new bool[4] {true, true, true, true } };

        maze.verticalWalls = new bool[4][] {    new bool[5] {true, true, false, false, true },
                                                new bool[5] {true, false, false, true, true },
                                                new bool[5] {true, false, true, true, true },
                                                new bool[5] {true, false, true, false, true } };

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
        MakeHorizontalWalls();
        MakeVerticalWalls();
    }

    private void MakeFloor()
    {
        GameObject floor = Instantiate(floorPrefab, this.transform);
        float sizeX = spaceSize * maze.X + wallThickness;
        float sizeZ = spaceSize * maze.Y + wallThickness;
        floor.transform.localScale = new Vector3(sizeX, panelThickness, sizeZ);
    }
    private void MakeCeiling()
    {
        GameObject ceiling = Instantiate(ceilingPrefab, this.transform);
        float sizeX = spaceSize * maze.X + wallThickness;
        float sizeZ = spaceSize * maze.Y + wallThickness;
        ceiling.transform.localScale = new Vector3(sizeX, panelThickness, sizeZ);
    }

    private void MakeHorizontalWalls()
    {
        // 행 고정 후 x좌표 순회
        for(int y = 0 ; y < maze.Y + 1 ; ++y)
        {
            int seq = 0;
            for(int x = 0 ; x < maze.X ; ++x)
            {
                bool isWall = maze.horizontalWalls[y][x];

                if(isWall)
                {
                    seq++;
                }
                else
                {
                    if (seq > 0 || x == maze.X - 1)
                    {
                        GameObject wall = Instantiate(wallPrefab, this.transform);

                        // Set Scale
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
        for(int x = 0 ; x < maze.X + 1 ; ++x)
        {
            int seq = 0;
            for(int y = 0 ; y < maze.Y ; ++y)
            {
                bool isWall = maze.horizontalWalls[y][x];

                if (isWall)
                {
                    seq++;
                }
                else
                {
                    if (seq > 0 || x == maze.X - 1)
                    {
                        GameObject wall = Instantiate(wallPrefab, this.transform);

                        // Set Scale
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
}