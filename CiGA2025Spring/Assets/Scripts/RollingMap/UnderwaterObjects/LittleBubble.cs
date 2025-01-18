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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider.gameObject.layer == LayerMask.GetMask("UnderwaterObject"))
        {
            BoomEffect.Set(transform.position, 0.4f);
            DestroySelf();
        }
    }
}