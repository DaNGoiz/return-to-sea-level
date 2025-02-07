using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlobalDataInitialize : MonoBehaviour
{
    private void Awake()
    {
        GlobalData.Player1Selected = true;
        GlobalData.Player2Selected = true;
    }
}
