using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameLaunchInitializer : MonoBehaviour
{
    private void Awake()
    {
        //生成TitleManager
        Object.Instantiate(Resources.Load<GameObject>("Prefabs/Managers/TitleManager"));
    }
}
