using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BubbleBar : MonoBehaviour
{
    [SerializeField]
    public int playerNum;
    private float bubbleValue;
    private float naturalDecreaseValue = 0.5f;
    private Slider slider;
    private Coroutine changeValueCoroutine;
    private bool canChangeValue = false;

    void Start()
    {
        slider = GetComponent<Slider>();
        bubbleValue = slider.value;
        Messenger.AddListener<int, float>(MsgType.ChangeBubbleBar, ChangeValue);
        Messenger.AddListener(MsgType.ResetPlayer, ResetValue);
        Messenger.AddListener(MsgType.GameStart, GameStart);
        Messenger.AddListener(MsgType.GameOver, () => canChangeValue = false);
    }

    void Update()
    {
        if (bubbleValue > slider.minValue && canChangeValue)
        {
            bubbleValue -= naturalDecreaseValue * Time.deltaTime;
            bubbleValue = Mathf.Clamp(bubbleValue, slider.minValue, slider.maxValue);
            slider.value = bubbleValue;
        }
    }

    private void ResetValue()
    {
        bubbleValue = slider.maxValue;
        slider.value = bubbleValue;
    }

    private void GameStart()
    {
        canChangeValue = true;
    }

    public void ChangeValue(int playerNum, float value)
    {
        if (this.playerNum != playerNum || canChangeValue == false)
        {
            return;
        }
        
        bubbleValue += value;
        bubbleValue = Mathf.Clamp(bubbleValue, slider.minValue, slider.maxValue);

        if (changeValueCoroutine != null)
        {
            StopCoroutine(changeValueCoroutine);
        }

        changeValueCoroutine = StartCoroutine(SmoothChangeValue(bubbleValue));
        
        if (bubbleValue == 0)
        {
            canChangeValue = false;
            if (playerNum == 1)
            {
                Messenger.Broadcast(MsgType.Player1IsDying);
            }
            else
            {
                Messenger.Broadcast(MsgType.Player2IsDying);
            }
            // Messenger.Broadcast(MsgType.GameOver);
        }
    }

    private IEnumerator SmoothChangeValue(float targetValue)
    {
        float startValue = slider.value;
        float elapsedTime = 0f;
        float duration = 0.5f;

        if (Mathf.Approximately(startValue, targetValue))
        {
            yield break;
        }

        while (elapsedTime < duration)
        {
            slider.value = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        slider.value = targetValue;
    }
}
