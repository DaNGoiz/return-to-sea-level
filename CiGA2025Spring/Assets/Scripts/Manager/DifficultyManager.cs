using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static float DiffFactor
    {
        get
        {
            return GlobalData.Distance / 30f;
        }
    }
}
