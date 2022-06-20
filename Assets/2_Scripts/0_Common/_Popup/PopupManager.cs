using UnityEngine;
using System.Collections.Generic;

public class PopupManager : MonoBehaviour
{
    private Transform canvas;
    private Dictionary<string, PopupPool> popupPools = new Dictionary<string, PopupPool>();
    private Stack<PopupPool> poolHistory = new Stack<PopupPool>();

    private void Start()
    {
        // canvas 정의
        canvas = this.GetComponentInChildren<Canvas>().transform;
        foreach (PopupPool pool in canvas.GetComponentsInChildren<PopupPool>())
        {
            popupPools.Add(pool.gameObject.name, pool);
        }
    }

    public Popup OpenPopup(string popupPoolName, Vector2 screenPosition, string data)
    {
        try
        {
            PopupPool pool = popupPools[popupPoolName];

            Popup popup = pool.Open(screenPosition, data);
            if (popup != null)
            {
                poolHistory.Push(pool);
                return popup;
            }
            return null;
        }
        catch
        {
            Debug.LogError("No PopupPool!");
            return null;
        }
    }

    public void Close()
    {
        if (poolHistory.Count <= 0)
        {
            return;
        }
        poolHistory.Pop()?.Close();
    }

    public void CloseAll()
    {
        foreach (PopupPool p in poolHistory)
        {
            p.Close();
        }
    }
}