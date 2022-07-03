using UnityEngine;

public class _Popup<T> : SingletonMono<_Popup<T>>
{
    [SerializeField] private Animator anim;

    private readonly int onHash = Animator.StringToHash("On");

    public bool IsOn
    {
        get;
        private set;
    }

    public virtual void Show()
    {
        anim.gameObject.SetActive(true);
        anim.SetBool(onHash, true);
        IsOn = true;
    }
    public virtual void Hide()
    {
        anim.SetBool(onHash, false);
        IsOn = false;
    }
}