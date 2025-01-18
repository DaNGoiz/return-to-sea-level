using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sugar : PickableObject
{
    protected override void Pick()
    {
        base.Pick();
        Messenger.Broadcast(MsgType.ChangeBubbleBar, 1, 20f);
        DestroySelf();
    }
}
