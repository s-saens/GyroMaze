using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApp : MonoBehaviour
{
    public Event quitEvent;
    
    private void OnEnable()
    {
        quitEvent.callback += Quit;
    }

    private void OnDisable()
    {
        quitEvent.callback -= Quit;
    }

    private void Quit(object param)
    {
        Application.Quit();
    }
}
