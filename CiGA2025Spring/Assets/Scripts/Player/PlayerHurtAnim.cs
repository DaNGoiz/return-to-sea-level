using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtAnim : MonoBehaviour
{
    SpriteRenderer playerSprite;

    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        Messenger.AddListener(MsgType.Player1Hurt, PlayerHurt);
    }

    public void PlayerHurt()
    {
        StartCoroutine(HurtCoroutine());
        // 播放漏气动画
    }

    IEnumerator HurtCoroutine() // dotween
    {
        playerSprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        playerSprite.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        playerSprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        playerSprite.color = Color.white;
    }
}
