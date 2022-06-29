using UnityEngine;
using UnityEngine.EventSystems;

public class BackInput : MonoBehaviour
{
    [SerializeField] private Event backEvent;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            backEvent.Invoke("");
        }
    }
}
