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
        PlayerPrefs.SetInt(KeyData.LAST_STAGE, GameData.stageIndex.value);
    }

    private void MakeMaze()
    {
        if(!PopupIndicator.Instance.IsOn)
        {
            PopupIndicator.Instance.Show();
        }

        FirebaseDBAccessor.GetValue<Maze>(
            FirebaseDBReference.Reference("maze", GameData.stageIndex.value.ToString()),
            (maze) => {
                mazeFactory.MakeMaze(maze);
                PopupIndicator.Instance.Hide();
            }
        );
    }
}