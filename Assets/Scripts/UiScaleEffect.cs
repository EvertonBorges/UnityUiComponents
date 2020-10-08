using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[
    RequireComponent(typeof(Graphics))
]
public class UiScaleEffect : MonoBehaviour
{

    [Range(0.1f, 5f)] [SerializeField] private float animationTime = 1f;
    private RectTransform _rectTransform;

    private Coroutine _currentCoroutine = null;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.localScale = Vector2.zero;
    }

    public void ScaleOut() 
    {
        if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(Scale(true));
    }

    public void ScaleIn() {
        if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(Scale(false));
    } 

    private IEnumerator Scale(bool normalDirection)
    {
        Vector2 startScale = normalDirection ? Vector2.zero : Vector2.one;
        Vector2 endScale = normalDirection ? Vector2.one : Vector2.zero;

        float startProportion = _rectTransform.localScale.x / animationTime;
        float currentTime = normalDirection ? startProportion : (2f - startProportion);

        while(currentTime <= animationTime)
        {
            currentTime += Time.deltaTime;
            float timeProportion = MathUtility.CurveAsc(currentTime) / animationTime;
            Vector2 scale = Vector2.Lerp(startScale, endScale, timeProportion);
            _rectTransform.localScale = scale;
            yield return null;
        }

        _rectTransform.localScale = endScale;
        yield return null;

        _currentCoroutine = null;
    }

}
