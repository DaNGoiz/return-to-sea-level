using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleBar : MonoBehaviour
{
    private float bubbleValue; // 泡泡的值
    private Slider slider;
    private Coroutine changeValueCoroutine;

    void Start()
    {
        slider = GetComponent<Slider>();
        bubbleValue = slider.value;
    }

    public void ChangeValue(float value)
    {
        bubbleValue += value;
        bubbleValue = Mathf.Clamp(bubbleValue, slider.minValue, slider.maxValue);

        if (changeValueCoroutine != null)
        {
            StopCoroutine(changeValueCoroutine);
        }
        changeValueCoroutine = StartCoroutine(SmoothChangeValue(bubbleValue));
    }

    private IEnumerator SmoothChangeValue(float targetValue)
    {
        float startValue = slider.value;
        float elapsedTime = 0f;
        float duration = 0.5f; // 滑动效果的持续时间

        while (elapsedTime < duration)
        {
            slider.value = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        slider.value = targetValue;
    }
}