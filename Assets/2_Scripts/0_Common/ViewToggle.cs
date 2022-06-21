using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewToggle : MonoBehaviour
{
    public static ViewToggle Instance;
    private GameObject[] views;
    private int activeViewIndex = 0;
    private Stack<int> viewIndexHistory = new Stack<int>();


    private void Start()
    {
        Instance = this;
        views = this.GetComponentsInChildren<GameObject>();
    }


    public void Open(int index) // = Push
    {
        viewIndexHistory.Push(index);
        views[activeViewIndex].SetActive(false);
        views[index].SetActive(true);
        activeViewIndex = index;
    }

    private void Back() // = Pop
    {
        int poppedIndex = viewIndexHistory.Pop();
        views[activeViewIndex].SetActive(false);
        views[poppedIndex].SetActive(true);
        activeViewIndex = poppedIndex;
    }
}
