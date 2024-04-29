using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject nameInputPanel;
    public GameObject welcomePanel;
    public Text welcomeText;
    public InputField nameInputField;

    // Start is called before the first frame update
    void Start()
    {
        // Initially, only the name input panel is active
        nameInputPanel.SetActive(true);
        welcomePanel.SetActive(false);
    }

    public void OnSubmitButtonClicked()
    {
        // Get the input name from the input field
        string playerName = nameInputField.text;

        // Set the welcome text to include the user's name
        welcomeText.text = "Welcome, " + playerName + "!";

        // Deactivate the name input panel
        nameInputPanel.SetActive(false);

        // Activate the welcome panel
        welcomePanel.SetActive(true);
    }
}