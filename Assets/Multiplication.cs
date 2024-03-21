using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Multiplication : MonoBehaviour
{
    
    public Text resultText;
    public Canvas panel;

    // Method to display the multiplication table of a given number
    public void DisplayMultiplicationTable(int number)
    {
        string table = "";
        List<string> results = new List<string>();

        // Generate all multiplication results
        for (int i = 1; i <= 10; i++)
        {
            int multiplicationResult = number * i;
            results.Add($"{number}x{i}={multiplicationResult}");
        }

        // Find the maximum length of the results
        int maxLength = 0;
        foreach (string result in results)
        {
            if (result.Length > maxLength)
            {
                maxLength = result.Length;
            }
        }

        // Add padding before each result to ensure consistent spacing
        for (int i = 0; i < results.Count; i++)
        {
            int paddingLength = maxLength - results[i].Length + 3; // Adjust the padding length as needed
            string padding = new string(' ', paddingLength);
            table += $"{padding}{results[i]}";

            // Add new line after every two multiplications except for the last two
            if (i % 2 == 1 && i < results.Count - 2)
            {
                table += "\n";
            }
            else
            {
                table += "   ";
            }
        }

        resultText.text = table;
        SetTextProperties();
        ChangePanelColor();
    }

    private void SetTextProperties()
    {
        // Set text properties
        resultText.fontSize = 30;
        resultText.fontStyle = FontStyle.Bold;
        resultText.lineSpacing = 2;
    }

    private void ChangePanelColor()
    {
        // Generate a random color
        Color color = new Color(Random.value, Random.value, Random.value);

        // Assign the color to the panel
        panel.GetComponent<Image>().color = color;
    }
}
