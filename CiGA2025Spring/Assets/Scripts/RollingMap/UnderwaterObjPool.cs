using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnderwaterObjPool", menuName = "UnderwaterObjPool")]
public class UnderwaterObjPool : ScriptableObject
{
    [SerializeField]
    public List<ObjPreset> objPresets;
    public static List<ObjPreset> ObjPresets;
    public static ObjPreset GetObj()
    {
        int i = Random.Range(0, ObjPresets.Count);
        return ObjPresets[i];
    }
}
