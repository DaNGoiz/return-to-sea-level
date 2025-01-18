using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayerDamagable : IDamagable
{
    //�����ţ�������1�������2
    [SerializeField]
    private int playerID;
    public override void Damage(AtkData atkData)
    {
        if (CanHurt)
        {
            if (playerID == 1)
            {
                Messenger.Broadcast(MsgType.Player1Hurt);
            }
            else
            {
                Messenger.Broadcast(MsgType.Player2Hurt);
            }
            Messenger.Broadcast(MsgType.ChangeBubbleBar, playerID, -atkData.dmg);
        }
    }
    private void Start()
    {
        CanHurt = true;
        Messenger.AddListener(MsgType.ResetPlayer, ResetDmgable);
        if (playerID == 1)
        {
            Messenger.AddListener(MsgType.Player1Hurt, Invincible);
        }
        else
        {
            Messenger.AddListener(MsgType.Player2Hurt, Invincible);
        }
    }
    private void Invincible()
    {
        StartCoroutine(Inv());
        IEnumerator Inv()
        {
            CanHurt = false;
            yield return new WaitForSeconds(GlobalData.PlayerInvincibleTime);
            CanHurt = true;
        }
    }
    private void ResetDmgable()
    {
        CanHurt = true;
    }
}




