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
    public Vector2 position;
    public GameObject obj;
}