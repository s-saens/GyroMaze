using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ChapterSwipeView : MonoBehaviour
{
    public ChapterSelectData data;

    public ChapterSlideEvent slideEvent;
    public ChapterClickEvent clickEvent;


    public Slider slider;
    public GameObject buttonPrefab;
    public RectTransform buttonsParent;
    private List<GameObject> buttons = new List<GameObject>();
    private IEnumerator swipeEndCoroutine;
    private IEnumerator magnetCoroutine;

    private int NowIndexPositionX
    {
        get
        {
            return -data.nowIndex * data.originalSize;
        }
    }
    private int LastIndexPositionX
    {
        get
        {
            return -(data.chapterCount - 1) * data.originalSize;
        }
    }


    public void InitializeButtons()
    {
        for (int i = 0; i < data.chapterCount; ++i)
        {
            GameObject go = Instantiate(buttonPrefab, buttonsParent);
            go.transform.localPosition = new Vector3(data.originalSize * i, 0, 0);
            go.name = $"Chapter{i + 1}";
            buttons.Add(go);

            // Button
            Button chapterButton = go.GetComponent<Button>();
            chapterButton.onClick.AddListener(() => clickEvent.OnClick?.Invoke(i));

            // Text
            TMP_Text chapterText = go.transform.GetChild(0).GetComponent<TMP_Text>();
            chapterText.text = $"{i + 1}";


            UpdateScale(i);
            UpdateActivation(i);
        }
        MoveToIndex(data.nowIndex);
    }

    private void InitializeSlider()
    {
        slider.minValue = 0;
        slider.maxValue = data.chapterCount-1;
        slider.onValueChanged.AddListener((value) => slideEvent.OnSlide?.Invoke((int)value));
    }

    private bool IndexOnFirst(float deltaX)
    {
        return (data.nowIndex <= 0 && deltaX >= 0);
    }
    private bool IndexOnLast(float deltaX)
    {
        return (data.nowIndex >= data.chapterCount - 1 && deltaX <= 0);
    }

    public void OnSwipe(float deltaX)
    {
        CancelAllCoroutines();
        MoveContents(deltaX);
    }
    private void MoveContents(float deltaX)
    {
        if (IndexOnFirst(deltaX))
        {
            deltaX *= Mathf.Pow(0.4f, buttonsParent.position.x / data.originalSize);
        }
        if(IndexOnLast(deltaX))
        {
            deltaX *= Mathf.Pow(0.4f, (buttonsParent.position.x - LastIndexPositionX) / data.originalSize);
        }
        buttonsParent.Translate(Vector2.right * deltaX, Space.Self);
        UpdateIndex();
        UpdateAllScaleAndActivation();
    }

    private void UpdateIndex()
    {
        data.nowIndex = -(int)((buttonsParent.transform.localPosition.x - (data.originalSize * 0.5f)) / data.originalSize);
        if (data.nowIndex < 0) data.nowIndex = 0;
        if (data.nowIndex >= data.chapterCount) data.nowIndex = data.chapterCount - 1;
    }

    private void UpdateAllScaleAndActivation()
    {
        for (int i = data.nowIndex - (data.visibleChapterCount / 2) - 1; i <= data.nowIndex + (data.visibleChapterCount / 2) + 1; ++i)
        {
            if (i >= 0 && i < data.chapterCount)
            {
                UpdateScale(i);
                UpdateActivation(i);
            }
        }
    }
    private void UpdateScale(int index)
    {
        RectTransform rt = (RectTransform)buttons[index].transform;

        float y = Mathf.Abs(buttons[index].transform.position.x - (buttonsParent.rect.xMax)) / data.originalSize;

        rt.sizeDelta = Vector2.one * data.originalSize * Mathf.Pow(0.6f, y);
    }
    private void UpdateActivation(int index)
    {
        if (Mathf.Abs(index - data.nowIndex) > data.visibleChapterCount / 2)
        {
            buttons[index].SetActive(false);
        }
        else
        {
            buttons[index].SetActive(true);
        }
    }

    public void OnSwipeEnd(float deltaX) // deltaX가 클수록 더 강하게 움직임.
    {
        swipeEndCoroutine = SwipeEndCoroutine(deltaX);
        StartCoroutine(swipeEndCoroutine);
    }

    public void MoveToIndex(int index)
    {
        CancelAllCoroutines();

        data.nowIndex = index;
        magnetCoroutine = MagnetPosition();
        StartCoroutine(magnetCoroutine);
    }


    private IEnumerator SwipeEndCoroutine(float deltaX)
    {
        for (float i=data.stopTime * Mathf.Log10(Mathf.Abs(deltaX))/data.originalSize; i >= 0; i -= Time.deltaTime)
        {
            if (IndexOnFirst(deltaX) || IndexOnLast(deltaX))
            {
                i *= 0.4f;
            }
            MoveContents(deltaX * i);
            yield return 0;
        }
        yield return MagnetPosition();
    }

    private IEnumerator MagnetPosition()
    {
        while (Mathf.Abs(buttonsParent.localPosition.x - NowIndexPositionX) > 0.0001f)
        {
            buttonsParent.localPosition = Vector2.right * Mathf.Lerp(buttonsParent.localPosition.x, NowIndexPositionX, data.magnetLerpTime);
            UpdateAllScaleAndActivation();
            yield return 0;
        }
    }

    private void CancelAllCoroutines()
    {
        if (swipeEndCoroutine != null) StopCoroutine(swipeEndCoroutine);
        if (magnetCoroutine != null) StopCoroutine(magnetCoroutine);
    }
}
