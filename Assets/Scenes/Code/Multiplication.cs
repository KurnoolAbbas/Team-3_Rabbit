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
        int maxLength = 0;

        // Generate all multiplication results and find the longest one
        for (int i = 1; i <= 10; i++)
        {
            int result = number * i;
            string resultString = $"{number}x{i}={result}";
            results.Add(resultString);
            if (resultString.Length > maxLength)
            {
                maxLength = resultString.Length;
            }
        }

        int initialPaddingCount = 7;  // Initial space for each row
        string initialPadding = new string(' ', initialPaddingCount);

        // Base spacing between pairs
        int basePairSpacingCount = 5;
        string basePairSpacing = new string(' ', basePairSpacingCount);

        for (int i = 0; i < results.Count; i += 2)
        {
            string leftResult = results[i];
            string rightResult = (i + 1 < results.Count) ? results[i + 1] : "";

            // Customize spacing based on specific rules
            string pairSpacing = basePairSpacing;
            if (number != 10 && (i + 1 == 1)) // Add an extra space for the first pair nx1 and nx2 when n is 1 to 9
            {
                pairSpacing = new string(' ', basePairSpacingCount + 1);
            }
            if (number != 10 && (i + 1 == 9)) // No extra space for the last pair nx9 and nx10 when n is 1 to 9
            {
                pairSpacing = new string(' ', basePairSpacingCount);
            }
            if (number == 10 && (i + 1 == 9)) // No extra space for the last pair nx9 and nx10 when n is 1 to 9
            {
                pairSpacing = new string(' ', basePairSpacingCount-1);
            }

            // Pad the left result to align both columns, add initial padding to each row
            table += initialPadding + leftResult.PadRight(maxLength + pairSpacing.Length) + rightResult + "\n";
        }

        resultText.text = table;
        SetTextProperties();
        ChangePanelColor();
    }

    private void SetTextProperties()
    {
        // Set text properties
        resultText.fontSize = 35;
        resultText.fontStyle = FontStyle.Bold;
        resultText.alignment = TextAnchor.MiddleLeft;
        resultText.lineSpacing = 2f;
    }

    private void ChangePanelColor()
    {
        // Randomly change the panel's background color
        float r = Random.Range(0f, 0.8f);
        float g = Random.Range(0f, 0.8f);
        float b = Random.Range(0f, 0.8f);
        panel.GetComponent<Image>().color = new Color(r, g, b);
    }
}
