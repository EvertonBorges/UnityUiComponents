using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSlideVertical : MonoBehaviour
{
    
    [SerializeField] private float animationTime = 1f;

    private RectTransform _rectTransform;
    private float screenHeight = Screen.height;

    private float topPosition => screenHeight * 2.2f;
    private float midPosition => screenHeight / 2;
    private float bottomPosition => -screenHeight * 1.2f;

    private Coroutine _coroutine;

    void Awake() {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Down()
    {
        if (_rectTransform.position.y > midPosition) SlideTopToMid();
        else if (_rectTransform.position.y > bottomPosition) SlideMidToBottom();
    }

    public void Up()
    {
        if (_rectTransform.position.y < midPosition) SlideBottomToMid();
        else if (_rectTransform.position.y < topPosition) SlideMidToTop();
    }

    private void SlideTopToMid() 
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(Slide(topPosition, midPosition));
    }

    private void SlideMidToBottom()
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(Slide(midPosition, bottomPosition));
    }

    private void SlideBottomToMid() 
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(Slide(bottomPosition, midPosition));
    }

    private void SlideMidToTop()
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(Slide(midPosition, topPosition));
    }

    private IEnumerator Slide(float startPosition, float endPosition)
    {
        float currentTime = 1f - (Mathf.Abs(endPosition - _rectTransform.position.y) / Mathf.Abs(endPosition - startPosition));

        while(currentTime <= animationTime)
        {
            currentTime += Time.deltaTime;
            float timeProportion = MathUtility.CurveAsc(currentTime) / animationTime;
            float position = Mathf.Lerp(startPosition, endPosition, timeProportion);
            _rectTransform.position = new Vector2(_rectTransform.position.x, position);

            yield return null;
        }

        _rectTransform.position = new Vector2(_rectTransform.position.x, endPosition);
        yield return null;
    }

}
