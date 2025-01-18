using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewObjPreset", menuName = "ObjPreset")]
public class ObjPreset : ScriptableObject
{
    public List<ObjData> datas;
}

[System.Serializable]
public class ObjData
{
    public bool randomPosition;
    public float positionX;
    public float randPosXMin;
    public float randPosXMax;
    public GameObject obj;
}