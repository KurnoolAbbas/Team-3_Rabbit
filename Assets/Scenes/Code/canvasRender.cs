using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasRender : MonoBehaviour
{// Reference to UI elements you want to scale
    public RectTransform[] uiElements;

    void Start()
    {
        // Get the current screen width and height
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Calculate the reference screen width and height as half of the screen size
        float referenceScreenWidth = screenWidth / 2f;
        float referenceScreenHeight = screenHeight / 2f;

        // Calculate the scale factor for the UI elements
        float widthRatio = screenWidth / referenceScreenWidth;
        float heightRatio = screenHeight / referenceScreenHeight;
        float scaleFactor = Mathf.Min(widthRatio, heightRatio);

        // Scale and position the UI elements
        foreach (RectTransform uiElement in uiElements)
        {
            uiElement.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
            // Optionally, adjust the position of UI elements based on the screen size
            // For example:
            // uiElement.localPosition = new Vector3(uiElement.localPosition.x * widthRatio, uiElement.localPosition.y * heightRatio, uiElement.localPosition.z);
        }
    }
}
