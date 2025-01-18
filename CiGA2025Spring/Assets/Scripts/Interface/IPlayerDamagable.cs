using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayerDamagable : IDamagable
{
    //玩家序号，标记玩家1还是玩家2
    [SerializeField]
    private int playerID;
    public override void Damage(AtkData atkData)
    {
        if (CanHurt)
        {
            //玩家受伤逻辑
            Debug.Log($"玩家{playerID}受到了{atkData.dmg}点伤害，伤害来源为{atkData.dmgSource}");
            ES3.DeleteDirectory("");
        }
    }
}




