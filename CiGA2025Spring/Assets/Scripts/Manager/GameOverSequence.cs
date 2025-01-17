using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSequence : MonoBehaviour
{
    void Start()
    {
        Messenger.AddListener(MsgType.GameOver, GameOver);
    }

    private void GameOver()
    {
        Debug.Log("GameOver");
        if (UnityEngine.SceneManagement.SceneManager.GetSceneByName("Summary").isLoaded == false)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Summary", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }
        // 传输距离，把信息加入排行榜
    }
}
