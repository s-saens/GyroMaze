using UnityEngine;
using System.Collections;
using TMPro;

public class PopupIndicator : _Popup<PopupIndicator>
{
    [SerializeField] private int timeOut = 3;
    [SerializeField] private TMP_Text networkIsPoorText;
    private IEnumerator indicatorTimeOut;

    public override void Show()
    {
        base.Show();

        if(indicatorTimeOut != null) StopCoroutine(indicatorTimeOut);
        indicatorTimeOut = IndicatorTimeOut();
        StartCoroutine(indicatorTimeOut);
    }

    private IEnumerator IndicatorTimeOut()
    {
        int activeTime = 0;
        string dots = "";
        while(this.IsOn)
        {
            if(activeTime >= timeOut-1)
            {
                networkIsPoorText.gameObject.SetActive(true);
                networkIsPoorText.text = "NETWORK IS POOR" + dots;
                dots += '.';
                if(dots.Length > 3) dots = "";
                yield return new WaitForSeconds(0.3f);
            }
            activeTime++;
            yield return new WaitForSecondsRealtime(1);
        }
        networkIsPoorText.gameObject.SetActive(false);
    }
}