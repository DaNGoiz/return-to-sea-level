using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerBubbleTrail : MonoBehaviour
{
    public void SetDirection(Vector2 direction)
    {
        StartCoroutine(TrailCoroutine(direction));
        StartCoroutine(ColliderCoroutine());
    }

    IEnumerator TrailCoroutine(Vector2 direction)
    {
        float initialSpeed = 5f;
        float upwardSpeed = 1f;
        float initialDuration = 0.5f;
        float upwardDuration = 2f;

        Vector3 initialTarget = transform.position + (Vector3)direction.normalized * initialSpeed;
        transform.DOMove(initialTarget, initialDuration).SetEase(Ease.OutQuad);

        yield return new WaitForSeconds(initialDuration);

        Vector3 upwardTarget = transform.position + Vector3.up * upwardSpeed;
        transform.DOMove(upwardTarget, upwardDuration).SetEase(Ease.Linear);
    }

    IEnumerator ColliderCoroutine()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        yield return new WaitForSeconds(0.05f);
        collider.enabled = true;
    }
}