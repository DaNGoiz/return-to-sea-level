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

            var files = ES3.GetFiles();
            foreach (var file in files)
            {
                ES3.DeleteFile(file);
            }
            Save(name1, name2, 400);
            // 调试时存储一些测试数据
            Save("11", "22", 100);
            Save("11", "22", 200);
            Save("11", "12", 300);

            // 加载所有记录
            List<(string name1, string name2, float distance)> records = LoadAllRecord();

            // 输出所有文件路径，调试时查看是否有正确加载所有文件
            Debug.Log("All files:");
            foreach (var file in ES3.GetFiles())
            {
                Debug.Log(file);
            }

            // 对记录按距离从大到小排序
            records.Sort((a, b) => b.distance.CompareTo(a.distance)); // 修改为从大到小排序

            // 输出所有记录
            for (int i = 0; i < records.Count; i++)
            {
                Debug.Log(records[i].name1 + " " + records[i].name2 + " " + records[i].distance);
            }

            // 更新当前记录
            for (int i = 0; i < records.Count; i++)
            {
                // 更新记录，确保本次玩家的名字被加入
                records[i] = (name1, name2, GlobalData.Distance);

                // 检查是否是本次玩家的记录
                if (records[i].name1 == name1 && records[i].name2 == name2 && records[i].distance == GlobalData.Distance)
                {
                    int rank = i + 1; // 排名从1开始
                    Debug.Log("排名为：" + rank);
                }
            }
        });
    }

    // 修改后的保存逻辑
    public static void Save(string p1, string p2, float distance)
    {
        string filePath = p1 + '+' + p2;

        // 检查是否已经保存了这个路径
        if (ES3.KeyExists(filePath))
        {
            Debug.Log("文件" + filePath + "已经存在");
            // 如果存在，加载现有记录
            float[] records = ES3.Load<float[]>(key: "Record", filePath: filePath);
            Debug.Log("文件" + filePath + "已经有" + records.Length + "条记录");
            
            // 创建一个新的数组，将现有记录和新的记录合并
            float[] newRecords = new float[records.Length + 1];
            for (int i = 0; i < records.Length; i++)
            {
                newRecords[i] = records[i];
            }
            newRecords[records.Length] = distance;
            Debug.Log("新的记录长度为" + newRecords.Length);

            // 保存合并后的记录
            ES3.Save(key: "Record", value: newRecords, filePath: filePath);
        }
        else
        {
            // 如果文件不存在，创建新的记录
            ES3.Save(key: "Record", value: new float[1] { distance }, filePath: filePath);
        }
    }

    // 加载所有记录的方法保持不变
    public List<(string name1, string name2, float distance)> LoadAllRecord()
    {
        string[] files = ES3.GetFiles(); // 获取所有文件路径
        Debug.Log("一共有" + files.Length + "个文件");
        List<(string name1, string name2, float distance)> records = new();
        string name1, name2;
        float[] rec;

        // 遍历所有文件，加载数据
        foreach (string file in files)
        {
            name1 = file.Split('+')[0];  // 获取名字1
            name2 = file.Split('+')[1];  // 获取名字2
            rec = ES3.Load<float[]>(key: "Record", filePath: file); // 加载记录
            Debug.Log("读取文件" + file + " " + rec.Length + "条记录");

            // 添加到记录列表
            for (int i = 0; i < rec.Length; i++)
            {
                Debug.Log("读取结果" + name1 + " " + name2 + " " + rec[i]);
                records.Add((name1, name2, rec[i]));
            }
        }
        return records;
    }
}
