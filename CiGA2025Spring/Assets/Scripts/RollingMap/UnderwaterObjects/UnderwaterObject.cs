using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnderwaterObject : MonoBehaviour
{
    protected virtual void OnSelfDestroy() { }
    private void FixedUpdate()
    {
        if (MapManager.Roll)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - GlobalData.MapRollingSpeed, transform.position.z);
            if (transform.position.y < -6)
            {
                OnSelfDestroy();
                Destroy(gameObject);
            }
        }
    }
}
