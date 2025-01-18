using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAttacker : MonoBehaviour
{
    protected abstract void OnTriggerEnter2D(Collider2D other);
    protected void TryAttack(Collider2D other, string dmgSource, float dmg)
    {
        if (other.TryGetComponent(out IDamagable idmg))
        {
            Debug.Log($"{dmgSource} 尝试对 {other.name} 造成 {dmg} 点伤害");
            idmg.Damage(new AtkData(dmgSource, dmg));
        }
    }
}

public struct AtkData
{
    public string dmgSource;
    public float dmg;
    public AtkData(string dmgSource, float dmg)
    {
        this.dmgSource = dmgSource;
        this.dmg = dmg;
    }
}
