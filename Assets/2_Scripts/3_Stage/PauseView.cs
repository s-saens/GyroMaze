using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseView : MonoBehaviour
{
    [SerializeField] private TMP_Text stageText;

    private void Start()
    {
        SetStageText();
    }

    private void SetStageText()
    {
        stageText.text = $"STAGE {GameData.stageIndex.value + 1}";
    }
}
