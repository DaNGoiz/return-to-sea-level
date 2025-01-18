using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInflator : MonoBehaviour
{
    [SerializeField]
    private float volumn;
    [SerializeField]
    private bool destroySelfAfterInflating;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInflatable other))
        {
            other.Inflate(volumn);
            if (destroySelfAfterInflating)
            {
                DestroySelf();
            }
        }
    }
    protected void DestroySelf()
    {
        Destroy(gameObject);
    }
}
