using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperFastKey : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.BackQuote))
        {
            Messenger.Broadcast(MsgType.GameOver);
        }
        // if(Input.GetKeyUp(KeyCode.Alpha2))
        // {
        //     Messenger.Broadcast(MsgType.GameRestart);
        // }
    }
}
