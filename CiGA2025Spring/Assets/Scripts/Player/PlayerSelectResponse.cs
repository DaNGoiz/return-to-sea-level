using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // 需要添加这个命名空间来处理场景加载事件

public class PlayerSelectResponse : MonoBehaviour
{
    public GameObject player1component;
    public GameObject player2component;

    void Start()
    {
        // 订阅场景加载事件，当场景加载时重新初始化玩家组件
        SceneManager.sceneLoaded += OnSceneLoaded;

        // 初次查找带有 Player1 和 Player2 标签的对象
        InitializePlayerComponents();

        // 添加事件监听器
        Messenger.AddListener<bool>(MsgType.Player1Selected, Player1CompChangeState);
        Messenger.AddListener<bool>(MsgType.Player2Selected, Player2CompChangeState);
        
        // 根据 GlobalData 设置玩家选择状态
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

    // 场景加载时重新查找组件
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializePlayerComponents();
    }

    // 查找 Player1 和 Player2 组件
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

    // 确保在销毁该对象时移除监听器，防止内存泄漏
    void OnDestroy()
    {
        Messenger.RemoveListener<bool>(MsgType.Player1Selected, Player1CompChangeState);
        Messenger.RemoveListener<bool>(MsgType.Player2Selected, Player2CompChangeState);

        // 取消订阅场景加载事件
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
