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
        BoomEffect.Set(transform.position, 0.8f * Mathf.Abs(transform.localScale.x));
        //·¢Éä1-4¸öÆøÅÝÇò
        int bubbleNum = Random.Range(2, 5);
        for (int i = 0; i < bubbleNum; i++)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/Map/LittleBubble"));
            go.AddComponent<BubbleSpread>();
            go.transform.position = transform.position;
        }
        Destroy(gameObject);
    }
}
