using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public static ObjectGenerator Instance;
    public static bool IsEnabled { get; set; }
    private static float GenInterval { get; set; }
    private static float lastGenDistance;
    private static GameObject ObjParent { get; set; }
    private void Awake()
    {
        Instance = this;
        Init();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsEnabled)
        {
            if (GlobalData.Distance - lastGenDistance >= GenInterval)
            {
                //��������������߼�
                Generate();
                lastGenDistance = GlobalData.Distance;
            }
        }
    }
    public void Init()
    {
        lastGenDistance = 0;
        IsEnabled = false;
        GenInterval = GlobalData.DefaultObjGenInterval;
        ObjParent = new("UnderwaterObjects");
        ObjParent.transform.position = new Vector3(0, 7, 0);
        UnderwaterObjPool.ObjPresets = Resources.Load<UnderwaterObjPool>("Prefabs/Map/ObjPresets/UnderwaterObjPool").objPresets;
    }
    public void ResetGenerator()
    {
        lastGenDistance = 0;
        IsEnabled = false;
        GenInterval = GlobalData.DefaultObjGenInterval;
    }
    public void Generate()
    {
        //�����ѶȺ�������ɵ�������Ʒ�����������Ľ������ϰ�������Ʒ���г�ȡ��Ʒ
        ObjPreset objPreset = UnderwaterObjPool.GetObj();
        //����Ʒ���ɵ������У��������ɺõ���������ΪObjParent��������
        GameObject objCache;
        foreach(ObjData data in objPreset.datas)
        {
            objCache = Instantiate(data.obj);
            objCache.transform.SetParent(ObjParent.transform, false);
            if (data.randomPosition)
            {
                objCache.transform.localPosition= new Vector3(
                    Random.Range(data.randPosXMin, data.randPosXMax), 0, 0);
            }
            else
            {
                objCache.transform.localPosition = new Vector3(data.positionX, 0, 0);
            }
        }
    }
}
