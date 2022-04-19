using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class StageButtonFactory : MonoBehaviour
{
    // Making Data
    public GameObject buttonPrefab;

    public List<StageButton> MakeButtons(int levelCount)
    {
        List<StageButton> buttons = new List<StageButton>();
        for(int i=0 ; i<levelCount ; ++i)
        {
            int index = i;

            GameObject go = Instantiate(buttonPrefab, this.transform);
            go.name = $"Level {index+1}";

            StageButton b = go.GetComponent<StageButton>();
            b.buttonId = index;
            buttons.Add(b);
            

            TMP_Text levelText = go.GetComponentInChildren<TMP_Text>();
            levelText.text = $"{index+1}";
        }

        return buttons;
    }
}