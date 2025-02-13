using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseTips : MonoBehaviour
{
    private Button button;
    
    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(CloseTip);
        }
    }

    private void CloseTip()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Tips");
    }
}
