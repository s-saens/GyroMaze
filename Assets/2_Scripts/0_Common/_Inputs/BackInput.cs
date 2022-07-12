using UnityEngine;
using UnityEngine.EventSystems;

public class BackInput : MonoBehaviour
{
    [SerializeField] private Event backEvent;
    public static bool canBack = true;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && canBack)
        {
            backEvent.callback?.Invoke("");
        }
    }
}
