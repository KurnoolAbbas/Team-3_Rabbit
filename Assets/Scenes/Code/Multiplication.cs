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
            if (i == 9)
            {

                results.Add($"  {number}x{i}={multiplicationResult}");
            }
            else
            {

                results.Add($"{number}x{i}={multiplicationResult}");
            }

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
        float r = Random.Range(0f, 0.5f);
        float g = Random.Range(0f, 0.5f);
        float b = Random.Range(0f, 0.5f);
        panel.GetComponent<Image>().color = new Color(r, g, b);

    }
}
