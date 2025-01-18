using UnityEngine;

public class ICoralAttacker : IAttacker
{
    [SerializeField]
    private float dmg;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        TryAttack(other, "Coral", dmg);
    }
}