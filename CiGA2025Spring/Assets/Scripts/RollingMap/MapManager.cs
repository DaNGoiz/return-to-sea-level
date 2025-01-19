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
        //地图滚动
        if (Roll)
        {
            foreach (MapGroup group in mapGroups)
            {
                group.Roll();
            }
            //更新玩家前进距离
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
        //初始化地图滚动速度
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

            //每前进16个标准单元格的距离就删除最旧的地图，加载一张新的地图
            if (rollingDistance > 16)
            {
                rollingDistance -= 16;
                InstNewMap(new Vector3(0, 32 - rollingDistance, rollingSpeedCorrection * 10));
                Destroy(mapImgs.Dequeue());
            }
        }
        private void InstNewMap(Vector3 position)
        {
            //实例化地图精灵图
            GameObject img = Instantiate(Resources.Load<GameObject>(GlobalData.PrefabRoot + $"Map/{name}"));
            img.transform.position = position;
            img.transform.SetParent(mapGo.transform);
            mapImgs.Enqueue(img);
        }
        public void Init()
        {
            //生成前3张地图
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

