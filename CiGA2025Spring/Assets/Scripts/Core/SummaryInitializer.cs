using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummaryInitializer : MonoBehaviour
{
    private void Awake()
    {
        Object.Instantiate(Resources.Load<GameObject>("Prefabs/UI/Canvas_Summary"));
    }
}
