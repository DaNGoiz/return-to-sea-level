using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlobalDataInitialize : MonoBehaviour
{
    private void Awake()
    {
        GlobalData.Player1Selected = true;
        GlobalData.Player2Selected = false;
        Messenger.Broadcast<bool>(MsgType.Player1Selected, GlobalData.Player1Selected);
        Messenger.Broadcast<bool>(MsgType.Player2Selected, GlobalData.Player2Selected);
    }
}
