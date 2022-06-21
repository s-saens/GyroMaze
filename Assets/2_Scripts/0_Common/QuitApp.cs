using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApp : MonoBehaviour
{
    public ButtonEvent quitEvent;
    
    private void OnEnable()
    {
        quitEvent.OnClick += Quit;
    }

    private void OnDisable()
    {
        quitEvent.OnClick -= Quit;
    }

    private void Quit(string s)
    {
        Application.Quit();
    }
}
