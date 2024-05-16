using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject panelToOpen;
    public GameObject settingspanel;
    public GameObject settingsMenu;
    public GameObject welcomePanel; // Reference to the welcome panel
    public Text welcomeText; // Reference to the welcome panel's text component
    public Dropdown dropdown;
    public InputField inputField;

    // Method to handle the selection change event
    public void OnAgeSelected(int index)
    {
        string selectedAge = dropdown.options[index].text;
        Debug.Log("Selected Age: " + selectedAge);

        // Implement logic to apply settings or behavior changes based on the selected age
        // For example, adjust game difficulty, content accessibility, etc.
    }

    public void SubmitSettings()
    {
        string newName = inputField.text;

        // Update the database with the new name
        Debug.Log("Name to update in database: " + newName);

        // Call the method to create a new game with the new name
        CreateNewGame(newName);

        // Close the settings panel
        settingspanel.SetActive(false);
        settingsMenu.SetActive(false);

        // Update the welcome text
        if (welcomeText != null)
        {
            welcomeText.text = "Welcome, " + newName + "!";
        }

        // Disable the welcome panel after opening it
        Invoke("DisableWelcomePanel", 2f); // Disable the welcome panel after 2 seconds
    }

    void DisableWelcomePanel()
    {
        welcomePanel.SetActive(false);
    }

    void CreateNewGame(string Name)
    {
        // Call the asynchronous method and pass onSuccess and onError callbacks
        Debug.Log("Creating New User for " + Name);
        PlayerPrefs.SetString("Name", Name);
        PlayerPrefs.SetString("userID", Name);
        PlayerPrefs.Save();
        StartCoroutine(GameScript.CreateNewGame(Name, onSuccess, OnError));
    }

    void onSuccess(string gameId)
    {
        Debug.Log("Game successfully created with gameId: " + gameId);
        PlayerPrefs.SetString("gameID", gameId);
    }

    void OnError(string error)
    {
        Debug.Log(error);
    }

    public void OpenPanel()
    {
        if (panelToOpen != null)
        {
            panelToOpen.SetActive(true);  // Show the panel
        }
    }

    public void OpensettingsPanel()
    {
        if (settingspanel != null)
        {
            settingspanel.SetActive(true);
            settingsMenu.SetActive(true);// Show the panel
        }
    }

    public void Exit()
    {
        settingspanel.SetActive(false);
        panelToOpen.SetActive(false);
    }
}
