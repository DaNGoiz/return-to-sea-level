using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SinglePlayeRankrUI : MonoBehaviour
{
    private TextMeshProUGUI rankText;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI nameText1;
    private TextMeshProUGUI nameText2;
    private GameObject andSign;

    private void Awake()
    {
        rankText = transform.Find("Rank").GetComponent<TextMeshProUGUI>();
        scoreText = transform.Find("Score").GetComponent<TextMeshProUGUI>();
        nameText1 = transform.Find("Name1").GetComponent<TextMeshProUGUI>();
        nameText2 = transform.Find("Name2").GetComponent<TextMeshProUGUI>();
        andSign = transform.Find("&").gameObject;
        SetPlayerInfo(5, 100, "", "");
    }

    public void SetPlayerInfo(int rank, int score, string name1, string name2)
    {
        SetRank(rank);
        SetScore(score);
        SetName(name1, name2);
    }

    private void SetRank(int rank)
    {
        rankText.text = rank.ToString();
    }

    private void SetScore(int score)
    {
        scoreText.text = "<wave>" + score.ToString() + "m";
    }

    private void SetName(string name1, string name2)
    {
        if (name1 == "" && name2 != "")
        {
            nameText1.text = "<wave>" + name2;
            nameText1.color = nameText2.color;
            andSign.SetActive(false);
            return;
        }
        else if (name1 != "" && name2 == "")
        {
            nameText1.text = "<wave>" + name1;
            andSign.SetActive(false);
            return;
        }

        nameText1.text = "<wave>" + name1;
        nameText2.text = "<wave>" + name2;
    }
    
}
