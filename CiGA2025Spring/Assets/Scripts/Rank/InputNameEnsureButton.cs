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
            // 从InputField中读取玩家输入的名字
            string name1 = inputField1.text;
            string name2 = inputField2.text;
            // 存储名字
            // Save(name1, name2, GlobalData.Distance);

            Save("11","22",100);
            Save("11","22",200);
            Save("11","12",300);

            List<(string name1, string name2, float distance)> records;
            records = LoadAllRecord();
            // 对记录按距离排序
            records.Sort((a, b) => a.distance.CompareTo(b.distance));

            for (int i = 0; i < records.Count; i++)
            {
                // 打印每个记录
                Debug.Log(records[i].name1 + " " + records[i].name2 + " " + records[i].distance);
                // 更新当前记录
                records[i] = (name1, name2, GlobalData.Distance);
                // 检查是否是本次玩家的记录
                if (records[i].name1 == name1 && records[i].name2 == name2 && records[i].distance == GlobalData.Distance)
                {
                    int rank = i + 1;
                    // 通知排行榜更新，可以在此处做任何排名显示的操作
                    Debug.Log("排名为：" + rank);
                }
            }
        });
    }

    public static void Save(string p1, string p2, float distance)
    {
        string filePath = p1 + '+' + p2;
        if (ES3.KeyExists(filePath))
        {
            float[] records = ES3.Load<float[]>(key: "Record", filePath: filePath);
            float[] newRecords = new float[records.Length + 1];
            for (int i = 0; i < records.Length; i++)
            {
                newRecords[i] = records[i];
            }
            newRecords[records.Length] = distance;
            ES3.Save(key: "Record", value: newRecords, filePath: filePath);
        }
        else
        {
            ES3.Save(key: "Record", value: new float[1] { distance }, filePath: filePath);
        }
    }

    public List<(string name1, string name2, float distance)> LoadAllRecord()
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
