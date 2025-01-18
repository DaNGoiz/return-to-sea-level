using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sugar : PickableObject
{
    protected override void Pick()
    {
        base.Pick();
        BoomEffect.Set(transform.position, 1f);
        Messenger.Broadcast(MsgType.ChangeBubbleBar, 1, 20f);
        DestroySelf();
    }
}
