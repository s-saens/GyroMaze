using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Menus : MonoBehaviour
{
    [SerializeField] private ButtonEvent backButton;

    private void OnEnable()
    {
        backButton.OnClick += Back;
    }
    private void OnDisable()
    {
        backButton.OnClick -= Back;
    }

    private void Back(string value)
    {
        // TODO: Popup 먼저 띄워야 함.
        C_Scene.Instance.LoadScene(SceneEnum.Lobby);
    }
}
