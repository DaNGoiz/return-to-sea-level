using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sugar : PickableObject
{
    protected override void Pick()
    {
        base.Pick();
        Debug.Log("ʰȡ����˼");
        DestroySelf();
    }
}
