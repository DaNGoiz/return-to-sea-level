using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBubbleTrail : MonoBehaviour
{

    public void SetDirection(Vector2 direction)
    {
        StartCoroutine(TrailCoroutine(direction));
    }
    IEnumerator TrailCoroutine(Vector2 direction)
    {
        float trailTime = 0.5f;
        float trailInterval = 0.1f;
        float trailSpeed = 0.3f;
        float elapsedTime = 0f;
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        yield return new WaitForSeconds(0.2f);
        collider.enabled = true;
        yield return new WaitForSeconds(0.4f);
        trailSpeed = 0.1f;
        while (elapsedTime < trailTime)
        {
            elapsedTime += Time.deltaTime;
            transform.position += (Vector3)direction * trailSpeed;
            yield return new WaitForSeconds(trailInterval);
        }
    }

}
