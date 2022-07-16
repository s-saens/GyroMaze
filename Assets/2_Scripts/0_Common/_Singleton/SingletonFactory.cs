using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonFactory : MonoBehaviour
{
    private static bool singletonsWereMade = false;

    [SerializeField] private GameObject[] singletonObjects;

    private void Awake()
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

#if UNITY_EDITOR
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) PlayerPrefs.DeleteAll();
    }
#endif
}
