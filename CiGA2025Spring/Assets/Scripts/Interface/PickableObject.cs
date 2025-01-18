using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickableObject : UnderwaterObject
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pick();
    }
    protected abstract void Pick();
}
