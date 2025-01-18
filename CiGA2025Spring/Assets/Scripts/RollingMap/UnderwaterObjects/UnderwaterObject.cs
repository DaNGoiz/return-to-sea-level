using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnderwaterObject : MonoBehaviour
{
    protected virtual void OnSelfDestroyed()
    {
        Messenger.RemoveListener(MsgType.ResetMap, DestroySelf);
    }
    protected void Awake()
    {
        Messenger.AddListener(MsgType.ResetMap, DestroySelf);
    }
    protected void FixedUpdate()
    {
        if (MapManager.Roll)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - GlobalData.MapRollingSpeed, transform.position.z);
            if (transform.position.y < -8)
            {
                DestroySelf();
            }
        }
    }
    protected void DestroySelf()
    {
        OnSelfDestroyed();
        Destroy(gameObject);
    }
}
