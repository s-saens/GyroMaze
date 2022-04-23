using UnityEngine;

public class _SingletonMono<T> : MonoBehaviour where T : _SingletonMono<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                if (instance == null) Debug.LogError("SINGLETON OBJECT DOES NOT EXIST.");
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this.GetComponent<T>();
    }
}