using UnityEngine;
using System;
using TMPro;

public class RemainTimeView : MonoBehaviour
{
    [SerializeField] private Event tickEvent;
    private TMP_Text text;

    private void OnEnable()
    {
        text = this.GetComponent<TMP_Text>();
        text.text = TickInvoker.Instance.remainTime;
        tickEvent.callback += OnTick;
    }

    private void OnDisable()
    {
        tickEvent.callback -= OnTick;
    }

    private void OnTick(string param)
    {
        text.text = param;
    }
}