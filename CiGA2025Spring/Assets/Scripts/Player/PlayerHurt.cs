using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Messenger.AddListener(MsgType.Player1Hurt, PlayerIsHurt);
    }

    private void PlayerIsHurt()
    {

    }
}
