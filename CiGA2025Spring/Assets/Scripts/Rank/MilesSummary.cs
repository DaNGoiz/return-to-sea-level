using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MilesSummary : MonoBehaviour
{
    void Start()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        text.text = $"上升了{GlobalData.Distance:N2}m";
    }
}
