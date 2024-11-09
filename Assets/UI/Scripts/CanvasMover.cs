using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMover : MonoBehaviour
{
    [SerializeField] private Vector2 disabledDelta;
    private Vector2 enabledLocation;
    [SerializeField] private float speed = 10;
    private RectTransform rectTransform;
    bool hasInit = false;
    [SerializeField] private bool isEnabling;
    [SerializeField] private bool isDisabling;
    private bool stopCoroutines = false; // used for shenanigan prevention

    public IEnumerator EnableAnimation()
    {
        Init();
        isEnabling = true;
        while (rectTransform.anchoredPosition != enabledLocation && !stopCoroutines)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, enabledLocation, speed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }
        isEnabling = false;
        stopCoroutines = false;
        yield return null;
    }

    public IEnumerator DisableAnimation()
    {
        Init();
        isDisabling = true;
        Vector2 pos = enabledLocation + disabledDelta;

        while (rectTransform.anchoredPosition != pos && !stopCoroutines)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, pos, speed * Time.deltaTime * 100);
            yield return null; // Wait for the next frame
        }
        isDisabling = false;
        stopCoroutines = false;
        yield return null;
    }

    public void Update()
    {
        Init();
        // prevents shenanigans
        if (isDisabling && isEnabling)
        {
            stopCoroutines = true;
            SetDisabled();
            isEnabling = isDisabling = false;
        }
    }

    public void SetDisabled()
    {
        Init();

        Vector2 pos = enabledLocation + disabledDelta;
        rectTransform.anchoredPosition = pos;
    }

    public void SetEnabled()
    {
        Init();
        rectTransform.anchoredPosition = enabledLocation;
    }


    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (!hasInit)
        {
            this.rectTransform = GetComponent<RectTransform>();
            this.enabledLocation = rectTransform.anchoredPosition;
            hasInit = true;
        }
    }
}
