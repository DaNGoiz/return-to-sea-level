using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteRotation : MonoBehaviour
{
    private PlayerMove playerMove;
    private Coroutine rotateCoroutine;

    void Start()
    {
        playerMove = GetComponentInParent<PlayerMove>();
    }

    void Update()
    {
        Vector2 moveDirection = playerMove.GetMoveDirection();
        if (moveDirection != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            if (rotateCoroutine != null)
            {
                StopCoroutine(rotateCoroutine);
            }
            rotateCoroutine = StartCoroutine(SmoothRotate(targetAngle));
        }
    }

    private IEnumerator SmoothRotate(float targetAngle)
    {
        float startAngle = transform.rotation.eulerAngles.z;
        float elapsedTime = 0f;
        float duration = 0.2f; // 平滑旋转的持续时间

        while (elapsedTime < duration)
        {
            float angle = Mathf.LerpAngle(startAngle, targetAngle, elapsedTime / duration);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, targetAngle));
    }
}