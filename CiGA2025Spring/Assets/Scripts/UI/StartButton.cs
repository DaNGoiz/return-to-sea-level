using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        Messenger.Broadcast(MsgType.GameStart);
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Title");
    }
}
