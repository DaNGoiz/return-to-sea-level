using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tips : MonoBehaviour
{
    private Sprite[] tips = new Sprite[6];
    private string[] tipsText = { "冲刺会朝反方向释放一颗气泡", "河豚可以用气泡充爆", "河豚王需要两个人同时喷气才能打爆", "拾取曼妥思可以回复气体", "队友没气时，可以为队友打气", "气泡槽归零时，游戏结束" };
    private Image image;
    private TextMeshProUGUI text;
    private int index = 0;
    

    void Start()
    {
        image = transform.Find("IntroImage").GetComponent<Image>();
        text = transform.Find("IntroText").GetComponent<TextMeshProUGUI>();
        image.sprite = Resources.Load<Sprite>("Textures/UI/Tips/TipsPages/tips" + (index+1));
        text.text = tipsText[index];
    }

    public void NextPage()
    {
        if (index < tips.Length - 1)
        {
            index++;
        }
        else
        {
            index = 0;
        }
        image.sprite = Resources.Load<Sprite>("Textures/UI/Tips/" + index);
        text.text = tipsText[index];
    }

    public void PreviousPage()
    {
        if (index > 0)
        {
            index--;
        }
        else
        {
            index = tips.Length - 1;
        }
        image.sprite = Resources.Load<Sprite>("Textures/UI/Tips/" + index);
        text.text = tipsText[index];
    }
}
