using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleBubble : PickableObject
{
    protected override void Pick()
    {
        base.Pick();
        BoomEffect.Set(transform.position, 0.4f);
        Messenger.Broadcast(MsgType.ChangeBubbleBar, 1, 5f);
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