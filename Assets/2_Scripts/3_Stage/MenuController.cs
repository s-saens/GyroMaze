using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private ButtonEvent homeButton;
    [SerializeField] private ButtonEvent retryButton;


    private void OnEnable()
    {
        homeButton.OnClick += GoHome;
    }
    private void OnDisable()
    {
        homeButton.OnClick -= GoHome;
    }

    private void GoHome(string s)
    {
        SceneController.Instance.LoadScene(SceneEnum.Home);
    }
}
