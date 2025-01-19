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
        Vector3 end = transform.position + dir * Random.Range(1f, 2f);
        float endTime = Time.time + 0.5f;
        Debug.Log(end);
        while ((transform.position - end).sqrMagnitude > 0.001f && Time.time < endTime)
        {
            transform.position += 0.05f * (end - transform.position);
            yield return null;
        }
    }
}
