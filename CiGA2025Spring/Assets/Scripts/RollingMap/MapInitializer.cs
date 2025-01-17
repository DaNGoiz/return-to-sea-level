using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapInitializer
{
    public static void Init()
    {
        //从Resources路径加载地图管理器
        Object.Instantiate(Resources.Load<GameObject>(GlobalData.PrefabRoot + "Map/MapManager"));
        //初始化地图管理器
        MapManager.Instance.Init();
        //添加地图管理器所有“开始游戏”相关的初始化逻辑到GameStart事件的监听
        Messenger.AddListener(MsgType.GameRestart, MapManager.StartRolling);
    }
}
