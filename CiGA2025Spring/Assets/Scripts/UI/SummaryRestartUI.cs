using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummaryRestartUI : MonoBehaviour
{
    Button restartButton;

    void Start()
    {
        restartButton = GetComponent<Button>();
        restartButton.onClick.AddListener(() =>
        {
            Messenger.Broadcast(MsgType.GameRestart);
        });
    }
}
