using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IDamagable : MonoBehaviour
{
    public bool CanHurt { get; set; }
    public abstract void Damage(AtkData atkData);
}
