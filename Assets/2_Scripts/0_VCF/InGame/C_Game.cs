using UnityEngine;
using Newtonsoft.Json;
using Firebase.Database;
using System;
using System.Threading.Tasks;
using Firebase.Extensions;

public class C_Game : MonoBehaviour
{
    [SerializeField] Ball ball;

    [SerializeField] private int stage;
    [SerializeField] private MazeFactory mazeFactory;


    private void Start()
    {
        MakeMaze();
    }

    private void MakeMaze()
    {
#if UNITY_EDITOR
        FirebaseInit fi = new FirebaseInit();
        fi.Init();
        C_Indicator.Instance.ShowIndicator();
#endif
        DatabaseReference mazeReference = DBRef.maze.Child(stage.ToString());
        Task t = mazeReference.GetValueAsync().ContinueWithOnMainThread((task) =>
        {
            if (task.IsCompleted)
            {
                if (task.Result.Value == null)
                {
                    Debug.LogError($"THERE IS NO MAZE OF STAGE {stage} ON DATABASE");
                    return;
                }
                Maze maze = JsonConvert.DeserializeObject<Maze>(task.Result.GetRawJsonValue());
                mazeFactory.MakeMaze(maze);
                C_Indicator.Instance.HideIndicator();
            }
        });
        t.LogExceptionIfFaulted();
    }
}