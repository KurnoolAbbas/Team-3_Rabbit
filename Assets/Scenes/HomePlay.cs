using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomePlay : MonoBehaviour
{
    public Button HomeButton, SettingsButton, CloseButton;
    public GameObject settingsPanel;
    // Start is called before the first frame update
    void Start()
    {
        settingsPanel.SetActive(false);
        SettingsButton.onClick.AddListener(ShowSettings);
        CloseButton.onClick.AddListener(CloseSettings);
        HomeButton.onClick.AddListener(LoadMultScreenScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowSettings()
    {
        settingsPanel.SetActive(true);
        SettingsButton.interactable = false;
    }
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        SettingsButton.interactable = true;
    }
    public void LoadMultScreenScene()
    {
        SceneManager.LoadScene("GameScreen");
    }
}
