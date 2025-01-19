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
        float upwardSpeed = 0.8f;
        float initialDuration = 0.5f;
        float upwardDuration = 0.5f;

        // 计算旋转角度
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // 快速朝 direction 方向移动
        Vector3 initialTarget = transform.position + (Vector3)direction.normalized * initialSpeed;
        transform.DOMove(initialTarget, initialDuration).SetEase(Ease.OutQuad);

        yield return new WaitForSeconds(initialDuration);

        // 匀速向上飘，并更新朝向
        Vector3 upwardTarget = transform.position + Vector3.up * upwardSpeed;
        transform.DOMove(upwardTarget, upwardDuration).SetEase(Ease.Linear);
        transform.DORotate(new Vector3(0, 0, 0f), upwardDuration).SetEase(Ease.Linear);
    }

    IEnumerator ColliderCoroutine()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        yield return new WaitForSeconds(0.05f);
        collider.enabled = true;
    }
}