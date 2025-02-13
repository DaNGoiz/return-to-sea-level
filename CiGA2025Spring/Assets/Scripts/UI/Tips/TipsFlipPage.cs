using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsFlipPage : MonoBehaviour
{
    private Button button;
    public bool isNextPage = true; // true for next page, false for previous page


    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(FlipPage);
    }

    private void FlipPage()
    {
        if (isNextPage)
        {
            Messenger.Broadcast<bool>(MsgType.TipsFlipPage, true);
        }
        else
        {
            Messenger.Broadcast<bool>(MsgType.TipsFlipPage, false);
        }
    }
}
