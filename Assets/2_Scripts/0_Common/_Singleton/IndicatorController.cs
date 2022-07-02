using UnityEngine;
using Firebase.Database;

// indicator 또한 Popup의 일종인 것으로 취급.
// Animator의 on/off를 통해 끌 수 있는 모든

public class IndicatorController : SingletonMono<IndicatorController>
{
    [SerializeField] private Animator indicator;

    private readonly int onHash = Animator.StringToHash("On");

    public bool IsOn
    {
        get;
        private set;
    }

    public void Show()
    {
        indicator.gameObject.SetActive(true);
        indicator.SetBool(onHash, true);
        IsOn = true;
    }
    public void Hide()
    {
        indicator.SetBool(onHash, false);
        IsOn = false;
    }
}