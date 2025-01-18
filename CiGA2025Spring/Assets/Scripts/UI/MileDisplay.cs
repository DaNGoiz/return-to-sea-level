using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MileDisplay : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = $"{GlobalData.Distance:N2}m";
    }
}
