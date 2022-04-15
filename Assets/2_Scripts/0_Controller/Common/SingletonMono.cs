using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject go = new GameObject();
                DontDestroyOnLoad(go);
                T t = go.AddComponent<T>();
                instance = t;
            }
            return instance;
        }
    }
}