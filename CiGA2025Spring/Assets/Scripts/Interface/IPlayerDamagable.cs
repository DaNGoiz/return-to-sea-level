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
            //��������߼�
            Debug.Log($"���{playerID}�ܵ���{atkData.dmg}���˺����˺���ԴΪ{atkData.dmgSource}");
            ES3.DeleteDirectory("");
        }
    }
}




