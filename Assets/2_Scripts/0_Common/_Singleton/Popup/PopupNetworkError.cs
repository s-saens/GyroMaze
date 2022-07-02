using UnityEngine;

public class PopupNetworkError : _Popup
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
        this.Hide();
        PopupIndicator.Instance.Show();
        networkChecker.Check((success)=>
        {
            PopupIndicator.Instance.Hide();

            if(!success) this.Show();
        });
    }
}