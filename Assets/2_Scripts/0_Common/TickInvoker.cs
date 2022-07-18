using UnityEngine;
using System;
using System.Collections;

public class TickInvoker : SingletonMono<TickInvoker>
{
    [SerializeField] Event tickEvent;
    public string remainTime = "Refill";

    public void StartTick()
    {
        StartCoroutine(Count());
    }

    private IEnumerator Count()
    {
        while(true)
        {
            TimeSpan t = UserData.databaseUser.remainTime;

            if(t.TotalMilliseconds < 0) remainTime = "00:00:00";
            else if(t.Days > 0) remainTime = t.Days + " days, " + t.ToString(@"hh\:mm\:ss");
            else remainTime = t.ToString(@"hh\:mm\:ss");

            tickEvent.Invoke(remainTime);

            int remainSecDecimal = (int)MathF.Abs(t.Milliseconds) - (int)MathF.Abs((int)(t.Milliseconds/1000)) * 1000;
            yield return new WaitForSecondsRealtime(remainSecDecimal * 0.001f);
        }
    }
}