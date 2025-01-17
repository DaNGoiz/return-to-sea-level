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
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Summary", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        // 传输距离，
    }
}
