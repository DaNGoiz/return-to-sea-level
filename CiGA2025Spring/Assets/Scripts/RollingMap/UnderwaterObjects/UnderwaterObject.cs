using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnderwaterObject : MonoBehaviour
{
    private void OnDestroy()
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
            if (transform.position.y < -10)
            {
                DestroySelf();
            }
        }
    }
    protected virtual void DestroySelf()
    {
        Destroy(gameObject);
    }
}
