using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSlideHorizontalEffect : MonoBehaviour
{
    
    [SerializeField] private float animationTime = 1f;

    private RectTransform _rectTransform;
    private float screenHeight = Screen.height;
    private float screenWidth = Screen.width;

    private float leftPosition => -screenWidth * 2f;
    private float midPosition => screenWidth / 2;
    private float rightPosition => screenWidth * 3f;

    private Coroutine _coroutine;

    private List<Action> commands = new List<Action>();

    void Awake() {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Left()
    {
        if (_rectTransform.position.x > midPosition) SlideRightToMid();
        else if (_rectTransform.position.x > leftPosition) SlideMidToLeft();
    }

    public void Rigth()
    {
        if (_rectTransform.position.x < midPosition) SlideLeftToMid();
        else if (_rectTransform.position.x < rightPosition) SlideMidToRight();
    }

    private void SlideRightToMid() 
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(Slide(rightPosition, midPosition));
    }

    private void SlideMidToLeft()
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(Slide(midPosition, leftPosition));
    }

    private void SlideLeftToMid() 
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(Slide(leftPosition, midPosition));
    }

    private void SlideMidToRight()
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(Slide(midPosition, rightPosition));
    }

    private IEnumerator Slide(float startPosition, float endPosition)
    {
        float currentTime = 1f - (Mathf.Abs(endPosition - _rectTransform.position.x) / Mathf.Abs(endPosition - startPosition));

        while(currentTime <= animationTime)
        {
            currentTime += Time.deltaTime;
            float timeProportion = MathUtility.CurveAsc(currentTime) / animationTime;
            float position = Mathf.Lerp(startPosition, endPosition, timeProportion);
            _rectTransform.position = new Vector2(position, _rectTransform.position.y);

            yield return null;
        }

        _rectTransform.position = new Vector2(endPosition, _rectTransform.position.y);
        yield return null;
    }

}
