using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankSummary : MonoBehaviour
{
    void Start()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        text.text = $"第{PlayerPrefs.GetInt("CurrentRank")}名";
    }
}
