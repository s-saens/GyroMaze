using UnityEngine;
using Firebase.Database;

public class C_Indicator : SingletonMono<C_Indicator>
{
    [SerializeField] private Animator indicator;
    public bool IsOn
    {
        get;
        private set;
    }

    public void ShowIndicator()
    {
        indicator.gameObject.SetActive(true);
        indicator.SetBool("On", true);
        IsOn = true;

    }
    public void HideIndicator()
    {
        indicator.SetBool("On", false);
        IsOn = false;
    }
}