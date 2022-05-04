using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class StageButtonFactory : MonoBehaviour
{
    [SerializeField] private ClickEvent clickEvent;

    // Making Data
    public GameObject buttonPrefab;

    public List<_EventButton> MakeButtons(int stageCount, float originalSize)
    {
        List<_EventButton> buttons = new List<_EventButton>();
        for (int i = 0; i < stageCount; ++i)
        {
            int index = i;

            GameObject go = Instantiate(buttonPrefab, this.transform);
            go.transform.localPosition = new Vector3(originalSize * index, 0, 0);
            go.name = $"Stage {i + 1}";
            _EventButton b = go.GetComponent<_EventButton>();
            b.buttonId = i;
            buttons.Add(b);

            // Text
            TMP_Text stageText = go.transform.GetChild(0).GetComponent<TMP_Text>();
            stageText.text = $"{i + 1}";
        }

        return buttons;
    }
}