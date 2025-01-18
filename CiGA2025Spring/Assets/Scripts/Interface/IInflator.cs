using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInflator : MonoBehaviour
{
    [SerializeField]
    private float volumn;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInflatable other))
        {
            other.Inflate(volumn);
        }
    }
    protected void DestroySelf()
    {
        Destroy(gameObject);
    }
}
