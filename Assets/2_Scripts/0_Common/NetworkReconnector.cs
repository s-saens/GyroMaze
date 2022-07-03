using UnityEngine;

public class NetworkReconnector : MonoBehaviour
{
    [SerializeField] Event tryReconnectionEvent;
    [SerializeField] NetworkChecker networkChecker;

    private void OnEnable()
    {
        tryReconnectionEvent.callback += TryReconnection;
    }
    private void OnDisable()
    {
        tryReconnectionEvent.callback -= TryReconnection;
    }

    private void TryReconnection(string param)
    {
        PopupNetworkError.Instance.Hide();
        PopupIndicator.Instance.Show();
        networkChecker.Check((success) =>
        {
            PopupIndicator.Instance.Hide();

            if (!success) PopupNetworkError.Instance.Show();
        });
    }
}