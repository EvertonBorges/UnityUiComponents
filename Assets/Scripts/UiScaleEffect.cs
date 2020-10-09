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
    private bool _normalDirection = true;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.localScale = Vector2.zero;
    }

    public void Scale()
    {
        if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
        _currentCoroutine = StartCoroutine(ScaleEffect());
        _normalDirection = !_normalDirection;
    }

    private IEnumerator ScaleEffect()
    {
        Vector2 startScale = _normalDirection ? Vector2.zero : Vector2.one;
        Vector2 endScale = _normalDirection ? Vector2.one : Vector2.zero;

        float startProportion = _rectTransform.localScale.x / animationTime;
        float currentTime = _normalDirection ? startProportion : (2f - startProportion);

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
