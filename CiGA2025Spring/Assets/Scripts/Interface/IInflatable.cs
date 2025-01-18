using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IInflatable : MonoBehaviour
{
    public bool CanInflate { get; set; }
    public abstract void Inflate(float volumn);
}
