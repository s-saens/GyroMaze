using UnityEngine;
// TODO: Make Indicator

public class C_Indicator : _SingletonMono<C_Indicator>
{
    [SerializeField] private Animator indicator;

    public void ShowIndicator()
    {
        indicator.SetBool("On", true);
    }
    public void HideIndicator()
    {
        indicator.SetBool("On", false);
    }
}