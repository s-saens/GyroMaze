using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChapterSelectionView : MonoBehaviour
{
    // Data
    public ChapterSelectionData viewData;
    public GlobalData globalData;

    // Events
    public SlideEvent slideEvent;
    public ClickEvent clickEvent;

    // Views
    public Slider slider;
    public RectTransform buttonsParent;

    private List<ChapterButton> buttons = new List<ChapterButton>();
    private IEnumerator swipeEndCoroutine;
    private IEnumerator magnetCoroutine;

    // Factory
    public ChapterButtonFactory chapterButtonFactory;



    // Call From Controller
    public void Swipe(float deltaX)
    {
        CancelAllCoroutines();
        MoveContents(deltaX);
    }
    public void SwipeEnd(float deltaX)
    {
        CancelAllCoroutines();
        swipeEndCoroutine = SwipeEndCoroutine(deltaX);
        StartCoroutine(swipeEndCoroutine);
    }
    public void SetSliderHandle(int value)
    {
        slider.value = value;
    }

    public void Magnet()
    {
        CancelAllCoroutines();
        magnetCoroutine = MagnetPositionCoroutine();
        StartCoroutine(magnetCoroutine);
    }

    // PRIVATE ZONE

    private void Start()
    {
        InitializeButtons();
        InitializeSlider();
    }
    private void InitializeButtons()
    {
        buttons = chapterButtonFactory.MakeButtons(viewData.chapterCount, viewData.originalSize);
        for(int i=0 ; i<buttons.Count ; ++i)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => clickEvent.OnClick?.Invoke(index));
        }
        UpdateAllScale();
    }
    private void InitializeSlider()
    {
        slider.minValue = 0;
        slider.maxValue = viewData.chapterCount - 1;
        slider.onValueChanged.AddListener((value) => slideEvent.OnSlide?.Invoke((int)value));
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

    public void MoveContents(float deltaX)
    {
        if (IndexOnFirst(deltaX))
        {
            deltaX *= Mathf.Pow(0.4f, Mathf.Abs(buttonsParent.localPosition.x) / viewData.originalSize);
        }
        if (IndexOnLast(deltaX))
        {
            deltaX *= Mathf.Pow(0.4f, Mathf.Abs(buttonsParent.localPosition.x - LastIndexPositionX) / viewData.originalSize);
        }
        buttonsParent.Translate(Vector3.right * deltaX, Space.Self);
        UpdateIndex();
        UpdateAllScale();
    }
    private bool IndexOnFirst(float deltaX)
    {
        return (globalData.chapterIndex.value <= 0 && deltaX >= 0);
    }
    private bool IndexOnLast(float deltaX)
    {
        return (globalData.chapterIndex.value >= viewData.chapterCount - 1 && deltaX <= 0);
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
        magnetCoroutine = MagnetPositionCoroutine();
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
        yield return MagnetPositionCoroutine();
    }

    private IEnumerator MagnetPositionCoroutine()
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
