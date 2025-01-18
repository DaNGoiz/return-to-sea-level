using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sugar : PickableObject
{
    protected override void Pick()
    {
        base.Pick();
        Debug.Log("Ê°È¡ÂüÍ×Ë¼");
        DestroySelf();
    }
}
