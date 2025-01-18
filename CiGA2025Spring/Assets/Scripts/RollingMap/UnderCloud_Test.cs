using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderCloud_Test : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(TestCor());
    }
    private IEnumerator TestCor()
    {
        ObjectGenerator.Instance.Generate();
        yield return null;
    }
}
