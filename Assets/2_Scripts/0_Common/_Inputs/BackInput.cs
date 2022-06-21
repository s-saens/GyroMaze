using UnityEngine;
using UnityEngine.EventSystems;

public class BackInput : MonoBehaviour
{
    [SerializeField] private ButtonEvent backEvent;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            backEvent.OnClick?.Invoke("");
        }
    }
}
