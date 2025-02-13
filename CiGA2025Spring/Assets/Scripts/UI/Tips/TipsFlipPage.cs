using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsFlipPage : MonoBehaviour
{
    private Button button;
    public bool isNextPage = true; // true for next page, false for previous page
    private GameObject tips;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(FlipPage);
        tips = GameObject.Find("Tips");
    }

    private void FlipPage()
    {
        if (isNextPage)
        {
            tips.GetComponent<Tips>().NextPage();
        }
        else
        {
            tips.GetComponent<Tips>().PreviousPage();
        }
    }
}
