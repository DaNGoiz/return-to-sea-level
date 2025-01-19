using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static int DiffFactor
    {
        get
        {
            if (GlobalData.Distance < 50f)
                return 0;
            else if (GlobalData.Distance < 130f)
                return 1;
            else if (GlobalData.Distance < 240f)
                return 2;
            else if (GlobalData.Distance < 320f)
                return 3;
            else if (GlobalData.Distance < 450f)
                return 4;
            else
                return 5;
        }
    }
}
