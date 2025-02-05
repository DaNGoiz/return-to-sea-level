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
                GenInterval = GlobalData.DefaultObjGenInterval - 3f * ( DifficultyManager.DiffFactor / 5) * Random.Range(0.8f, 1.2f);
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
        UnderwaterObjPool.Presets = Resources.Load<UnderwaterObjPool>("Prefabs/Map/ObjPresets/Difficulty/UnderwaterObjPool").presets;
        Debug.Log("");
    }
    public void ResetGenerator()
    {
        lastGenDistance = 0;
        IsEnabled = false;
        GenInterval = GlobalData.DefaultObjGenInterval;
    }
    public void Generate()
    {
        ObjPreset objPreset = UnderwaterObjPool.GetObj();
        //将物品生成到场景中，并将生成好的物体设置为ObjParent的子物体
        GameObject objCache;
        int sortingOrder = 0;
        foreach(ObjData data in objPreset.datas)
        {
            objCache = Instantiate(data.obj);
            objCache.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder++;
            objCache.transform.SetParent(ObjParent.transform, false);
            if (data.randomPosition)
            {
                objCache.transform.localPosition= new Vector3(
                    Random.Range(data.randPosXMin, data.randPosXMax),
                    Random.Range(data.randPosYMin * 2.5f, data.randPosYMax * 2.5f), 0);
            }
            else
            {
                objCache.transform.localPosition = data.position;
            }
            if (data.randomScale)
            {
                float scale = Random.Range(data.randScaleMin, data.randScaleMax);
                objCache.transform.localScale = new Vector3(scale, scale, 1);
            }
            if (data.randomOrientation && Random.Range(0, 2) == 0)
            {
                objCache.transform.localScale = new Vector3(-objCache.transform.localScale.x, objCache.transform.localScale.y, 1);
            }
        }
    }
}
