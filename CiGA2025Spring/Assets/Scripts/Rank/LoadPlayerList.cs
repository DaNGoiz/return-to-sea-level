using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayerList : MonoBehaviour
{
    private List<(string name1, string name2, float distance)> records;
    private GameObject singleRecordPrefab;
    
    void Start()
    {
        singleRecordPrefab = Resources.Load<GameObject>("PlayerInfo");
        records = LoadAllRecord();
        records.Sort((a, b) => b.distance.CompareTo(a.distance));
        for (int i = 0; i < records.Count && i < 10; i++)
        {
            var record = records[i];
            var go = Instantiate(singleRecordPrefab, transform);
            go.transform.Find("Name1").GetComponent<TMPro.TextMeshProUGUI>().text = record.name1;
            go.transform.Find("Name2").GetComponent<TMPro.TextMeshProUGUI>().text = record.name2;
            go.transform.Find("Distance").GetComponent<TMPro.TextMeshProUGUI>().text = record.distance.ToString();
        }

    }

    private List<(string name1, string name2, float distance)> LoadAllRecord()
    {
        List<(string name1, string name2, float distance)> records = new List<(string name1, string name2, float distance)>();
        var files = ES3.GetFiles();
        foreach (var file in files)
        {
            records.Add(ES3.Load<(string name1, string name2, float distance)>(file));
        }
        return records;
    }

}
