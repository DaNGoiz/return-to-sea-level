using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerHurtAnim : MonoBehaviour
{
    public int playerNum;
    SpriteRenderer playerSprite;

    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        
        if (playerNum == 1) 
        {
            Messenger.AddListener(MsgType.Player1IsDying, PlayerGameOver);
            Messenger.AddListener(MsgType.Player1Hurt, PlayerHurt);
        }
        else if (playerNum == 2)
        {
            Messenger.AddListener(MsgType.Player2IsDying, PlayerGameOver);
            Messenger.AddListener(MsgType.Player2Hurt, PlayerHurt);
        }

        Messenger.AddListener(MsgType.ResetPlayer, ResetPlayer);
    }

    public void ResetPlayer()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        Animator playerAnim = GetComponent<Animator>();
        playerAnim.enabled = true;
    }

    public void PlayerHurt()
    {
        StartCoroutine(HurtCoroutine());
        // 播放漏气动画
    }

    private void PlayerGameOver()
    {
        Animator playerAnim = GetComponent<Animator>();
        playerAnim.enabled = false;

        if (playerNum == 1)
        {
            playerSprite.sprite = Resources.Load<Sprite>("Textures/Player/Caco_dead");
        }
        else if (playerNum == 2)
        {
            playerSprite.sprite = Resources.Load<Sprite>("Textures/Player/Wepsi_dead");
        }
        StartCoroutine(DeadCoroutine());
    }

    IEnumerator DeadCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Vector3 target = new Vector3(-15, -15, 0);
        transform.DOJump(target, 2f, -5, 1f);
        yield return new WaitForSeconds(1f);
        Messenger.Broadcast(MsgType.GameOver);
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
