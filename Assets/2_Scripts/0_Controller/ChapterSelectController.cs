using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ChapterSelectController : MonoBehaviour
{
    // Data
    public ChapterSelectData data;

    // Events
    public ChapterSwipeEvent swipeEvent;
    public ChapterSlideEvent slideEvent;
    public ChapterClickEvent clickEvent;

    public ChapterSwipeView chapterView;


    public Slider slider;
    public GameObject buttonPrefab;
    public RectTransform buttonsParent;


    private List<GameObject> buttons = new List<GameObject>();
    private IEnumerator swipeEndCoroutine;
    private IEnumerator magnetCoroutine;

    private bool isSwiping = false;


    private void Start()
    {
        InitializeProperties();
        InitializeButtons();
        InitializeSlider();
    }

    private void OnEnable()
    {
        data.nowIndex.onChange += OnChangeNowIndex;

        swipeEvent.OnSwipe += OnSwipe;
        swipeEvent.OnSwipeEnd += OnSwipeEnd;
        slideEvent.OnSlide += OnSlide;
        clickEvent.OnClick += OnClick;
    }

    private void OnDisable()
    {
        data.nowIndex.onChange -= OnChangeNowIndex;

        swipeEvent.OnSwipe -= OnSwipe;
        swipeEvent.OnSwipeEnd -= OnSwipeEnd;
        slideEvent.OnSlide -= OnSlide;
        clickEvent.OnClick -= OnClick;
    }

    // Events
    private void OnSwipe(float deltaX)
    {
        isSwiping = true;

        CancelAllCoroutines();
        MoveContents(deltaX);
    }

    private void OnSwipeEnd(float deltaX)
    {
        isSwiping = false;

        swipeEndCoroutine = SwipeEndCoroutine(deltaX);
        StartCoroutine(swipeEndCoroutine);
    }

    private void OnSlide(int value)
    {
        MoveToIndex(value);
    }

    private void OnClick(int index)
    {
        if(index == data.nowIndex.value)
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
        slider.value = index;
    }


    private int NowIndexPositionX
    {
        get
        {
            return -data.nowIndex.value * data.originalSize;
        }
    }
    private int LastIndexPositionX
    {
        get
        {
            return -(data.chapterCount - 1) * data.originalSize;
        }
    }

    private void InitializeProperties()
    {
        data.nowIndex.value = 0;
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < data.chapterCount; ++i)
        {
            int index = i;

            GameObject go = Instantiate(buttonPrefab, buttonsParent);
            go.transform.localPosition = new Vector3(data.originalSize * index, 0, 0);
            go.name = $"Chapter{i + 1}";
            buttons.Add(go);

            // Button
            Button chapterButton = go.GetComponent<Button>();
            chapterButton.onClick.AddListener(() => clickEvent.OnClick?.Invoke(index));

            // Text
            TMP_Text chapterText = go.transform.GetChild(0).GetComponent<TMP_Text>();
            chapterText.text = $"{i + 1}";

            UpdateScale(index);
        }
        MoveToIndex(data.nowIndex.value);
    }

    private void InitializeSlider()
    {
        slider.minValue = 0;
        slider.maxValue = data.chapterCount - 1;
        slider.onValueChanged.AddListener((value) => slideEvent.OnSlide?.Invoke((int)value));
    }

    private bool IndexOnFirst(float deltaX)
    {
        return (data.nowIndex.value <= 0 && deltaX >= 0);
    }
    private bool IndexOnLast(float deltaX)
    {
        return (data.nowIndex.value >= data.chapterCount - 1 && deltaX <= 0);
    }
    private void MoveContents(float deltaX)
    {
        if (IndexOnFirst(deltaX))
        {
            deltaX *= Mathf.Pow(0.4f, Mathf.Abs(buttonsParent.position.x) / data.originalSize);
        }
        if (IndexOnLast(deltaX))
        {
            deltaX *= Mathf.Pow(0.4f, Mathf.Abs(buttonsParent.position.x - LastIndexPositionX) / data.originalSize);
        }
        buttonsParent.Translate(Vector3.right * deltaX, Space.Self);
        UpdateIndex();
        UpdateAllScale();
    }

    private void UpdateIndex()
    {
        data.nowIndex.value = -(int)((buttonsParent.transform.localPosition.x - (data.originalSize * 0.5f)) / data.originalSize);
        if (data.nowIndex.value < 0) data.nowIndex.value = 0;
        if (data.nowIndex.value >= data.chapterCount) data.nowIndex.value = data.chapterCount - 1;
    }

    private void UpdateAllScale()
    {
        for (int i = 0; i < data.chapterCount; ++i)
        {
            UpdateScale(i);
        }
    }

    private void UpdateScale(int index)
    {
        RectTransform rt = (RectTransform)buttons[index].transform;

        float power = Mathf.Abs(buttons[index].transform.position.x - (buttonsParent.rect.xMax)) / data.originalSize;
        float size = data.originalSize * Mathf.Pow(0.6f, power);

        rt.sizeDelta = size < data.minSize ? Vector2.one * size : Vector2.one * data.minSize;
    }

    public void MoveToIndex(int index)
    {
        if(isSwiping) return;
        
        CancelAllCoroutines();

        data.nowIndex.value = index;
        magnetCoroutine = MagnetPosition();
        StartCoroutine(magnetCoroutine);
    }


    private IEnumerator SwipeEndCoroutine(float deltaX)
    {
        for (float i = data.stopTime * Mathf.Log10(Mathf.Abs(deltaX)) / data.originalSize; i >= 0; i -= Time.deltaTime)
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
