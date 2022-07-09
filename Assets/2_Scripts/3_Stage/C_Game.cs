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
        if(!PopupIndicator.Instance.IsOn)
        {
            PopupIndicator.Instance.Show();
        }

        FirebaseDBAccessor.GetValue(
            FirebaseDBReference.Reference("maze", GameData.stageIndex.value.ToString()),
            (value) => {
                Maze maze = JsonConvert.DeserializeObject<Maze>(value);
                mazeFactory.MakeMaze(maze);
                PopupIndicator.Instance.Hide();
            }
        );
    }
}