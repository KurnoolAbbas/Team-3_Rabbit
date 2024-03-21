using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    public InputField nameInputField;
    //public Sprite pencilIcon; // Reference to the pencil icon sprite

    void Start()
    {
        // Create a new image GameObject for the pencil icon
       // GameObject pencilIconObject = new GameObject("PencilIcon");
        //pencilIconObject.transform.SetParent(nameInputField.transform, false);

        // Add an Image component to the pencil icon GameObject
        //Image imageComponent = pencilIconObject.AddComponent<Image>();
       // imageComponent.sprite = pencilIcon;

        // Set the position of the pencil icon within the Input Field
        //RectTransform rectTransform = pencilIconObject.GetComponent<RectTransform>();
        //rectTransform.anchoredPosition = new Vector2(20f, 0f); // Adjust the position as needed

        // Make the pencil icon interactable (optional)
       // pencilIconObject.AddComponent<Button>();
    }

    // Method to handle the "My Name Is" button click event
    public void OnNameButtonClicked()
    {
        string playerName = nameInputField.text;
        Debug.Log("Player's Name: " + playerName);
    }
}
