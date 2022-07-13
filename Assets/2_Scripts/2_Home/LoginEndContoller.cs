using UnityEngine;

public class LoginEndController : MonoBehaviour
{
    [SerializeField] private Event loginEndEvent;

    [SerializeField] private Event viewToggleEvent;

    private void OnEnable()
    {
        loginEndEvent.callback += OnLoginEnd;
    }
    private void OnDisable()
    {
        loginEndEvent.callback -= OnLoginEnd;
    }

    private void OnLoginEnd(string param)
    {
        viewToggleEvent.Invoke("1");
    }
}