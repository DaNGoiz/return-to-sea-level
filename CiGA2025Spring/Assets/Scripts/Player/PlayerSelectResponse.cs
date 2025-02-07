using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectResponse : MonoBehaviour
{
    private GameObject player1component;
    private GameObject player2component;
    void Start()
    {
        player1component = transform.Find("Player1").gameObject;
        player2component = transform.Find("Player2").gameObject;

        Messenger.AddListener<bool>(MsgType.Player1Selected, Player1CompChangeState);
        Messenger.AddListener<bool>(MsgType.Player2Selected, Player2CompChangeState);
        
        if (GlobalData.Instance.Player1Selected)
        {
            Player1CompChangeState(true);
        }
        else 
        {
            Player1CompChangeState(false);
        }
        if (GlobalData.Instance.Player2Selected)
        {
            Player2CompChangeState(true);
        }
        else 
        {
            Player2CompChangeState(false);
        }
    }

    private void Player1CompChangeState(bool isPlayer1Selected)
    {
        player1component.SetActive(isPlayer1Selected);
    }

    private void Player2CompChangeState(bool isPlayer2Selected)
    {
        player2component.SetActive(isPlayer2Selected);
    }

}
