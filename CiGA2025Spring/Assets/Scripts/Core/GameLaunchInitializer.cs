using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class GameLaunchInitializer : MonoBehaviour
{
    private void Awake()
    {
        //≥ı ºªØµÿÕº
        MapInitializer.Init();
    }
}
