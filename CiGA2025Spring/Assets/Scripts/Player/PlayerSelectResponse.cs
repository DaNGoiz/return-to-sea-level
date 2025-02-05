using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectResponse : MonoBehaviour
{
    private GameObject player1component;
    private GameObject player2component;
    void Start()
    {
        // 在子物体里寻找带有player1和player2的tag的物体，绑定到component上

        // Messenger listener
        // Messenger.AddListener(MsgType.)
        
        // 读取存在global data的目前玩家，动态加载玩家框
    }

    private void Player1CompChangeState(bool isPlayer1Selected)
    {
        // 这个值等于playercomponent的set active
    }

    private void Player2CompChangeState(bool isPlayer2Selected)
    {
        // 这个值等于playercomponent的set active
    }

}
