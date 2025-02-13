using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelectResponse : MonoBehaviour
{
    private GameObject player1component;
    private GameObject player2component;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        InitializePlayerComponents();

        Messenger.AddListener<bool>(MsgType.Player1Selected, Player1CompChangeState);
        Messenger.AddListener<bool>(MsgType.Player2Selected, Player2CompChangeState);
        
        if (GlobalData.Player1Selected)
        {
            Player1CompChangeState(true);
        }
        else 
        {
            Player1CompChangeState(false);
        }

        if (GlobalData.Player2Selected)
        {
            Player2CompChangeState(true);
        }
        else 
        {
            Player2CompChangeState(false);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializePlayerComponents();
    }

    void InitializePlayerComponents()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Player1"))
            {
                player1component = child.gameObject;
            }
            if (child.CompareTag("Player2"))
            {
                player2component = child.gameObject;
            }
        }
    }

    private void Player1CompChangeState(bool isPlayer1Selected)
    {
        if (player1component != null)
        {
            player1component.SetActive(isPlayer1Selected);
        }
        else
        {
            Debug.LogWarning("Player1 component is missing or destroyed.");
        }
    }

    private void Player2CompChangeState(bool isPlayer2Selected)
    {
        if (player2component != null)
        {
            player2component.SetActive(isPlayer2Selected);
        }
        else
        {
            Debug.LogWarning("Player2 component is missing or destroyed.");
        }
    }

    void OnDestroy()
    {
        Messenger.RemoveListener<bool>(MsgType.Player1Selected, Player1CompChangeState);
        Messenger.RemoveListener<bool>(MsgType.Player2Selected, Player2CompChangeState);

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
