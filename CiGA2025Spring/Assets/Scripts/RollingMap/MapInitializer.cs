using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapInitializer
{
    public static void Init()
    {
        //��Resources·�����ص�ͼ������
        Object.Instantiate(Resources.Load<GameObject>(GlobalData.PrefabRoot + "Map/MapManager"));
        //��ʼ����ͼ������
        MapManager.Instance.Init();
        //��ӵ�ͼ�������߼�������¼��ļ���
        Messenger.AddListener(MsgType.GameStart, () => MapManager.Roll = true);
        Messenger.AddListener(MsgType.GameOver, () => MapManager.Roll = false);
        Messenger.AddListener(MsgType.GameRestart, MapManager.Instance.ResetMap);
    }
}
