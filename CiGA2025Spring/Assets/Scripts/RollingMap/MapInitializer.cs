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
        //��ӵ�ͼ���������С���ʼ��Ϸ����صĳ�ʼ���߼���GameStart�¼��ļ���
        Messenger.AddListener(MsgType.GameRestart, MapManager.StartRolling);
    }
}
