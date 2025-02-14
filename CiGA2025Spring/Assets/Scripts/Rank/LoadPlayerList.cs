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

        // Display the top 10 records
        for (int i = 0; i < records.Count && i < 10; i++)
        {
            var record = records[i];
            var go = Instantiate(singleRecordPrefab, transform);
            go.transform.localPosition = new Vector3(0, -(i - 1) * 60, 0);
            go.GetComponent<SinglePlayeRankrUI>().SetPlayerInfo(i + 1, (int)record.distance, record.name1, record.name2);
        }
    }

    private List<(string name1, string name2, float distance)> LoadAllRecord()
    {
        List<(string name1, string name2, float distance)> records = new List<(string, string, float)>();

        // Get the list of all player pair keys
        string keyList = PlayerPrefs.GetString("AllPlayerKeys", string.Empty);
        if (string.IsNullOrEmpty(keyList)) return records;

        string[] keys = keyList.Split(',');

        // Iterate through each key and load the corresponding records
        foreach (string key in keys)
        {
            if (key.Contains("+"))
            {
                string name1 = key.Split('+')[0];
                string name2 = key.Split('+')[1];

                string storedRecords = PlayerPrefs.GetString(key, string.Empty);
                string[] recordArray = storedRecords.Split(',');

                foreach (var record in recordArray)
                {
                    if (float.TryParse(record, out float distance))
                    {
                        records.Add((name1, name2, distance));
                    }
                }
            }
        }
        return records;
    }

    // Call this method when saving data
    public static void SaveRecord(string name1, string name2, float distance)
    {
        string key = name1 + "+" + name2;
        string storedRecords = PlayerPrefs.GetString(key, string.Empty);

        List<float> records = new List<float>();
        if (!string.IsNullOrEmpty(storedRecords))
        {
            string[] recordArray = storedRecords.Split(',');
            foreach (var record in recordArray)
            {
                if (float.TryParse(record, out float result))
                {
                    records.Add(result);
                }
            }
        }

        // Add the new distance
        records.Add(distance);

        // Save the updated record list as a comma-separated string
        PlayerPrefs.SetString(key, string.Join(",", records));

        // Add this key to the list of all player keys
        string keyList = PlayerPrefs.GetString("AllPlayerKeys", string.Empty);
        if (!keyList.Contains(key))
        {
            PlayerPrefs.SetString("AllPlayerKeys", keyList + "," + key);
        }

        PlayerPrefs.Save();
    }
}