using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputNameEnsureButton : MonoBehaviour
{
    private Button button;
    private TMP_InputField inputField1;
    private TMP_InputField inputField2;

    private void Start()
    {
        button = GetComponent<Button>();
        inputField1 = transform.parent.Find("NameLabel1").GetComponent<TMP_InputField>();
        inputField2 = transform.parent.Find("NameLabel2").GetComponent<TMP_InputField>();

        button.onClick.AddListener(() =>
        {
            string name1 = inputField1.text;
            string name2 = inputField2.text;

            // Save the record to PlayerPrefs
            Save(name1, name2, GlobalData.Distance);

            // Load all the records
            List<(string name1, string name2, float distance)> records = LoadAllRecord();

            // Sort the records by distance in descending order
            records.Sort((a, b) => b.distance.CompareTo(a.distance));

            // Find the rank for the current entry
            for (int i = 0; i < records.Count; i++)
            {
                float tolerance = 0.01f;
                if (Mathf.Abs(records[i].distance - GlobalData.Distance) < tolerance)
                {
                    int rank = i + 1;
                    PlayerPrefs.SetInt("CurrentRank", rank);
                }
                // Debug.Log($"{records[i].name1} + {records[i].name2} : {records[i].distance}");
            }

            // Instantiate and display the rank UI
            GameObject rankPrefab = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Ranks"));
            rankPrefab.transform.SetParent(GameObject.Find("Canvas_Summary(Clone)").transform, false);
            transform.parent.gameObject.SetActive(false);
        });
    }

    // Save player names and distance to PlayerPrefs
    public void Save(string p1, string p2, float distance)
    {
        if (GlobalData.Player1Selected && GlobalData.Player2Selected)
        {
            if (p1 == "" || p1 == null)
            {
                p1 = "anonymous";
            }
            if (p2 == "" || p2 == null)
            {
                p2 = "anonymous";
            }
            
        }
        string key = p1 + '+' + p2;
        
        // Retrieve existing records from PlayerPrefs
        string storedRecords = PlayerPrefs.GetString(key, string.Empty);
        List<float> records = new List<float>();

        if (!string.IsNullOrEmpty(storedRecords))
        {
            // Convert stored records back to list of floats
            string[] recordArray = storedRecords.Split(',');
            foreach (var record in recordArray)
            {
                if (float.TryParse(record, out float result))
                {
                    records.Add(result);
                }
            }
        }

        // Add the new distance record
        records.Add(distance);

        // Save the updated records back to PlayerPrefs
        PlayerPrefs.SetString(key, string.Join(",", records));

        // Save the list of all keys if it's the first time
        string keyList = PlayerPrefs.GetString("AllPlayerKeys", string.Empty);
        if (!keyList.Contains(key))
        {
            keyList += string.IsNullOrEmpty(keyList) ? key : "," + key;
            PlayerPrefs.SetString("AllPlayerKeys", keyList);
        }

        PlayerPrefs.Save();
    }

    // Load all the records from PlayerPrefs
    public List<(string name1, string name2, float distance)> LoadAllRecord()
    {
        List<(string name1, string name2, float distance)> records = new List<(string name1, string name2, float distance)>();

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
}
