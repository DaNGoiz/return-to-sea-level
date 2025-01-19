using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : IAttacker
{
    [SerializeField]
    private float dmg;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        TryAttack(other, "Player", dmg * Random.Range(1f, 10f));
    }
}
