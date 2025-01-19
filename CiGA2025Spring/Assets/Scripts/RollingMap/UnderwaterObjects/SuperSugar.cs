using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSugar : PickableObject
{
    protected override void Pick(int playerNum)
    {
        base.Pick(playerNum);
        BoomEffect.Set(transform.position, 1f * Mathf.Abs(transform.localScale.x));
        Messenger.Broadcast(MsgType.ChangeBubbleBar, playerNum, 30f * Mathf.Abs(transform.localScale.x));
        Messenger.Broadcast(MsgType.InfBubble, playerNum, Random.Range(3f, 7f));
        DestroySelf();
    }
}
