using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBarController : MonoBehaviour
{
    [SerializeField] private Color _speedColor;
    [SerializeField] private Color _baseColor;
    [SerializeField] private int minSpeedLevel;
    [SerializeField] private float delayBeforeFade = 0.5f;
    [SerializeField] private float fadeOutDuration = 2f;
    [SerializeField] private CanvasGroup _canvasGroup;

    private int maxSpeedLevel;

    private List<Image> _speedBatchs = new();
    private int _speedLevel = 1;

    public int MaxSpeedLevel => maxSpeedLevel;

    public IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(delayBeforeFade);

        float elapsedTime = 0f;
        float startAlpha = _canvasGroup.alpha;

        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeOutDuration);
            _canvasGroup.alpha = newAlpha;
            yield return null;
        }

        _canvasGroup.alpha = 0f;
    }

    private void OnEnable()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            _speedBatchs.Add(transform.GetChild(i).GetComponent<Image>());
        }
        maxSpeedLevel = _speedBatchs.Count;
    }
    public void IncreaseSpeed()
    {
        if (_speedLevel < maxSpeedLevel)
        {
            ++_speedLevel;
            _speedBatchs[(maxSpeedLevel - _speedLevel)].color = _speedColor;
        }
        StopAllCoroutines();
        _canvasGroup.alpha = 1;
        StartCoroutine(FadeOut());
    }

    public void DecreaseSpeed()
    {
        if (_speedLevel > minSpeedLevel)
        {
            --_speedLevel;
            Debug.Log(_speedLevel);
            _speedBatchs[(maxSpeedLevel - _speedLevel - 1)].color = _baseColor;
        }
        StopAllCoroutines();
        _canvasGroup.alpha = 1;
        StartCoroutine(FadeOut());
    }
}
