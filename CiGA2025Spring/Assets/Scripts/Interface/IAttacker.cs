using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAttacker : MonoBehaviour
{
    public string dmgSource;
    public float dmg;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamagable idmg))
        {
            Debug.Log($"{dmgSource} ���Զ� {other.name} ��� {dmg} ���˺�");
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
