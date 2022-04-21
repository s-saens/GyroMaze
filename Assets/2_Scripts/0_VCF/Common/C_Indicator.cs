using UnityEngine;
// TODO: Make Indicator

public class C_Indicator : _SingletonMono<C_Indicator>
{
    [SerializeField] private Animator indicator;

    public void ShowIndicator()
    {
        indicator.gameObject.SetActive(true);
        indicator.SetBool("On", true);
    }
    public void HideIndicator()
    {
        indicator.SetBool("On", false);
    }
}