using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRestartSequence : MonoBehaviour
{
    void Start()
    {
        Messenger.AddListener(MsgType.GameRestart, GameRestart);
    }

    private void GameRestart()
    {
        Debug.Log("GameRestart");
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Summary");
        if (UnityEngine.SceneManagement.SceneManager.GetSceneByName("Title").isLoaded == false)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Title", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }
        // 可乐瓶归位
        // 重置距离
        // 清空地图
    }
}
