using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayerList : MonoBehaviour
{
    private List<(string name1, string name2, float distance)> records;
    private GameObject singleRecordPrefab;
    
    void Start()
    {
        singleRecordPrefab = Resources.Load<GameObject>("Prefabs/UI/PlayerInfo");
        records = LoadAllRecord();
        records.Sort((a, b) => b.distance.CompareTo(a.distance));
        for (int i = 0; i < records.Count && i < 10; i++)
        {
            var record = records[i];
            var go = Instantiate(singleRecordPrefab, transform);
            go.transform.localPosition = new Vector3(0, -(i-1) * 100, 0);
            go.GetComponent<SinglePlayeRankrUI>().SetPlayerInfo(i + 1, (int)record.distance, record.name1, record.name2);
            
        }
    }

    private List<(string name1, string name2, float distance)> LoadAllRecord()
    {
        string[] files = ES3.GetFiles();
        List<(string name1, string name2, float distance)> records = new();
        string name1, name2;
        float[] rec;

        foreach (string file in files)
        {
            name1 = file.Split('+')[0];
            name2 = file.Split('+')[1];
            rec = ES3.Load<float[]>(key: "Record", filePath: file);

            for (int i = 0; i < rec.Length; i++)
            {
                records.Add((name1, name2, rec[i]));
            }
        }
        return records;
    }

}
