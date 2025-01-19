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
    public Vector2 position;
    public float randPosXMin;
    public float randPosXMax;
    public float randPosYMin;
    public float randPosYMax;

    public bool randomScale;
    public float randScaleMin;
    public float randScaleMax;

    public bool randomOrientation;
    public GameObject obj;
}