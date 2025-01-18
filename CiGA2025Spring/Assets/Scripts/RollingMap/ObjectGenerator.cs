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
                //触发生成物体的逻辑
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
        //根据难度和最近生成的数个物品（避免连续的奖励或障碍）在物品池中抽取物品
        ObjPreset objPreset = UnderwaterObjPool.GetObj();
        //将物品生成到场景中，并将生成好的物体设置为ObjParent的子物体
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
