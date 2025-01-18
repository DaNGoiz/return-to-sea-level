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
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IPicker picker) && CanPick)
        {
            Pick(picker.playerNum);
        }
    }
    protected virtual void Pick(int playerNum)
    {
        CanPick = false;
    }
}
