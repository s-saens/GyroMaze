using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Event homeButton;
    [SerializeField] private Event retryButton;


    private void OnEnable()
    {
        homeButton.callback += GoHome;
    }
    private void OnDisable()
    {
        homeButton.callback -= GoHome;
    }

    private void GoHome(string s)
    {
        SceneController.Instance.LoadScene(SceneEnum.Home);
    }
}
