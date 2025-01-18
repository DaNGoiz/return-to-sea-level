using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainInitializer : MonoBehaviour
{
    private void Awake()
    {
        //Game场景初始化的所有逻辑
        //初始化地图
        MapInitializer.Init();
        //初始化Manger
        Object.Instantiate(Resources.Load<GameObject>("Prefabs/Managers/GameMainManager"));
        //初始化玩家
        Object.Instantiate(Resources.Load<GameObject>("Prefabs/Player/PlayerComponents"));
        //初始化UI
        Object.Instantiate(Resources.Load<GameObject>("Prefabs/UI/Canvas"));
    }
}
