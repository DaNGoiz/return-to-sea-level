using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleBubble : PickableObject
{
    protected override void Pick(int playerNum)
    {
        base.Pick(playerNum);
        BoomEffect.Set(transform.position, 0.4f);
        Messenger.Broadcast(MsgType.ChangeBubbleBar, playerNum, 5f);
        DestroySelf();
    }
    protected new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.layer == 9)
        {
            BoomEffect.Set(transform.position, 0.4f);
            DestroySelf();
        }
    }
}