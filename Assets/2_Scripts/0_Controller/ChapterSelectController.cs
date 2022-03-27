using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class ChapterSelectController : MonoBehaviour
{
    // Data
    public ChapterSelectViewData viewData;
    public GlobalData globalData;

    // Events
    public ChapterSwipeEvent swipeEvent;
    public ChapterSlideEvent slideEvent;
    public ChapterClickEvent clickEvent;

    public EventSystem eventSystem;

    // Views
    public ChapterSwipeView chapterView;


    public Slider slider;
    public GameObject buttonPrefab;
    public RectTransform buttonsParent;


    private List<GameObject> buttons = new List<GameObject>();
    private IEnumerator swipeEndCoroutine;
    private IEnumerator magnetCoroutine;


    private void Start()
    {
        InitializeProperties();
        InitializeButtons();
        InitializeSlider();
    }

    private void OnEnable()
    {
        globalData.chapterIndex.onChange += OnChangeNowIndex;

        swipeEvent.OnTouchDown += OnTouchDown;
        swipeEvent.OnSwipe += OnSwipe;
        swipeEvent.OnSwipeEnd += OnSwipeEnd;
        slideEvent.OnSlide += OnSlide;
        clickEvent.OnClick += OnClick;
    }

    private void OnDisable()
    {
        globalData.chapterIndex.onChange -= OnChangeNowIndex;

        swipeEvent.OnTouchDown -= OnTouchDown;
        swipeEvent.OnSwipe -= OnSwipe;
        swipeEvent.OnSwipeEnd -= OnSwipeEnd;
        slideEvent.OnSlide -= OnSlide;
        clickEvent.OnClick -= OnClick;
    }

    // Events
    private void OnTouchDown()
    {
        CancelAllCoroutines();
        magnetCoroutine = MagnetPosition();
        StartCoroutine(magnetCoroutine);
    }

    private void OnSwipe(float deltaX)
    {
        CancelAllCoroutines();
        MoveContents(deltaX);
    }

    private void OnSwipeEnd(float deltaX)
    {
        swipeEndCoroutine = SwipeEndCoroutine(deltaX);
        StartCoroutine(swipeEndCoroutine);
    }

    private void OnSlide(int value)
    {
        MoveToIndex(value);
    }

    private void OnClick(int index)
    {
        if(index == globalData.chapterIndex.value)
        {
            // TODO: Move to Level Select Window
        }
        else
        {
            MoveToIndex(index);
        }
    }


    // Change Data
    private void OnChangeNowIndex(int index)
    {
        slideEvent.OnSlide -= OnSlide;
        slider.value = index;
        slideEvent.OnSlide += OnSlide;
    }
    private IEnumerator DoAfterFrame(Action action)
    {
        yield return 0;
        action?.Invoke();
    }


    private int NowIndexPositionX
    {
        get
        {
            return -globalData.chapterIndex.value * viewData.originalSize;
        }
    }
    private int LastIndexPositionX
    {
        get
        {
            return -(viewData.chapterCount - 1) * viewData.originalSize;
        }
    }

    private void InitializeProperties()
    {
        globalData.chapterIndex.value = 0;
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < viewData.chapterCount; ++i)
        {
            int index = i;

            GameObject go = Instantiate(buttonPrefab, buttonsParent);
            go.transform.localPosition = new Vector3(viewData.originalSize * index, 0, 0);
            go.name = $"Chapter{i + 1}";
            buttons.Add(go);

            // Button
            Button chapterButton = go.GetComponent<Button>();
            chapterButton.onClick.AddListener(() => {
                eventSystem.SetSelectedGameObject(null);
                clickEvent.OnClick?.Invoke(index);
            });

            // Text
            TMP_Text chapterText = go.transform.GetChild(0).GetComponent<TMP_Text>();
            chapterText.text = $"{i + 1}";

            UpdateScale(index);
        }
        MoveToIndex(globalData.chapterIndex.value);
    }

    private void InitializeSlider()
    {
        slider.minValue = 0;
        slider.maxValue = viewData.chapterCount - 1;
        slider.onValueChanged.AddListener((value) => slideEvent.OnSlide?.Invoke((int)value));
    }

    private bool IndexOnFirst(float deltaX)
    {
        return (globalData.chapterIndex.value <= 0 && deltaX >= 0);
    }
    private bool IndexOnLast(float deltaX)
    {
        return (globalData.chapterIndex.value >= viewData.chapterCount - 1 && deltaX <= 0);
    }
    private void MoveContents(float deltaX)
    {
        if (IndexOnFirst(deltaX))
        {
            deltaX *= Mathf.Pow(0.4f, Mathf.Abs(buttonsParent.position.x) / viewData.originalSize);
        }
        if (IndexOnLast(deltaX))
        {
            deltaX *= Mathf.Pow(0.4f, Mathf.Abs(buttonsParent.position.x - LastIndexPositionX) / viewData.originalSize);
        }
        buttonsParent.Translate(Vector3.right * deltaX, Space.Self);
        UpdateIndex();
        UpdateAllScale();
    }

    private void UpdateIndex()
    {
        globalData.chapterIndex.value = -(int)((buttonsParent.transform.localPosition.x - (viewData.originalSize * 0.5f)) / viewData.originalSize);
        if (globalData.chapterIndex.value < 0) globalData.chapterIndex.value = 0;
        if (globalData.chapterIndex.value >= viewData.chapterCount) globalData.chapterIndex.value = viewData.chapterCount - 1;
    }

    private void UpdateAllScale()
    {
        for (int i = 0; i < viewData.chapterCount; ++i)
        {
            UpdateScale(i);
        }
    }

    private void UpdateScale(int index)
    {
        RectTransform rt = (RectTransform)buttons[index].transform;

        float l = (Mathf.Abs(buttons[index].transform.position.x - (Screen.width * 0.5f)) / viewData.originalSize) + 1.2f;
        float size = viewData.originalSize / l;

        rt.sizeDelta = size < viewData.minSize ? Vector2.one * size : Vector2.one * viewData.minSize;
    }

    public void MoveToIndex(int index)
    {   
        CancelAllCoroutines();

        globalData.chapterIndex.value = index;
        magnetCoroutine = MagnetPosition();
        StartCoroutine(magnetCoroutine);
    }


    private IEnumerator SwipeEndCoroutine(float deltaX)
    {
        for (float i = viewData.stopTime * Mathf.Log10(Mathf.Abs(deltaX)) / viewData.originalSize; i >= 0; i -= Time.deltaTime)
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
            buttonsParent.localPosition = Vector2.right * Mathf.Lerp(buttonsParent.localPosition.x, NowIndexPositionX, viewData.magnetLerpTime * Time.deltaTime);
            UpdateAllScale();
            yield return 0;
        }
    }

    private void CancelAllCoroutines()
    {
        if (swipeEndCoroutine != null) StopCoroutine(swipeEndCoroutine);
        if (magnetCoroutine != null) StopCoroutine(magnetCoroutine);
    }

}
