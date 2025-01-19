using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sugar : PickableObject
{
    protected override void Pick(int playerNum)
    {
        base.Pick(playerNum);
        BoomEffect.Set(transform.position, 1f * transform.localScale.x);
        Messenger.Broadcast(MsgType.ChangeBubbleBar, playerNum, 20f * transform.localScale.x);
        DestroySelf();
    }
}
