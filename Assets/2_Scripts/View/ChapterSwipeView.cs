using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChapterSwipeView : MonoBehaviour
{
    public ChapterSwipeEvent swipeEvent;

    public int chapterCount = 5;
    public int visibleChapterCount = 3;
    public int nowIndex = 0;
    public int maxDeltaX = 100;
    public int originalSize = 500;
    public int smallSize = 400;
    public float stopTime = 0.6f;
    public float magnetLerpTime = 0.5f;

    public GameObject slider;
    public GameObject contentPrefab;
    public RectTransform contentsParent;

    private List<GameObject> contents = new List<GameObject>();
    private IEnumerator swipeEndCoroutine;
    private IEnumerator moveEndCoroutine;

    private int NowIndexPositionX
    {
        get
        {
            return -nowIndex * originalSize;
        }
    }

    private int LastIndexPositionX
    {
        get
        {
            return -(chapterCount - 1) * originalSize;
        }
    }

    private bool IndexOnFirst(float deltaX)
    {
        return (nowIndex <= 0 && deltaX >= 0);
    }
    private bool IndexOnLast(float deltaX)
    {
        return (nowIndex >= chapterCount - 1 && deltaX <= 0);
    }

    private void Start()
    {
        for (int i = 0; i < chapterCount; ++i)
        {
            GameObject go = Instantiate(contentPrefab, contentsParent);
            go.transform.localPosition = new Vector3(originalSize * i, 0, 0);
            go.name = $"Chapter{i + 1}";

            TMP_Text chapterText = go.transform.GetChild(0).GetComponent<TMP_Text>();
            chapterText.text = $"{i + 1}";

            contents.Add(go);

            UpdateScale(i);
            UpdateActivation(i);
        }
    }


    public void OnSwipe(float deltaX)
    {
        if (swipeEndCoroutine != null) StopCoroutine(swipeEndCoroutine);
        if (moveEndCoroutine != null) StopCoroutine(moveEndCoroutine);

        MoveContents(deltaX);
    }
    private void MoveContents(float deltaX)
    {
        if (IndexOnFirst(deltaX))
        {
            deltaX *= Mathf.Pow(0.5f, contentsParent.position.x / originalSize);
        }
        if(IndexOnLast(deltaX))
        {
            deltaX *= Mathf.Pow(0.5f, (contentsParent.position.x - LastIndexPositionX) / originalSize);
        }
        contentsParent.Translate(Vector2.right * deltaX, Space.Self);
        UpdateIndex();
    }

    private void UpdateIndex()
    {
        this.nowIndex = -(int)((contentsParent.transform.localPosition.x - (originalSize * 0.5f)) / originalSize);
        if (this.nowIndex < 0) this.nowIndex = 0;
        if (this.nowIndex >= chapterCount) this.nowIndex = chapterCount - 1;


        for (int i = nowIndex - (visibleChapterCount / 2) - 1; i <= nowIndex + (visibleChapterCount / 2) + 1; ++i)
        {
            if (i >= 0 && i < chapterCount)
            {
                UpdateScale(i);
                UpdateActivation(i);
            }
        }
    }
    private void UpdateScale(int index)
    {
        RectTransform rt = (RectTransform)contents[index].transform;

        float y = Mathf.Abs(contents[index].transform.position.x - (contentsParent.rect.xMax)) / originalSize;

        rt.sizeDelta = Vector2.one * originalSize * Mathf.Pow(0.6f, y);
    }
    private void UpdateActivation(int index)
    {
        if (Mathf.Abs(index - nowIndex) > visibleChapterCount / 2)
        {
            contents[index].SetActive(false);
        }
        else
        {
            contents[index].SetActive(true);
        }
    }

    public void OnSwipeEnd(float deltaX) // deltaX가 클수록 더 강하게 움직임.
    {
        swipeEndCoroutine = SwipeEndCoroutine(deltaX);
        StartCoroutine(swipeEndCoroutine);
    }


    private IEnumerator SwipeEndCoroutine(float deltaX)
    {
        for (float i = stopTime; i >= 0; i -= Time.deltaTime)
        {
            if (IndexOnFirst(deltaX) || IndexOnLast(deltaX))
            {
                break;
            }
            MoveContents(deltaX * i);
            yield return 0;
        }
        yield return MagnetPosition();
    }

    private IEnumerator MagnetPosition()
    {
        while (true)
        {
            contentsParent.localPosition = Vector2.right * Mathf.Lerp(contentsParent.localPosition.x, NowIndexPositionX, magnetLerpTime);
            UpdateIndex();

            yield return 0;
        }
    }






}
