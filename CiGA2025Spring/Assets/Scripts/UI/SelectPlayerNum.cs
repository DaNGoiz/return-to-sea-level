using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayerNum : MonoBehaviour
{
    public int playerNum;
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
{
    if (playerNum == 1)
    {
        if (!GlobalData.Player2Selected && GlobalData.Player1Selected)
        {
            return;
        }

        GlobalData.Player1Selected = !GlobalData.Player1Selected;
        Messenger.Broadcast<bool>(MsgType.Player1Selected, GlobalData.Player1Selected);
    }
    else if (playerNum == 2)
    {
        if (!GlobalData.Player1Selected && GlobalData.Player2Selected)
        {
            return;
        }

        GlobalData.Player2Selected = !GlobalData.Player2Selected;
        Messenger.Broadcast<bool>(MsgType.Player2Selected, GlobalData.Player2Selected);
    }
}
}
