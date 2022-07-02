using UnityEngine;
using System;

using System.Threading.Tasks;
using Firebase.Extensions;

public static class TaskExts
{
    public static void OnFaulted(this Task task, Action callback)
    {
        task.ContinueWithOnMainThread(t =>
        {
            if (t.IsFaulted)
            {
                // Debug.LogException(t.Exception.Flatten().InnerException);
                IndicatorController.Instance.Hide(); // 무언가 실패한 경우 반드시 Indicator 꺼주기.
                callback?.Invoke();
            }
        });
    }
}