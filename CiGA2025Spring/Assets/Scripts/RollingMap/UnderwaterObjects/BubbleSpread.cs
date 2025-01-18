using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class BubbleSpread : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Spread());
    }
    private IEnumerator Spread()
    {
        Vector3 dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        Vector3 start = transform.position,
            end = transform.position + dir * Random.Range(0f, 2f);
        while ((transform.position - end).x > 0.001f)
        {
            transform.position += 0.05f * (end - transform.position);
            yield return null;
        }
    }
}
