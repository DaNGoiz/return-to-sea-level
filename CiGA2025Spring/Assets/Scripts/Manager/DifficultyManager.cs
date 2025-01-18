using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static int DiffFactor
    {
        get
        {
            if (GlobalData.Distance < 80f)
                return 0;
            else if (GlobalData.Distance < 180f)
                return 1;
            else if (GlobalData.Distance < 300f)
                return 2;
            else if (GlobalData.Distance < 500f)
                return 3;
            else if (GlobalData.Distance < 750f)
                return 4;
            else
                return 5;
        }
    }
}
