using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ChapterButtonFactory : MonoBehaviour
{
    [SerializeField] private ClickEvent clickEvent;

    // Making Data
    public GameObject buttonPrefab;

    public List<ChapterButton> MakeButtons(int chapterCount, float originalSize)
    {
        List<ChapterButton> buttons = new List<ChapterButton>();
        for (int i = 0; i < chapterCount; ++i)
        {
            int index = i;

            GameObject go = Instantiate(buttonPrefab, this.transform);
            go.transform.localPosition = new Vector3(originalSize * index, 0, 0);
            go.name = $"Chapter{i + 1}";
            ChapterButton b = go.GetComponent<ChapterButton>();
            b.buttonId = i;
            buttons.Add(b);
            
            b.SetClickEvent(clickEvent);

            // Text
            TMP_Text chapterText = go.transform.GetChild(0).GetComponent<TMP_Text>();
            chapterText.text = $"{i + 1}";
        }

        return buttons;
    }
}