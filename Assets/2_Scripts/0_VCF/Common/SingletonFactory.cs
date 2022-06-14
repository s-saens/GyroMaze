using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonFactory : MonoBehaviour
{
    private static bool singletonsWereMade = false;

    [SerializeField] private GameObject[] singletonObjects;

    private void Start()
    {
        if(!singletonsWereMade)
        {
            foreach (GameObject go in singletonObjects)
            {
                DontDestroyOnLoad(Instantiate(go));
            }
            singletonsWereMade = true;
        }
    }
}
