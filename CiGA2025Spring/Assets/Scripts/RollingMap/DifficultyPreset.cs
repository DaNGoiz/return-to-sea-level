using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DifficultyPreset")]
public class DifficultyPreset : ScriptableObject
{
    public List<ObjPreset> objPresets;
}
