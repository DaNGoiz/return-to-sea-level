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
        //·¢Éä1-4¸öÆøÅÝÇò
        int bubbleNum = Random.Range(1, 4);
        for (int i = 0; i < bubbleNum; i++)
        {
            StartCoroutine(BubbleSpread());
        }
        Destroy(gameObject);

        static IEnumerator BubbleSpread()
        {
            Vector3 dir;
            float speed;
            GameObject bubbleGo;
            speed = Random.Range(0.03f, 0.1f);

            bubbleGo = Instantiate(Resources.Load<GameObject>("Prefabs/Map/LittleBubble"));

            dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            for (int i = 0; i <= 40; i++)
            {
                bubbleGo.transform.position += dir * speed;
                speed /= 10f;
                yield return null;
            }
        }
    }
}
