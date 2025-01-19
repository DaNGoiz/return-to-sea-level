using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    public static bool Roll { get; set; }
    private static GameObject mapGo;
    private readonly List<MapGroup> mapGroups = new();
    private void Awake()
    {
        Instance = this;
        mapGroups.Add(new MapGroup("RollingMap_Image", 0.4f));
        mapGroups.Add(new MapGroup("RollingMap_Image1", 0.6f));
        mapGroups.Add(new MapGroup("RollingMap_Image2", 0.8f));
        mapGroups.Add(new MapGroup("RollingMap_Image3", 1.1f));
        mapGroups.Add(new MapGroup("RollingMap_Image4", 1.3f));
    }
    private void FixedUpdate()
    {
        //��ͼ����
        if (Roll)
        {
            foreach (MapGroup group in mapGroups)
            {
                group.Roll();
            }
            //�������ǰ������
            GlobalData.Distance += GlobalData.MapRollingSpeed;
            GlobalData.MapRollingSpeed = GlobalData.DefaultMapRollingSpeed + (DifficultyManager.DiffFactor / 5f) * 0.05f;
            
        }
    }
    public void Init()
    {
        GlobalData.Distance = 0f;
        mapGo = Instantiate(Resources.Load<GameObject>(GlobalData.PrefabRoot + "Map/RollingMap"));
        foreach (MapGroup group in mapGroups)
        {
            group.Init();
        }
        //��ʼ����ͼ�����ٶ�
        GlobalData.MapRollingSpeed = GlobalData.DefaultMapRollingSpeed;
    }
    public void ResetMap()
    {
        GlobalData.Distance = 0f;
        ObjectGenerator.Instance.ResetGenerator();
        Roll = false;
        foreach (MapGroup group in mapGroups)
        {
            group.Reset();
        }
        Messenger.Broadcast(MsgType.ResetPlayer);
        Messenger.Broadcast(MsgType.ResetMap);
    }
    class MapGroup
    {
        private readonly Queue<GameObject> mapImgs = new();
        private readonly string name;
        private float rollingDistance;
        private readonly float rollingSpeedCorrection;
        public MapGroup(string name, float rollingSpeedCorrection)
        {
            this.name = name;
            this.rollingSpeedCorrection = rollingSpeedCorrection;
        }
        public void Roll()
        {
            foreach (GameObject go in mapImgs)
            {
                go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y - GlobalData.MapRollingSpeed * rollingSpeedCorrection, go.transform.position.z);
            }
            rollingDistance += GlobalData.MapRollingSpeed * rollingSpeedCorrection;

            //ÿǰ��16����׼��Ԫ��ľ����ɾ����ɵĵ�ͼ������һ���µĵ�ͼ
            if (rollingDistance > 16)
            {
                rollingDistance -= 16;
                InstNewMap(new Vector3(0, 32 - rollingDistance, rollingSpeedCorrection * 10));
                Destroy(mapImgs.Dequeue());
            }
        }
        private void InstNewMap(Vector3 position)
        {
            //ʵ������ͼ����ͼ
            GameObject img = Instantiate(Resources.Load<GameObject>(GlobalData.PrefabRoot + $"Map/{name}"));
            img.transform.position = position;
            img.transform.SetParent(mapGo.transform);
            mapImgs.Enqueue(img);
        }
        public void Init()
        {
            //����ǰ3�ŵ�ͼ
            InstNewMap(new Vector3(0, 0, rollingSpeedCorrection * 10));
            InstNewMap(new Vector3(0, 16, rollingSpeedCorrection * 10));
            InstNewMap(new Vector3(0, 32, rollingSpeedCorrection * 10));
        }
        public void Reset()
        {
            int c = 0;
            rollingDistance = 0;
            foreach(GameObject go in mapImgs)
            {
                go.transform.position = new Vector3(0, 16 * c, rollingSpeedCorrection * 10);
                c++;
            }
        }
    }
}

