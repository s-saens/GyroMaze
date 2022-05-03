using UnityEngine;

public class C_Game : MonoBehaviour
{
    [SerializeField] Ball ball;

    [SerializeField] private MazeFactory mazeFactory;

    private void Start()
    {
        MazeGenerator mg = new MazeGenerator();
        
        mazeFactory.MakeMaze(mg.MakeMazeDFS(10, 10, 0, 0));
    }
}