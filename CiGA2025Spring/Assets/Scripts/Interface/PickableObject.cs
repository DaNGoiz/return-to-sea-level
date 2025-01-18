using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickableObject : UnderwaterObject
{
    protected bool CanPick { get; set; }
    private new void Awake()
    {
        base.Awake();
        CanPick = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IPicker>() && CanPick)
        {
            Pick();
        }
    }
    protected virtual void Pick()
    {
        CanPick = false;
    }
}
