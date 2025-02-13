using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCollaborator : MonoBehaviour
{
    private GameObject collaboratorPanel;
    private Button showButton;

    void Start()
    {
        collaboratorPanel = transform.Find("CollaboratorPanel").gameObject;
        collaboratorPanel.SetActive(false);
        showButton = GetComponent<Button>();

        if (showButton != null)
        {
            showButton.onClick.AddListener(ToggleCollaboratorPanel);
        }
    }

    void ToggleCollaboratorPanel()
    {
        collaboratorPanel.SetActive(!collaboratorPanel.activeSelf);
    }
}