using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferInflatable : IInflatable
{
    public override void Inflate(float volumn)
    {
        GetComponent<Animator>().SetTrigger("Explode");
        GetComponent<Collider2D>().enabled = false;
        Destroy(GetComponentInChildren<PufferAttacker>());
    }
    public void Explode()
    {
        BoomEffect.Set(transform.position, 0.8f);
        //·¢Éän¸öÆøÅÝÇò
        Destroy(gameObject);
    }
}
