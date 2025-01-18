using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferAttacker : IAttacker
{
    [SerializeField]
    private float dmg;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        TryAttack(other, "Puffer", dmg);
    }
}
