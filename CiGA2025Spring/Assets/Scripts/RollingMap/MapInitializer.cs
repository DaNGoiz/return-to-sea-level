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
        //添加地图管理器逻辑到相关事件的监听
        Messenger.AddListener(MsgType.GameStart, () => MapManager.Roll = true);
        Messenger.AddListener(MsgType.GameOver, () => MapManager.Roll = false);
        Messenger.AddListener(MsgType.GameRestart, MapManager.Instance.ResetMap);
    }
}
