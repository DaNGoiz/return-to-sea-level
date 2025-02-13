using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tips : MonoBehaviour
{
    private Sprite[] tips = new Sprite[6];
    private string[] tipsText = { "冲刺会朝反方向释放一颗气泡", "气泡鱼可以用气泡填充爆炸", "皇家气泡鱼需要两个人同时喷气才能打爆", "拾取曼妥思可以回复气体", "队友没气时，可以为队友打气回复", "气泡槽归零时，游戏结束" };
    private Image image;
    private TextMeshProUGUI text;
    private TextMeshProUGUI pageNumber;
    private int index = 0;
    
    void Start()
    {
        image = transform.Find("IntroImage")?.GetComponent<Image>();
        text = transform.Find("IntroText")?.GetComponent<TextMeshProUGUI>();
        pageNumber = transform.Find("Page")?.GetComponent<TextMeshProUGUI>();

        if (image != null)
        {
            image.sprite = Resources.Load<Sprite>("Textures/UI/Tips/TipsPages/tips" + (index + 1));
        }
        if (text != null)
        {
            text.text = tipsText[index];
        }
        
        Messenger.AddListener<bool>(MsgType.TipsFlipPage, FlipPage);
    }

    private void FlipPage(bool isNextPage)
    {
        if (isNextPage)
        {
            NextPage();
        }
        else
        {
            PreviousPage();
        }

        if (pageNumber != null)
        {
            pageNumber.text = (index + 1) + "/6";
        }
    }

    private void NextPage()
    {
        if (index < tips.Length - 1)
        {
            index++;
        }
        else
        {
            index = 0;
        }

        if (image != null)
        {
            image.sprite = Resources.Load<Sprite>("Textures/UI/Tips/TipsPages/tips" + (index + 1));
        }
        if (text != null)
        {
            text.text = tipsText[index];
        }
    }

    private void PreviousPage()
    {
        if (index > 0)
        {
            index--;
        }
        else
        {
            index = tips.Length - 1;
        }

        if (image != null)
        {
            image.sprite = Resources.Load<Sprite>("Textures/UI/Tips/TipsPages/tips" + (index + 1));
        }
        if (text != null)
        {
            text.text = tipsText[index];
        }
    }
}