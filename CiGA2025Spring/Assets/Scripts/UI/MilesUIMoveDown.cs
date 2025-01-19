using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilesUIMoveDown : MonoBehaviour
{
    void Start()
    {
        Debug.Log(transform.localPosition);
        Debug.Log(transform.position);
        Messenger.AddListener(MsgType.GameStart, MoveDown);
        Messenger.AddListener(MsgType.ResetPlayer, ResetPlayer);
    }

    private void MoveDown()
    {
        StartCoroutine(MoveDownCoroutine());
    }

    private void ResetPlayer()
    {
        transform.position = new Vector3(640, 783, 0);
    }

    IEnumerator MoveDownCoroutine()
    {
        while (transform.localPosition.y > 300)
        {
            transform.localPosition = new Vector3(0, transform.localPosition.y - 1, 0);
            yield return null;
        }
        Debug.Log(transform.position);
    }
}
