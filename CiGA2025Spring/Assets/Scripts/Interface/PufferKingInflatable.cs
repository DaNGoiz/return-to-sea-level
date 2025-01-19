using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferKingInflatable : IInflatable
{
    private bool Inflated { get; set; }
    private void Awake()
    {
        Inflated = false;
    }
    public override void Inflate(float volumn)
    {
        if (!Inflated)
        {
            //设置动画参数到准备引爆
            Inflated = true;
            GetComponent<Animator>().SetTrigger("ReadyToExplode");
        }
        else
        {
            //设置动画参数到引爆
            GetComponent<Animator>().SetTrigger("Explode");
            GetComponent<Collider2D>().enabled = false;
            Destroy(GetComponentInChildren<PufferAttacker>());
        }
    }
    public void ResetInflation()
    {
        Inflated = false;
    }
    public void Explode()
    {
        BoomEffect.Set(transform.position, 0.8f * Mathf.Abs(transform.localScale.x));
        //发射6-10个气泡球
        int bubbleNum = Random.Range(6, 11);
        for (int i = 0; i < bubbleNum; i++)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/Map/LittleBubble"));
            go.AddComponent<BubbleSpread>();
            go.transform.position = transform.position;
        }
        Destroy(gameObject);
    }
}
