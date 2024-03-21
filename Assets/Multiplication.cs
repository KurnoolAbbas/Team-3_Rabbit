using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Multiplication : MonoBehaviour
{
    public Text resultText;

    // Method to display the multiplication table of a given number
    public void DisplayMultiplicationTable(int number)
    {
        string table = "";
        for (int i = 1; i <= 10; i++)
        {
            int multiplicationResult = number * i;
            table += $"{number}x{i}={multiplicationResult}";
            if (i % 2 == 0)
            {
                table += "\n\n\n";
            }
            else
            {
                table += "     ";
            }
        }
        resultText.text = table;
    }
}
