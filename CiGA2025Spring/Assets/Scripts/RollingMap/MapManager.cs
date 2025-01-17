using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    private static GameObject mapGo;
    private readonly List<MapGroup> mapGroups = new();
    private void Awake()
    {
        Instance = this;
        mapGroups.Add(new MapGroup("RollingMap_Image", 8f));
    }
    private void FixedUpdate()
    {
        //更新玩家前进距离
        //地图滚动
        foreach (MapGroup group in mapGroups)
        {
            group.Roll();
        }
    }
    public void Init()
    {
        mapGo = Instantiate(Resources.Load<GameObject>(GlobalData.PrefabRoot + "Map/RollingMap"));
        foreach (MapGroup group in mapGroups)
        {
            group.Init();
        }
        //初始化地图滚动速度
        GlobalData.MapRollingSpeed = GlobalData.DefaultMapRollingSpeed;
    }
    public static void StartRolling()
    {

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
                InstNewMap(new Vector3(0, 32 - rollingDistance));
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
            InstNewMap(Vector3.zero);
            InstNewMap(new Vector3(0, 16));
            InstNewMap(new Vector3(0, 32));
        }
    }
}

