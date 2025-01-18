using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleStart : MonoBehaviour
{
    void Awake()
    {
        // Cursor.visible = false;
        if (UnityEngine.SceneManagement.SceneManager.GetSceneByName("Game").isLoaded == false)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Game", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }
    }
    void Update()
    {
        if (Input.anyKeyDown)
        {
            // 可以有个协程来做淡出效果，做完以后再卸载场景
            Messenger.Broadcast(MsgType.GameStart);
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Title");
        }
    }
}
