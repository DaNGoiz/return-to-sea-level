using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowTips : MonoBehaviour
{
    private Button button;
    
    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(ShowTip);
        }
    }

    private void ShowTip()
    {
        SceneManager.LoadSceneAsync("Tips", LoadSceneMode.Additive);
    }
}