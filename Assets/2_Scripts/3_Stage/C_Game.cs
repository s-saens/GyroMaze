using UnityEngine;
using Newtonsoft.Json;
using Firebase.Database;
using System;
using System.Threading.Tasks;
using Firebase.Extensions;

public class C_Game : MonoBehaviour
{
    [SerializeField] Ball ball;

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
#endif
        if(!IndicatorController.Instance.IsOn)
        {
            IndicatorController.Instance.Show();
        }

        FirebaseDBAccessor.GetValue(
            FirebaseDBReference.Reference("maze", GameData.stageIndex.value.ToString()),
            (value) => {
                Maze maze = JsonConvert.DeserializeObject<Maze>(value);
                mazeFactory.MakeMaze(maze);
                IndicatorController.Instance.Hide();
            }
        );
    }
}