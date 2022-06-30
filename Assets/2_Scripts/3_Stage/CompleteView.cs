using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CompleteView : MonoBehaviour
{
    [SerializeField] private Event nextEvent;

    [SerializeField] private TMP_Text stageText;

    private void Start()
    {
        SetStageText();
    }

    private void OnEnable()
    {
        nextEvent.callback += Next;
        BackInput.canBack = false;
    }
    private void OnDisable()
    {
        nextEvent.callback -= Next;
        BackInput.canBack = true;
    }

    private void SetStageText()
    {
        stageText.text = $"STAGE {GameData.stageIndex.value + 1}";
    }

    private void Next(string param)
    {
        GameData.stageIndex.value++;
    }
}
