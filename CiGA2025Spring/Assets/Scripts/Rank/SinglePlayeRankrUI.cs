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

    private void Awake()
    {
        rankText = transform.Find("Rank").GetComponent<TextMeshProUGUI>();
        scoreText = transform.Find("Score").GetComponent<TextMeshProUGUI>();
        nameText1 = transform.Find("Name1").GetComponent<TextMeshProUGUI>();
        nameText2 = transform.Find("Name2").GetComponent<TextMeshProUGUI>();
        SetPlayerInfo(5, 100, "Player1", "Player2");
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
        scoreText.text = score.ToString() + "m";
    }

    private void SetName(string name1, string name2)
    {
        nameText1.text = name1;
        nameText2.text = name2;
    }
    
}
