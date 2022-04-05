using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ChapterButtonFactory : MonoBehaviour
{
    // Making Data
    public GameObject buttonPrefab;

    public List<Button> MakeButtons(int chapterCount, float originalSize)
    {
        List<Button> buttons = new List<Button>();
        for (int i = 0; i < chapterCount; ++i)
        {
            int index = i;

            GameObject go = Instantiate(buttonPrefab, this.transform);
            go.transform.localPosition = new Vector3(originalSize * index, 0, 0);
            go.name = $"Chapter{i + 1}";
            buttons.Add(go.GetComponent<Button>());

            // Text
            TMP_Text chapterText = go.transform.GetChild(0).GetComponent<TMP_Text>();
            chapterText.text = $"{i + 1}";
        }

        return buttons;
    }
}