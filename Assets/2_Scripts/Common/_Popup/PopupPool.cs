using UnityEngine;
using System.Collections.Generic;

public class PopupPool : MonoBehaviour
{
    public int poolCount = 1;
    public bool poolResizable = true;
    public GameObject popupPrefab;
    private List<Popup> popups = new List<Popup>();
    private int popupsPeekIndex = -1;

    private void Start()
    {
        SetPool();
    }

    private void SetPool()
    {
        for (int i = 0; i < poolCount; ++i)
        {
            GameObject popup = Instantiate(popupPrefab, this.transform);
            popup.gameObject.SetActive(false);
            popups.Add(popup.GetComponent<Popup>());
        }
    }

    // Pool이 꽉 찼을 때, pool을 두 배로 늘림.
    private void ResizePool()
    {
        SetPool();
        poolCount *= 2;
    }

    private Popup GetFromPool()
    {
        popupsPeekIndex++;

        if (popupsPeekIndex >= poolCount)
        {
            if (poolResizable == false)
            {
                popupsPeekIndex--;
                return null;
            }
            ResizePool();
        }
        Popup p = popups[popupsPeekIndex];
        return p;
    }

    private void ReturnToPool()
    {
        popups[popupsPeekIndex].gameObject.SetActive(false);
        popupsPeekIndex--;
    }

    public Popup Open(Vector2 position, string data = "")
    {
        Popup popup = GetFromPool();
        if (popup == null)
        {
            Debug.LogWarning("No more pool!");
            return null;
        }
        popup.OnOpen();
        popup.gameObject.SetActive(true);
        popup.SetData(data);
        return popup;
    }

    public void Close()
    {
        if (popupsPeekIndex < 0)
        {
            Debug.LogError("Something went wrong on Popup Pool System.");
            return;
        }

        // TODO : 다음 코드 두 줄은 비동기로 수행해야 함.
        popups[popupsPeekIndex].OnClose();
        ReturnToPool();
    }
}