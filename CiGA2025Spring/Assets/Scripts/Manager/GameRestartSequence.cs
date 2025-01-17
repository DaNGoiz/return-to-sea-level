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
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Summary");
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Title", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }
}
