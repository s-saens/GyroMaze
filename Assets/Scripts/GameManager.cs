using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
#region Singleton
    private static GameManager instance;
    private void Awake()
    {
        instance = this;
        GameObject.DontDestroyOnLoad(this);
    }
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("instance is null!!");
            }
            return instance;
        }
    }
#endregion
}
