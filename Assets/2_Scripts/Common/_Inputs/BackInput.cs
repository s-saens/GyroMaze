using UnityEngine;
using UnityEngine.EventSystems;

public class BackInput : MonoBehaviour
{
    [SerializeField] private ButtonEvent backEvent;

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            backEvent.OnClick?.Invoke("");
        }
    }
}
