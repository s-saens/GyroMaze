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
                if (instance == null) Debug.LogError("Singleton instance does not exist. Check if the SingletonFactory object is in the scene.");
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this.GetComponent<T>();
    }
}