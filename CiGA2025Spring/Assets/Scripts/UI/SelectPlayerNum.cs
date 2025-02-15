using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayerNum : MonoBehaviour
{
    public Sprite[] sprites;
    private float[] width = { 90f, 100f, 80f };

    private int playerMode = 0; // 0 for player1, 1 for 2 players, 2 for player 2
    private Button button;
    private Image image;
    private TMPro.TextMeshProUGUI text;

    void Start()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (playerMode == 0)
        {
            playerMode = 1;
            image.sprite = sprites[1];
            image.rectTransform.sizeDelta = new Vector2(width[1], image.rectTransform.sizeDelta.y);
            GlobalData.Player1Selected = true;
            GlobalData.Player2Selected = true;
            text.text = "双人模式";
        }
        else if (playerMode == 1)
        {
            playerMode = 2;
            image.sprite = sprites[2];
            image.rectTransform.sizeDelta = new Vector2(width[2], image.rectTransform.sizeDelta.y);
            GlobalData.Player1Selected = false;
            GlobalData.Player2Selected = true;
            text.text = "单人模式";
        }
        else if (playerMode == 2)
        {
            playerMode = 0;
            image.sprite = sprites[0];
            image.rectTransform.sizeDelta = new Vector2(width[0], image.rectTransform.sizeDelta.y);
            GlobalData.Player1Selected = true;
            GlobalData.Player2Selected = false;
            text.text = "单人模式";
        }
        Messenger.Broadcast<bool>(MsgType.Player1Selected, GlobalData.Player1Selected);
        Messenger.Broadcast<bool>(MsgType.Player2Selected, GlobalData.Player2Selected);
    }
}
