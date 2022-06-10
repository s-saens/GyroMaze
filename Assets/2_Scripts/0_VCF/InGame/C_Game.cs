using UnityEngine;

public class C_Game : MonoBehaviour
{
    [SerializeField] Ball ball;

    [SerializeField] private MazeFactory mazeFactory;

    private void Start()
    {
        MazeGenerator mg = new MazeGenerator();
        Maze maze = mg.MakeMazeDFS(10, 10, 4, 4);
        mazeFactory.MakeMaze(maze);
        Debug.Log(Newtonsoft.Json.JsonConvert.SerializeObject(maze));
    }
}