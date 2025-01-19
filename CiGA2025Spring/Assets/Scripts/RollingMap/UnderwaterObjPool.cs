using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnderwaterObjPool", menuName = "UnderwaterObjPool")]
public class UnderwaterObjPool : ScriptableObject
{
    [SerializeField]
    public List<DifficultyPreset> presets;
    public static List<DifficultyPreset> Presets;
    public static ObjPreset GetObj()
    {
        int diff = Mathf.Min(DifficultyManager.DiffFactor, Presets.Count - 1),
            i = Random.Range(0, Presets[diff].objPresets.Count);
        return Presets[diff].objPresets[i];
    }
}

