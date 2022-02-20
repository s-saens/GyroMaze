//C# Example
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;


/*
 - 가장자리를 모두 막기 (size는 이걸 포함하지 않은 size.)
 - 2x+1개.
 - 홀수 번째 칸만 움직일 수 있음.
*/

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
    None,
}


public class Node
{
    private Vector2Int position = new Vector2Int(1, 1);
    public Vector2Int Position
    {
        get { return position; }
    }

    private bool visited = false;
    public bool Visited
    {
        get { return visited; }
        set { visited = value; }
    }

    public Dictionary<Direction, Node> adjacentNodes = new Dictionary<Direction, Node>();
    public Dictionary<Direction, GameObject> adjacentWalls = new Dictionary<Direction, GameObject>();

    public Node(int x, int y)
    {
        this.position.x = x;
        this.position.y = y;
        this.visited = false;
    }
}


public class MazeGenerator : EditorWindow
{
    // Inputs
    private Vector2Int size = new Vector2Int(1, 1);
    private GameObject horizontalWallPrefab;
    private GameObject verticalWallPrefeb;
    private Transform parentTransform;




    private List<List<Node>> nodes = new List<List<Node>>();

    private List<List<GameObject>> horizontalWalls = new List<List<GameObject>>();
    private List<List<GameObject>> verticalWalls = new List<List<GameObject>>();

    private void Clear()
    {
        nodes.Clear();
        horizontalWalls.Clear();
        verticalWalls.Clear();
    }

    private void Generate()
    {
        Clear();

        SetNodesList();
        SetWallsList();
        // SetAdjacentNodesAndWalls();

        // SetPath(nodes[0][0]);
    }

    private void DestroyAll()
    {
        int c = parentTransform.childCount;
        for(int i=0 ; i<c ; ++i)
        {
            DestroyImmediate(parentTransform.GetChild(0).gameObject);
        }
    }

    private void SetNodesList()
    {
        for (int y = 0; y < size.y; ++y)
        {
            List<Node> nodes1dim = new List<Node>();

            for (int x = 0; x < size.x; ++x)
            {
                Node n = new Node(x, y);
                nodes1dim.Add(n);
            }

            nodes.Add(nodes1dim);
        }
    }
    private void SetWallsList()
    {
        // Horizontal
        for (int y = 0; y < size.y+1; ++y)
        {
            List<GameObject> horWalls1dim = new List<GameObject>();

            for (int x = 0; x < size.x; ++x)
            {
                GameObject hor_w;
                    hor_w = Instantiate(horizontalWallPrefab, parentTransform);
                    hor_w.transform.localPosition = new Vector3(x, 0, y - 0.5f);
                    horWalls1dim.Add(hor_w);
                    hor_w.transform.name = $"wall_hor ({x}, {y})";

            }

            horizontalWalls.Add(horWalls1dim);
        }


        // Vertical
        // Horizontal
        for (int y = 0; y < size.y; ++y)
        {
            List<GameObject> verWalls1dim = new List<GameObject>();

            for (int x = 0; x < size.x; ++x)
            {
                GameObject ver_w;
                if (x == 0)
                {
                    ver_w = Instantiate(verticalWallPrefeb, parentTransform);
                    ver_w.transform.localPosition = new Vector3(x - 0.5f, 0, y);
                    verWalls1dim.Add(ver_w);
                    ver_w.transform.name = $"wall_ver ({x}, {y})";
                }
                ver_w = Instantiate(verticalWallPrefeb, parentTransform);
                ver_w.transform.localPosition = new Vector3(x + 0.5f, 0, y);
                verWalls1dim.Add(ver_w);
                ver_w.transform.name = $"wall_ver ({x}, {y})";
            }
            verticalWalls.Add(verWalls1dim);
        }
    }

    private void SetAdjacentNodesAndWalls()
    {
        for (int y = 0; y < size.y; ++y)
        {
            for (int x = 0; x < size.x; ++x)
            {
                Node n = nodes[y][x];

                // Node 추가

                if (x > 0) // 왼쪽 추가할 수 있는 경우
                {
                    n.adjacentNodes.Add(Direction.Left, nodes[y][x-1]);
                }
                if (x < size.x - 1) // 오른쪽 ~
                {
                    n.adjacentNodes.Add(Direction.Right, nodes[y][x+1]);
                }
                if (y > 0) // 아래 ~
                {
                    n.adjacentNodes.Add(Direction.Down, nodes[y-1][x]);
                }
                if (y < size.y - 1) // 위 ~
                {
                    n.adjacentNodes.Add(Direction.Up, nodes[y+1][x]);
                }

                // Walls 추가
                Debug.Log($"Horizontal : {horizontalWalls.Count} x {horizontalWalls[0].Count}");
                Debug.Log($"Vertical : {verticalWalls.Count} x {verticalWalls[0].Count}");
                // 왼쪽
                n.adjacentWalls.Add(Direction.Left, verticalWalls[y][x]);
                // 오른쪽
                n.adjacentWalls.Add(Direction.Right, verticalWalls[y][x+1]);
                // 아래
                n.adjacentWalls.Add(Direction.Down, horizontalWalls[y+1][x]);
                // 위
                n.adjacentWalls.Add(Direction.Up, horizontalWalls[y][x]);
            }
        }
    }


    private void SetPath(Node nowNode)
    {
        if(nowNode.Visited == true)
        {
            return;
        }
        nowNode.Visited = true;

        while(true)
        {
            Direction dir = RandomNextDirection(nodes[0][0]);
            if (dir == Direction.None) // 더는 이동할 수 없을 때.
            {
                return;
            }
            nowNode.adjacentWalls[dir].SetActive(false);
            SetPath(nowNode.adjacentNodes[dir]);
        }
    }


    private Direction RandomNextDirection(Node lastNode)
    {
        Direction dir = (Direction)Random.Range(0, 4);
        for (int i = 0; i < 4; ++i)
        {
            if (lastNode.adjacentNodes.ContainsKey(dir))
            {
                if (lastNode.adjacentNodes[dir].Visited == false)
                {
                    return dir;
                }
            }
            dir = (Direction)((int)(dir + 1) % 4);
        }

        return Direction.None;
    }






    // Add menu item named "My Window" to the Window menu
    [MenuItem("SAENS/MazeGenerator")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(MazeGenerator));
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();

        size = EditorGUILayout.Vector2IntField("Maze Size", size);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        horizontalWallPrefab = (GameObject)EditorGUILayout.ObjectField("Wall - Horizontal", horizontalWallPrefab, typeof(GameObject), false);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        verticalWallPrefeb = (GameObject)EditorGUILayout.ObjectField("Wall - Vertical", verticalWallPrefeb, typeof(GameObject), false);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        parentTransform = (Transform)EditorGUILayout.ObjectField("Parent Transform", parentTransform, typeof(Transform), true);

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();

        if(GUILayout.Button("Generate"))
        {
            Generate();
        }
        if (GUILayout.Button("Destroy"))
        {
            DestroyAll();
        }


        EditorGUILayout.EndHorizontal();
    }
}