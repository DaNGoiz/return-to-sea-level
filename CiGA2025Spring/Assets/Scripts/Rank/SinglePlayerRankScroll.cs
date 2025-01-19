using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerRankScroll : MonoBehaviour
{
    public float scrollSpeed = 10000f;
    public float upperLimit = 0f;
    public float lowerLimit = -10000f;
    public float initialPositionY = 0f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
        Messenger.AddListener(MsgType.GameOver, ResetPosition);
    }

    void ResetPosition()
    {
        transform.localPosition = initialPosition;
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        Vector3 newPosition = transform.localPosition + new Vector3(0, scroll, 0);

        newPosition.y = Mathf.Clamp(newPosition.y, initialPosition.y + lowerLimit, initialPosition.y + upperLimit);

        transform.localPosition = newPosition;
    }
}