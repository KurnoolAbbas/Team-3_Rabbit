using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public GameObject panelToOpen;
    public GameObject settingspanel;
    public GameObject settingsMenu;
    public GameObject welcomePanel; // Reference to the welcome panel
    public Text welcomeText; // Reference to the welcome panel's text component
    public Dropdown Dropdown;
    public GameObject submit;
    public Text inputField;

    // Method to handle the selection change event
    public void OnAgeSelected(int index)
    {
        string selectedAge = Dropdown.options[index].text;
        Debug.Log("Selected Age: " + selectedAge);

        // Implement logic to apply settings or behavior changes based on the selected age
        // For example, adjust game difficulty, content accessibility, etc.
    }


    public void SubmitSettings()
    {
        string newText = inputField.text;

        // Update the database with the new text (Replace this with your database update logic)
        Debug.Log("Text to update in database: " + newText);

        // Close the settings panel
        settingspanel.SetActive(false);

        welcomePanel.SetActive(true);

        // Change text in welcome panel
        if (welcomeText != null)
        {
            welcomeText.text = "Welcome, " + newText + "!";
        }
        // Disable the welcome panel after opening it
        Invoke("DisableWelcomePanel", 2f); // Disable the welcome panel after 2 seconds

    }
    void DisableWelcomePanel()
    {
        welcomePanel.SetActive(false);
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
                                         // SceneManager.LoadScene();
        }
    }

    public void EXit()
    {
        settingspanel.SetActive(false);
        panelToOpen.SetActive(false);
    }
}