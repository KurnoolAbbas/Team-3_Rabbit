using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
   public Dropdown Dropdown;

    // Method to handle the selection change event
    public void OnAgeSelected(int index)
    {
        string selectedAge = Dropdown.options[index].text;
        Debug.Log("Selected Age: " + selectedAge);

        // Implement logic to apply settings or behavior changes based on the selected age
        // For example, adjust game difficulty, content accessibility, etc.
    }
}
