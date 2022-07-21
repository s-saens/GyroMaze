using System;
using System.Collections.Generic;
using UnityEngine;

/*
가장 처음에 열리는 View는 [1]
스택에 [1]만 남았을 때 Back() 수행할 경우 열리는 View는 [0]
*/

public class ViewToggle : MonoBehaviour
{

    private List<GameObject> views = new List<GameObject>();
    private int activeViewIndex = 1;
    private Stack<int> viewIndexHistory = new Stack<int>();

    // Events
    [SerializeField] private Event backEvent;
    [SerializeField] private EventInt viewToggleEvent;


    private void OnEnable()
    {
        viewToggleEvent.callback += Open;
        backEvent.callback += Back;
    }
    private void OnDisable()
    {
        viewToggleEvent.callback -= Open;
        backEvent.callback -= Back;
    }

    private void Awake()
    {
        for(int i=0 ; i<this.transform.childCount ; ++i)
        {
            views.Add(this.transform.GetChild(i).gameObject);
            views[i].SetActive(false);
        }
        views[1].SetActive(true);
    }

    public void Open(int i) // = Push
    {
        viewIndexHistory.Push(activeViewIndex);
        views[activeViewIndex].SetActive(false);
        views[i].SetActive(true);
        activeViewIndex = i;
    }

    private void Back() // = Pop
    {
        if(viewIndexHistory.Count <= 0)
        {
            Open(0);
            return;
        }

        int poppedIndex = viewIndexHistory.Pop();
        views[activeViewIndex].SetActive(false);
        views[poppedIndex].SetActive(true);
        activeViewIndex = poppedIndex;
    }
}
