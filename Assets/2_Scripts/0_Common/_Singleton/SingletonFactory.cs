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
#if UNITY_EDITOR
            UserData.stage.value = 34;
#endif
            foreach (GameObject go in singletonObjects)
            {
                DontDestroyOnLoad(Instantiate(go));
            }
            singletonsWereMade = true;
        }
    }
}
