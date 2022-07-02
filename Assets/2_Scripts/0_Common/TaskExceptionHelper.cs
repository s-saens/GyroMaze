using UnityEngine;
using System.Threading.Tasks;
using Firebase.Extensions;

public static class TaskExts
{
    public static void HandleFaulted(this Task task)
    {
        task.ContinueWithOnMainThread(t =>
        {
            if (t.IsFaulted)
            {
                PopupIndicator.Instance.Hide();
                PopupNetworkError.Instance.Show();
            }
        });
    }
}