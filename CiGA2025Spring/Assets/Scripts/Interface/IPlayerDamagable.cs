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
            else if (playerID == 2)
            {
                Messenger.Broadcast(MsgType.Player2Hurt);
            }
            //��������߼�
            Debug.Log($"���{playerID}�ܵ���{atkData.dmg}���˺����˺���ԴΪ{atkData.dmgSource}");
        }
    }
}




