using UnityEngine;
// TODO: Make Indicator

public class C_Indicator : _SingletonMono<C_Indicator>
{
    public void ShowIndicator()
    {
        Debug.Log($"Show Indicator");
    }
    public void ShowIndicator(string type)
    {
        Debug.Log($"Show Indicator[{type}]");
    }
    public void HideIndicator()
    {
        Debug.Log($"Hide Indicator");
    }
    public void HideIndicator(string type)
    {
        Debug.Log($"Hide Indicator[{type}]");
    }
}