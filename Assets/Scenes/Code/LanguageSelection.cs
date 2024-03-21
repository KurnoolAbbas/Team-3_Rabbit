using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSelection : MonoBehaviour
{
    public Dropdown languageDropdown;

    void Start()
    {
        // Optionally, you can populate the dropdown options dynamically here
        // PopulateDropdown();
    }

    // Method to handle the selection change event
    public void OnLanguageSelected(int index)
    {
        string selectedLanguage = languageDropdown.options[index].text;
        Debug.Log("Selected Language: " + selectedLanguage);

        // You can implement logic here to change the game's language
    }
}
