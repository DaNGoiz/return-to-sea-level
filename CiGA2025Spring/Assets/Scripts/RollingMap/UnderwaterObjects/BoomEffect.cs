using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEffect : MonoBehaviour
{
    public static void Set(Vector3 position, float scale)
    {
        Transform go = Instantiate(Resources.Load<GameObject>("Prefabs/Map/BoomEffect")).transform;
        go.position = position;
        go.localScale = new Vector3(scale, scale, 1);
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
